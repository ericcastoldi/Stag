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
            var dialogResult = MessageBox.Show("Para realizar a criação do ambiente de desenvolvimento o Stag descartará todas as alterações pendentes no repositório git. Deseja continuar?", "Criação de ambiente de desenvolvimento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var createEnvironmentFeedback = new CreateEnvironmentFeedback(txtVersion.Text, txtVersionBranchName.Text, txtTaskBranchName.Text);
                createEnvironmentFeedback.ShowDialog(this);
                this.Close();
            }
        }
    }
}