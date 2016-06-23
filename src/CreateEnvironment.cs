using Stag.Configuration;
using Stag.SourceControl;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Stag
{
    public partial class CreateEnvironment : Form
    {
        public CreateEnvironment()
        {
            InitializeComponent();
        }

        private void btnCreateEnvironment_Click(object sender, EventArgs e)
        {
            var createEnvironmentFeedback = new CreateEnvironmentFeedback(txtVersion.Text, txtVersionBranchName.Text, txtTaskBranchName.Text);
            createEnvironmentFeedback.ShowDialog(this);
            this.Close();
        }
    }
}