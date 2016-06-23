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
            this.grpGit = new System.Windows.Forms.GroupBox();
            this.lnkOpenVisualStudio = new System.Windows.Forms.LinkLabel();
            this.btnCreateDevelopmentBranch = new System.Windows.Forms.Button();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.btnCleanBranches = new System.Windows.Forms.Button();
            this.btnCreateNewEnvironment = new System.Windows.Forms.Button();
            this.grpGit.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGit
            // 
            this.grpGit.Controls.Add(this.lnkOpenVisualStudio);
            this.grpGit.Controls.Add(this.btnCreateDevelopmentBranch);
            this.grpGit.Controls.Add(this.txtBranchName);
            this.grpGit.Controls.Add(this.lblBranchName);
            this.grpGit.Location = new System.Drawing.Point(12, 12);
            this.grpGit.Name = "grpGit";
            this.grpGit.Size = new System.Drawing.Size(400, 88);
            this.grpGit.TabIndex = 3;
            this.grpGit.TabStop = false;
            this.grpGit.Text = "Git";
            // 
            // lnkOpenVisualStudio
            // 
            this.lnkOpenVisualStudio.AutoSize = true;
            this.lnkOpenVisualStudio.Location = new System.Drawing.Point(9, 63);
            this.lnkOpenVisualStudio.Name = "lnkOpenVisualStudio";
            this.lnkOpenVisualStudio.Size = new System.Drawing.Size(107, 13);
            this.lnkOpenVisualStudio.TabIndex = 5;
            this.lnkOpenVisualStudio.TabStop = true;
            this.lnkOpenVisualStudio.Text = "Abrir no Visual Studio";
            this.lnkOpenVisualStudio.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpenVisualStudio_LinkClicked);
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
            // txtBranchName
            // 
            this.txtBranchName.Enabled = false;
            this.txtBranchName.Location = new System.Drawing.Point(9, 36);
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Size = new System.Drawing.Size(301, 20);
            this.txtBranchName.TabIndex = 1;
            // 
            // lblBranchName
            // 
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.Location = new System.Drawing.Point(6, 20);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(86, 13);
            this.lblBranchName.TabIndex = 0;
            this.lblBranchName.Text = "Nome do branch";
            // 
            // btnCleanBranches
            // 
            this.btnCleanBranches.Location = new System.Drawing.Point(12, 106);
            this.btnCleanBranches.Name = "btnCleanBranches";
            this.btnCleanBranches.Size = new System.Drawing.Size(145, 23);
            this.btnCleanBranches.TabIndex = 4;
            this.btnCleanBranches.Text = "Limpar branches...";
            this.btnCleanBranches.UseVisualStyleBackColor = true;
            this.btnCleanBranches.Click += new System.EventHandler(this.btnCleanBranches_Click);
            // 
            // btnCreateNewEnvironment
            // 
            this.btnCreateNewEnvironment.Location = new System.Drawing.Point(163, 106);
            this.btnCreateNewEnvironment.Name = "btnCreateNewEnvironment";
            this.btnCreateNewEnvironment.Size = new System.Drawing.Size(217, 23);
            this.btnCreateNewEnvironment.TabIndex = 5;
            this.btnCreateNewEnvironment.Text = "Criar ambiente de desenvolvimento...";
            this.btnCreateNewEnvironment.UseVisualStyleBackColor = true;
            this.btnCreateNewEnvironment.Click += new System.EventHandler(this.btnCreateNewEnvironment_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 140);
            this.Controls.Add(this.btnCreateNewEnvironment);
            this.Controls.Add(this.btnCleanBranches);
            this.Controls.Add(this.grpGit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Stag";
            this.Load += new System.EventHandler(this.Main_Load);
            this.grpGit.ResumeLayout(false);
            this.grpGit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGit;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.Button btnCreateDevelopmentBranch;
        private System.Windows.Forms.LinkLabel lnkOpenVisualStudio;
        private System.Windows.Forms.Button btnCleanBranches;
        private System.Windows.Forms.Button btnCreateNewEnvironment;
    }
}

