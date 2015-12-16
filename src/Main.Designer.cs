namespace Stag
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lblTasks = new System.Windows.Forms.Label();
            this.cmbTasks = new System.Windows.Forms.ComboBox();
            this.grpGit = new System.Windows.Forms.GroupBox();
            this.btnCreateMergeBranch = new System.Windows.Forms.Button();
            this.btnCreateDevelopmentBranch = new System.Windows.Forms.Button();
            this.txtMergeBranch = new System.Windows.Forms.TextBox();
            this.lblMergeBranch = new System.Windows.Forms.Label();
            this.txtDevelopmentBranch = new System.Windows.Forms.TextBox();
            this.lblDevelopmentBranch = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpXsdExe = new System.Windows.Forms.GroupBox();
            this.lblXsdDirectory = new System.Windows.Forms.Label();
            this.lblXsdNamespace = new System.Windows.Forms.Label();
            this.txtXsdDirectory = new System.Windows.Forms.TextBox();
            this.txtXsdNamespace = new System.Windows.Forms.TextBox();
            this.btnGenerateXsd = new System.Windows.Forms.Button();
            this.ckbTryRecoverDsigErrors = new System.Windows.Forms.CheckBox();
            this.grpGit.SuspendLayout();
            this.grpXsdExe.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(12, 15);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(43, 13);
            this.lblTasks.TabIndex = 0;
            this.lblTasks.Text = "Tarefas";
            // 
            // cmbTasks
            // 
            this.cmbTasks.FormattingEnabled = true;
            this.cmbTasks.Location = new System.Drawing.Point(61, 12);
            this.cmbTasks.Name = "cmbTasks";
            this.cmbTasks.Size = new System.Drawing.Size(453, 21);
            this.cmbTasks.TabIndex = 1;
            this.cmbTasks.SelectedValueChanged += new System.EventHandler(this.cmbTasks_SelectedValueChanged);
            // 
            // grpGit
            // 
            this.grpGit.Controls.Add(this.btnCreateMergeBranch);
            this.grpGit.Controls.Add(this.btnCreateDevelopmentBranch);
            this.grpGit.Controls.Add(this.txtMergeBranch);
            this.grpGit.Controls.Add(this.lblMergeBranch);
            this.grpGit.Controls.Add(this.txtDevelopmentBranch);
            this.grpGit.Controls.Add(this.lblDevelopmentBranch);
            this.grpGit.Location = new System.Drawing.Point(12, 52);
            this.grpGit.Name = "grpGit";
            this.grpGit.Size = new System.Drawing.Size(502, 117);
            this.grpGit.TabIndex = 3;
            this.grpGit.TabStop = false;
            this.grpGit.Text = "Git";
            // 
            // btnCreateMergeBranch
            // 
            this.btnCreateMergeBranch.Location = new System.Drawing.Point(317, 76);
            this.btnCreateMergeBranch.Name = "btnCreateMergeBranch";
            this.btnCreateMergeBranch.Size = new System.Drawing.Size(75, 23);
            this.btnCreateMergeBranch.TabIndex = 5;
            this.btnCreateMergeBranch.Text = "Criar";
            this.btnCreateMergeBranch.UseVisualStyleBackColor = true;
            this.btnCreateMergeBranch.Click += new System.EventHandler(this.btnCreateMergeBranch_Click);
            // 
            // btnCreateDevelopmentBranch
            // 
            this.btnCreateDevelopmentBranch.Location = new System.Drawing.Point(317, 32);
            this.btnCreateDevelopmentBranch.Name = "btnCreateDevelopmentBranch";
            this.btnCreateDevelopmentBranch.Size = new System.Drawing.Size(75, 23);
            this.btnCreateDevelopmentBranch.TabIndex = 4;
            this.btnCreateDevelopmentBranch.Text = "Criar";
            this.btnCreateDevelopmentBranch.UseVisualStyleBackColor = true;
            this.btnCreateDevelopmentBranch.Click += new System.EventHandler(this.btnCreateDevelopmentBranch_Click);
            // 
            // txtMergeBranch
            // 
            this.txtMergeBranch.Enabled = false;
            this.txtMergeBranch.Location = new System.Drawing.Point(10, 80);
            this.txtMergeBranch.Name = "txtMergeBranch";
            this.txtMergeBranch.Size = new System.Drawing.Size(300, 20);
            this.txtMergeBranch.TabIndex = 3;
            // 
            // lblMergeBranch
            // 
            this.lblMergeBranch.AutoSize = true;
            this.lblMergeBranch.Location = new System.Drawing.Point(7, 63);
            this.lblMergeBranch.Name = "lblMergeBranch";
            this.lblMergeBranch.Size = new System.Drawing.Size(89, 13);
            this.lblMergeBranch.TabIndex = 2;
            this.lblMergeBranch.Text = "Branch de Merge";
            // 
            // txtDevelopmentBranch
            // 
            this.txtDevelopmentBranch.Enabled = false;
            this.txtDevelopmentBranch.Location = new System.Drawing.Point(9, 36);
            this.txtDevelopmentBranch.Name = "txtDevelopmentBranch";
            this.txtDevelopmentBranch.Size = new System.Drawing.Size(301, 20);
            this.txtDevelopmentBranch.TabIndex = 1;
            // 
            // lblDevelopmentBranch
            // 
            this.lblDevelopmentBranch.AutoSize = true;
            this.lblDevelopmentBranch.Location = new System.Drawing.Point(6, 20);
            this.lblDevelopmentBranch.Name = "lblDevelopmentBranch";
            this.lblDevelopmentBranch.Size = new System.Drawing.Size(139, 13);
            this.lblDevelopmentBranch.TabIndex = 0;
            this.lblDevelopmentBranch.Text = "Branch de desenvolvimento";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(439, 328);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpXsdExe
            // 
            this.grpXsdExe.Controls.Add(this.ckbTryRecoverDsigErrors);
            this.grpXsdExe.Controls.Add(this.btnGenerateXsd);
            this.grpXsdExe.Controls.Add(this.txtXsdNamespace);
            this.grpXsdExe.Controls.Add(this.txtXsdDirectory);
            this.grpXsdExe.Controls.Add(this.lblXsdNamespace);
            this.grpXsdExe.Controls.Add(this.lblXsdDirectory);
            this.grpXsdExe.Location = new System.Drawing.Point(12, 176);
            this.grpXsdExe.Name = "grpXsdExe";
            this.grpXsdExe.Size = new System.Drawing.Size(502, 146);
            this.grpXsdExe.TabIndex = 5;
            this.grpXsdExe.TabStop = false;
            this.grpXsdExe.Text = "Geração de XSD";
            // 
            // lblXsdDirectory
            // 
            this.lblXsdDirectory.AutoSize = true;
            this.lblXsdDirectory.Location = new System.Drawing.Point(7, 23);
            this.lblXsdDirectory.Name = "lblXsdDirectory";
            this.lblXsdDirectory.Size = new System.Drawing.Size(34, 13);
            this.lblXsdDirectory.TabIndex = 0;
            this.lblXsdDirectory.Text = "Pasta";
            // 
            // lblXsdNamespace
            // 
            this.lblXsdNamespace.AutoSize = true;
            this.lblXsdNamespace.Location = new System.Drawing.Point(6, 56);
            this.lblXsdNamespace.Name = "lblXsdNamespace";
            this.lblXsdNamespace.Size = new System.Drawing.Size(64, 13);
            this.lblXsdNamespace.TabIndex = 1;
            this.lblXsdNamespace.Text = "Namespace";
            // 
            // txtXsdDirectory
            // 
            this.txtXsdDirectory.Location = new System.Drawing.Point(76, 20);
            this.txtXsdDirectory.Name = "txtXsdDirectory";
            this.txtXsdDirectory.Size = new System.Drawing.Size(406, 20);
            this.txtXsdDirectory.TabIndex = 2;
            this.txtXsdDirectory.Leave += new System.EventHandler(this.txtXsdDirectory_Leave);
            // 
            // txtXsdNamespace
            // 
            this.txtXsdNamespace.Location = new System.Drawing.Point(76, 53);
            this.txtXsdNamespace.Name = "txtXsdNamespace";
            this.txtXsdNamespace.Size = new System.Drawing.Size(406, 20);
            this.txtXsdNamespace.TabIndex = 3;
            this.txtXsdNamespace.Leave += new System.EventHandler(this.txtXsdNamespace_Leave);
            // 
            // btnGenerateXsd
            // 
            this.btnGenerateXsd.Enabled = false;
            this.btnGenerateXsd.Location = new System.Drawing.Point(389, 117);
            this.btnGenerateXsd.Name = "btnGenerateXsd";
            this.btnGenerateXsd.Size = new System.Drawing.Size(93, 23);
            this.btnGenerateXsd.TabIndex = 4;
            this.btnGenerateXsd.Text = "Gerar classes";
            this.btnGenerateXsd.UseVisualStyleBackColor = true;
            this.btnGenerateXsd.Click += new System.EventHandler(this.btnGenerateXsd_Click);
            // 
            // ckbTryRecoverDsigErrors
            // 
            this.ckbTryRecoverDsigErrors.AutoSize = true;
            this.ckbTryRecoverDsigErrors.Checked = true;
            this.ckbTryRecoverDsigErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTryRecoverDsigErrors.Location = new System.Drawing.Point(7, 82);
            this.ckbTryRecoverDsigErrors.Name = "ckbTryRecoverDsigErrors";
            this.ckbTryRecoverDsigErrors.Size = new System.Drawing.Size(475, 17);
            this.ckbTryRecoverDsigErrors.TabIndex = 5;
            this.ckbTryRecoverDsigErrors.Text = "Tentar gerar novamente schemas que retornarem erro por falta do schema de assinat" +
    "ura digital.";
            this.ckbTryRecoverDsigErrors.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 361);
            this.Controls.Add(this.grpXsdExe);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpGit);
            this.Controls.Add(this.cmbTasks);
            this.Controls.Add(this.lblTasks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Stag";
            this.Load += new System.EventHandler(this.Main_Load);
            this.grpGit.ResumeLayout(false);
            this.grpGit.PerformLayout();
            this.grpXsdExe.ResumeLayout(false);
            this.grpXsdExe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.ComboBox cmbTasks;
        private System.Windows.Forms.GroupBox grpGit;
        private System.Windows.Forms.Label lblDevelopmentBranch;
        private System.Windows.Forms.TextBox txtDevelopmentBranch;
        private System.Windows.Forms.TextBox txtMergeBranch;
        private System.Windows.Forms.Label lblMergeBranch;
        private System.Windows.Forms.Button btnCreateMergeBranch;
        private System.Windows.Forms.Button btnCreateDevelopmentBranch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpXsdExe;
        private System.Windows.Forms.Button btnGenerateXsd;
        private System.Windows.Forms.TextBox txtXsdNamespace;
        private System.Windows.Forms.TextBox txtXsdDirectory;
        private System.Windows.Forms.Label lblXsdNamespace;
        private System.Windows.Forms.Label lblXsdDirectory;
        private System.Windows.Forms.CheckBox ckbTryRecoverDsigErrors;
    }
}

