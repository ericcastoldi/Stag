using Stag.Service;
using Stag.Storage;
using Stag.Tasks;
using System;
using System.IO.Abstractions;
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
            var task = cmbTasks.Items[taskIndex] as Stag.Tasks.Task;

            var taskService = new TaskService();
            taskService.StartWork(task);
        }

        private void btnCreateMergeBranch_Click(object sender, EventArgs e)
        {
            var taskIndex = cmbTasks.SelectedIndex;
            var task = cmbTasks.Items[taskIndex] as Stag.Tasks.Task;

            var taskService = new TaskService();
            taskService.Merge(task);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO: Implementar regra de persistencia no TaskService
            var warehouse = new Warehouse<Task>();
            warehouse.Store(_currentTask);
        }

        private void btnGenerateXsd_Click(object sender, EventArgs e)
        {
            btnGenerateXsd.Enabled = false;

            var xsdService = new XsdWrapperService();
            var messages = xsdService.GenerateClasses(txtXsdDirectory.Text, txtXsdNamespace.Text, ckbTryRecoverDsigErrors.Checked);

            var output = string.Join("\r\n", messages);

            MessageBox.Show(string.IsNullOrWhiteSpace(output) ? "Classes geradas com sucesso!" : output);

            TryEnableBtnGenerateXsd();
        }

        private void txtXsdDirectory_Leave(object sender, EventArgs e)
        {
            TryEnableBtnGenerateXsd();
        }

        private void txtXsdNamespace_Leave(object sender, EventArgs e)
        {
            TryEnableBtnGenerateXsd();
        }

        private void TryEnableBtnGenerateXsd()
        {
            var fileSystem = new FileSystem();
            bool validXsdDirectory = !string.IsNullOrWhiteSpace(txtXsdDirectory.Text) && fileSystem.Directory.Exists(txtXsdDirectory.Text);

            btnGenerateXsd.Enabled = validXsdDirectory && (!string.IsNullOrWhiteSpace(txtXsdNamespace.Text));
        }
    }
}