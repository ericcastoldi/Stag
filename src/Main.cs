using Stag.Configuration;
using Stag.Service;
using Stag.SourceControl;
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
            this.UpdateTitle();
        }

        private void UpdateTitle()
        {
            var git = new Git();
            this.Text = "Stag - " + git.GetCurrentBranch().Name;
        }

        private void btnCreateDevelopmentBranch_Click(object sender, EventArgs e)
        {
            var branchName = txtBranchName.Text;

            if (string.IsNullOrWhiteSpace(branchName))
            {
                MessageBox.Show("Defina um nome para o branch.", "Criação de branch...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var git = new Git();
            git.CreateBranch(branchName);

            MessageBox.Show(string.Format("Branch '{0}' criado com sucesso!", branchName), "Criação de branch...", MessageBoxButtons.OK, MessageBoxIcon.None);

            this.UpdateTitle();
        }

        private static void ShowResult(ServiceResult result, string boxTitle)
        {
            var icon = result.Success ?
                MessageBoxIcon.Information :
                MessageBoxIcon.Error;

            MessageBox.Show(result.Description, boxTitle, MessageBoxButtons.OK, icon);
        }

        private void lnkOpenVisualStudio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var settings = new Settings();

            System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\IDE\\devenv.exe", settings.Workspace + "\\SapiensNfe.sln");
        }

        private void btnCleanBranches_Click(object sender, EventArgs e)
        {
            var cleanBranches = new CleanBranches();
            cleanBranches.Show();
        }

        private void btnCreateNewEnvironment_Click(object sender, EventArgs e)
        {
            var environment = new CreateEnvironment();
            environment.ShowDialog(this);
            UpdateTitle();
        }
    }
}