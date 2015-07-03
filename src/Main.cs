using Stag.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var taskService = new TaskService();

            var tasks = taskService.MyTasks();
            cmbTasks.Items.AddRange(tasks.ToArray());
        }

        private void cmbTasks_SelectedValueChanged(object sender, EventArgs e)
        {
            var taskIndex = ((ComboBox)sender).SelectedIndex;
            var task = ((ComboBox)sender).Items[taskIndex] as Stag.Model.Task;

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
    }
}
