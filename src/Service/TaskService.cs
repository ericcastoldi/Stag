using LibGit2Sharp;
using Stag.Configuration;
using Stag.SourceControl;
using Stag.Storage;
using Stag.Tasks;
using System;
using System.Linq;
using GitLab = NGitLab;
using GitLabModels = NGitLab.Models;

namespace Stag.Service
{
    public class TaskService
    {
        private readonly ISettings _settings;
        private readonly IWarehouse<Task> _warehouse;

        public TaskService()
            : this(new Warehouse<Task>(), new Settings())
        {
        }

        internal TaskService(IWarehouse<Task> warehouse, ISettings settings)
        {
            _warehouse = warehouse;
            _settings = settings;
        }

        public void StartWork(string taskTitle, string taskId)
        {
            StartWork(new Task(taskTitle, taskId));
        }

        public void StartWork(Task task)
        {
            if (task == null || (string.IsNullOrWhiteSpace(task.Id) || string.IsNullOrWhiteSpace(task.Title)))
                throw new ArgumentNullException("task");

            var git = new Git();
            UpdateWorkBranch(git);
            var taskBranch = CreateTaskBranch(task, git);

            task.DevelopmentBranchName = taskBranch.Name;

            _warehouse.Store(task);
        }

        public void Merge(Task task)
        {
            if (task == null
                || string.IsNullOrWhiteSpace(task.DevelopmentBranchName))
            {
                throw new ArgumentNullException("task");
            }

            var git = new Git();
            UpdateWorkBranch(git);

            git.Merge(task.DevelopmentBranchName);

            _warehouse.Store(task);
        }

        public GitLabModels.MergeRequest Submit(Task task)
        {
            // TODO: Definir a implementação dessas validações
            if (task == null
                || (string.IsNullOrWhiteSpace(task.Id)
                    || string.IsNullOrWhiteSpace(task.Title)))
            {
                throw new ArgumentNullException("task");
            }

            var gitLab = GitLab.GitLabClient.Connect(_settings.RemoteUrl, _settings.GitLabPrivateToken);
            var project = gitLab.Projects.Accessible.Where(p => p.Name == _settings.GitLabProjectName).First();
            var approver = gitLab.Users.All.Where(u => u.Username == _settings.Username).First();

            var mr = new GitLabModels.MergeRequestCreate()
            {
                Title = string.Format("{0} - {1}", task.Id, task.Title),
                TargetBranch = _settings.WorkBranch,
                AssigneeId = approver.Id
            };

            var mergeRequest = gitLab.GetMergeRequest(project.Id).Create(mr);

            _warehouse.Store(task);

            return mergeRequest;
        }

        private void UpdateWorkBranch(Git git)
        {
            git.Checkout();
            var mergeResult = git.UpdateCurrentBranch();

            if (mergeResult.Status == MergeStatus.Conflicts)
            {
                throw new GitException(string.Format("O workspace {0} contém modificações que conflitam com o Pull. Limpe seu repositório e tente novamente.", _settings.Workspace));
            }
        }

        private static Branch CreateTaskBranch(Task task, Git git)
        {
            // TODO: Se o cara alterar o nome do branch na tela, ele vai ignorar aqui pois cria de novo. O nome deve ser propagado até aqui.
            var branchNamingService = new BranchNamingService();
            var branchName = branchNamingService.CreateDevelopmentBranchName(task);

            git.CreateBranch(branchName);
            return git.Checkout(branchName);
        }
    }
}