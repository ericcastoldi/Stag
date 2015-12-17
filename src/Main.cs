using Stag.Configuration;
using Stag.Service;
using Stag.Tasks;
using System;
using System.Windows.Forms;

namespace Stag
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.LoadTasks();
        }

        private void LoadTasks()
        {
            var taskProvider = new TaskProvider();
            var tasks = taskProvider.GetTasks();

            foreach (var task in tasks)
                cmbTasks.Items.Add(task);
        }

        private void cmbTasks_SelectedValueChanged(object sender, EventArgs e)
        {
            var task = this.GetSelectedTask();

            this.LoadTask(task);
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
            txtDevelopmentBranch.Enabled = true;
        }

        private void btnCreateDevelopmentBranch_Click(object sender, EventArgs e)
        {
            var branchName = txtDevelopmentBranch.Text;
            var task = this.GetSelectedTask();

            var taskService = new TaskService();
            var result = taskService.CreateTaskBranch(task, branchName);

            ShowResult(result, "Criação de branch...");
        }

        private static void ShowResult(ServiceResult result, string boxTitle)
        {
            var icon = result.Success ?
                MessageBoxIcon.Information :
                MessageBoxIcon.Error;

            MessageBox.Show(result.Description, boxTitle, MessageBoxButtons.OK, icon);
        }

        private void btnCreateMergeBranch_Click(object sender, EventArgs e)
        {
            var taskIndex = cmbTasks.SelectedIndex;
            var task = cmbTasks.Items[taskIndex] as Stag.Tasks.Task;

            var taskService = new TaskService();
            taskService.Merge(task);
        }

        private void lnkOpenVisualStudio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var settings = new Settings();

            System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\IDE\\devenv.exe", settings.Workspace + "\\SapiensNfe.sln");
        }
    }
}