using LibGit2Sharp;
using GitLab = NGitLab;
using GitLabModels = NGitLab.Models;
using Stag.Configuration;
using Stag.Model;
using Stag.SourceControl;
using Stag.Storage;
using Stag.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Stag.Service
{
    public class TaskService
    {
        private Settings _settings;
        private Warehouse<Task> _warehouse;

        public TaskService()
        {
            _settings = new Settings();
            _warehouse = new Warehouse<Task>();
        }

        public void StartWork(string taskTitle, string taskId = null)
        {
            StartWork(new Task(taskTitle, taskId));
        }

        public void StartWork(Task task)
        {
            var git = new Git();
            UpdateWorkBranch(git);
            var taskBranch = CreateTaskBranch(task, git);
            
            task.State = TaskState.Started;
            task.DevelopmentBranchName = taskBranch.Name;

            _warehouse.Store(task);
        }

        public void Merge(Task task)
        {
            var git = new Git();
            UpdateWorkBranch(git);
            
            var mergeBranch = CreateMergeBranch(task, git);

            git.Merge(task.DevelopmentBranchName);

            task.MergeBranchName = mergeBranch.Name;
            _warehouse.Store(task);
        }

        public GitLabModels.MergeRequest Submit(Task task)
        {
            var git = new Git();
            git.Push(task.MergeBranchName);

            var gitLab = GitLab.GitLabClient.Connect(_settings.RemoteUrl, _settings.GitLabPrivateToken);
            var project = gitLab.Projects.Accessible.Where(p => p.Name == _settings.GitLabProjectName).First();
            var approver = gitLab.Users.All.Where(u => u.Username == _settings.Username).First();

            var mr = new GitLabModels.MergeRequestCreate()
            {
                Title = string.Format("{0} - {1}", task.Id, task.Title),
                SourceBranch = task.MergeBranchName,
                TargetBranch = _settings.WorkBranch,
                AssigneeId = approver.Id
            };

            var mergeRequest = gitLab.GetMergeRequest(project.Id).Create(mr);

            task.State = TaskState.Completed;
            _warehouse.Store(task);

            return mergeRequest;
        }

        public IList<Task> MyTasks()
        {
            var helpnetTasks = GetHelpnetTasks();
            var localTaskList = _warehouse.Retrieve().ToList();

            foreach (var task in helpnetTasks)
            {
                if (!localTaskList.Any(p => p.Id == task.Id))
                {
                    localTaskList.Add(task);
                }
            }

            return localTaskList.AsQueryable().Where(t => t.State != TaskState.Completed).ToList();
        }

        public IList<Task> GetHelpnetTasks()
        {
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();

                var query = "SELECT TarID, TarTitulo FROM Tarefa WHERE UsuIDResponsavel = (SELECT UsuID FROM Usuario WHERE UsuUsuario = @usuario)";
                cmd.Parameters.Add(new SqlParameter("usuario", _settings.Username.ToLower()));

                if (!string.IsNullOrWhiteSpace(_settings.MiscTaskId))
                {
                    query += " AND TarID <> @miscTaskId";
                    cmd.Parameters.Add(new SqlParameter("miscTaskId", _settings.MiscTaskId));
                }

                cmd.CommandText = query;

                using (var dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    var taskList = new List<Task>();
                    while (dataReader.Read())
                    {
                        var id = dataReader["TarId"].ToString();
                        var title = dataReader["TarTitulo"].ToString();

                        taskList.Add(new Task(title, id));
                    }

                    return taskList;
                }
            }
        }

        private void UpdateWorkBranch(Git git)
        {
            var baseBranch = git.Checkout();
            var mergeResult = git.UpdateCurrentBranch();

            if (mergeResult.Status == MergeStatus.Conflicts)
            {
                throw new GitException(string.Format("O workspace {0} contém modificações que conflitam com o Pull. Limpe seu repositório e tente novamente.", _settings.Workspace));
            }
        }

        private Branch CreateMergeBranch(Task task, Git git)
        {
            // TODO: Se o cara alterar o nome do branch na tela, ele vai ignorar aqui pois cria de novo. O nome deve ser propagado até aqui.
            var branchNamingService = new BranchNamingService();
            var branchName = branchNamingService.CreateMergeBranchName(task); 

            var mergeBranch = git.CreateBranch(branchName);
            return git.Checkout(branchName);
        }

        private Branch CreateTaskBranch(Task task, Git git)
        {
            // TODO: Se o cara alterar o nome do branch na tela, ele vai ignorar aqui pois cria de novo. O nome deve ser propagado até aqui. 
            var branchNamingService = new BranchNamingService();
            var branchName = branchNamingService.CreateDevelopmentBranchName(task); 

            var taskBranch = git.CreateBranch(branchName);
            return git.Checkout(branchName);
        }

        private IDbConnection OpenConnection()
        {
            const string connectionString = "ConnectionString";

             var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}
