using Stag.Configuration;
using Stag.Service;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Stag
{
    public partial class CreateEnvironmentFeedback : Form
    {
        private readonly string _version;
        private readonly string _branch;
        private readonly string _taskBranchName;
        private readonly ISettings _settings;

        public CreateEnvironmentFeedback()
        {
            InitializeComponent();
        }

        internal CreateEnvironmentFeedback(ISettings settings, string version, string branch, string taskBranchName)
            : this()
        {
            _branch = branch;
            _version = version;
            _taskBranchName = taskBranchName;
            _settings = settings;
        }

        private void AppendLineToTextBox(string text)
        {
            var formattedText = string.Format("{0}{1}", text, "\r\n");

            if (txtStatus.InvokeRequired)
            {
                txtStatus.Invoke((Action)delegate
                {
                    txtStatus.AppendText(formattedText);
                });
            }
            else
            {
                txtStatus.AppendText(formattedText);
            }
        }

        private void CreateEnvironmentFeedback_Shown(object sender, System.EventArgs e)
        {
            var environmentCreationService = new DevelopmentEnvironmentCreationService(_settings, _version, _branch, _taskBranchName);

            environmentCreationService.Done += DoneCreating;
            environmentCreationService.MessageEmited += AppendLineToTextBox;

            var thread = new Thread(environmentCreationService.CreateEnvironment);
            thread.Start();
        }

        private void DoneCreating(ServiceResult result)
        {
            if (result.Success)
            {
                MessageBox.Show(result.Description);
            }
            else
            {
                MessageBox.Show(result.Description, "Erro ao criar ambiente de desenvolvimento.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (this.InvokeRequired)
            {
                this.Invoke((Action)delegate
                {
                    this.Close();
                });
            }
            else
            {
                this.Close();
            }
        }
    }
}