using Stag.Model;
using Stag.Service;
using Stag.Storage;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Stag
{
    public partial class Main : Form
    {
        private Task _currentTask;

        public Main()
        {
            _currentTask = null;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadMyTasks();
        }

        private void LoadMyTasks()
        {
            var taskService = new TaskService();

            var tasks = taskService.MyTasks();
            cmbTasks.Items.AddRange(tasks.ToArray());
        }

        private Task GetSelectedTask()
        {
            var taskIndex = cmbTasks.SelectedIndex;
            var task = cmbTasks.Items[taskIndex] as Task;

            return task;
        }

        private void LoadTask(Task task)
        {
            var namingService = new BranchNamingService();

            txtDevelopmentBranch.Text = task.DevelopmentBranchName ?? namingService.CreateDevelopmentBranchName(task);
            txtMergeBranch.Text = task.MergeBranchName ?? namingService.CreateMergeBranchName(txtDevelopmentBranch.Text);

            _currentTask = task;
        }

        private void cmbTasks_SelectedValueChanged(object sender, EventArgs e)
        {
            var task = GetSelectedTask();

            var namingService = new BranchNamingService();
            txtDevelopmentBranch.Text = namingService.CreateDevelopmentBranchName(task);
            txtDevelopmentBranch.Enabled = true;
        }

        private void btnCreateDevelopmentBranch_Click(object sender, EventArgs e)
        {
            var taskIndex = cmbTasks.SelectedIndex;
            var task = cmbTasks.Items[taskIndex] as Stag.Model.Task;

            var taskService = new TaskService();
            taskService.StartWork(task);
        }

        private void btnCreateMergeBranch_Click(object sender, EventArgs e)
        {
            var taskIndex = cmbTasks.SelectedIndex;
            var task = cmbTasks.Items[taskIndex] as Stag.Model.Task;

            var taskService = new TaskService();
            taskService.Merge(task);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO: Implementar regra de persistencia no TaskService
            var warehouse = new Warehouse<Task>();
            warehouse.Store(_currentTask);
        }
    }
}
