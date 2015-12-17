using LibGit2Sharp;
using Stag.Configuration;
using Stag.SourceControl;
using Stag.Storage;
using Stag.Tasks;
using System;
using System.Globalization;
using System.Linq;
using GitLab = NGitLab;
using GitLabModels = NGitLab.Models;

namespace Stag.Service
{
    public class TaskService
    {
        private static CultureInfo DefaultCulture = new CultureInfo("pt-BR");

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

        public ServiceResult CreateTaskBranch(Task task, string branchName)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            if (string.IsNullOrWhiteSpace(branchName))
                throw new ArgumentNullException("branchName");

            try
            {
                var resultDescription = string.Format(DefaultCulture, "Branch '{0}' criado com sucesso!", branchName);

                var git = new Git();
                git.Checkout(_settings.WorkBranch);
                git.UpdateCurrentBranch();

                try
                {
                    git.CreateBranch(branchName);
                }
                catch (NameConflictException)
                {
                    resultDescription = string.Format(DefaultCulture, "O branch '{0}' já existe. Feito checkout do branch '{0}'.", branchName);
                }
                finally
                {
                    git.Checkout(branchName);
                }

                task.DevelopmentBranchName = branchName;
                _warehouse.Store(task);

                return new ServiceResult(true, resultDescription);
            }
            catch (MergeConflictException)
            {
                var message = string.Format(DefaultCulture, "Ocorreram conflitos ao tentar atualizar o branch '{0}'. Resolva os conflitos em seu workspace ({1}) e tente novamente.",
                    _settings.WorkBranch, _settings.Workspace);

                return new ServiceResult(false, message);
            }
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
                Title = task.ToString(),
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
                throw new GitException(string.Format(DefaultCulture, "O workspace {0} contém modificações que conflitam com o Pull. Limpe seu repositório e tente novamente.", _settings.Workspace));
            }
        }
    }
}