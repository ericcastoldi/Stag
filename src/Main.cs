using Stag.Configuration;
using Stag.Service;
using Stag.SourceControl;
using System;
using System.Windows.Forms;

namespace Stag
{
    public partial class Main : Form
    {
        private readonly string _workspace;
        private readonly ISettings _settings;

        public Main(string workspace)
            : this()
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "config.json");

            _workspace = workspace;
            _settings = new Settings(path);

            if (!string.IsNullOrWhiteSpace(_workspace))
            {
                _settings.Workspace = _workspace;
            }
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.UpdateTitle();
            this.txtWorkspace.Text = _settings.Workspace;
        }

        private void UpdateTitle()
        {
            var git = new Git(_settings);
            this.Text = "Stag - " + git.GetCurrentBranch().Name;
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
            System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\IDE\\devenv.exe", _settings.Workspace + "\\SapiensNfe.sln");
        }

        private void btnCleanBranches_Click(object sender, EventArgs e)
        {
            var cleanBranches = new CleanBranches(_settings);
            cleanBranches.Show();
        }

        private void btnCreateNewEnvironment_Click(object sender, EventArgs e)
        {
            var environment = new CreateEnvironment(_settings);
            environment.ShowDialog(this);
            UpdateTitle();
        }
    }
}