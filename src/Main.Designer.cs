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
            this.lnkOpenVisualStudio = new System.Windows.Forms.LinkLabel();
            this.btnCreateDevelopmentBranch = new System.Windows.Forms.Button();
            this.txtDevelopmentBranch = new System.Windows.Forms.TextBox();
            this.lblDevelopmentBranch = new System.Windows.Forms.Label();
            this.grpGit.SuspendLayout();
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
            this.grpGit.Controls.Add(this.lnkOpenVisualStudio);
            this.grpGit.Controls.Add(this.btnCreateDevelopmentBranch);
            this.grpGit.Controls.Add(this.txtDevelopmentBranch);
            this.grpGit.Controls.Add(this.lblDevelopmentBranch);
            this.grpGit.Location = new System.Drawing.Point(12, 52);
            this.grpGit.Name = "grpGit";
            this.grpGit.Size = new System.Drawing.Size(502, 88);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 151);
            this.Controls.Add(this.grpGit);
            this.Controls.Add(this.cmbTasks);
            this.Controls.Add(this.lblTasks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Stag";
            this.Load += new System.EventHandler(this.Main_Load);
            this.grpGit.ResumeLayout(false);
            this.grpGit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.ComboBox cmbTasks;
        private System.Windows.Forms.GroupBox grpGit;
        private System.Windows.Forms.Label lblDevelopmentBranch;
        private System.Windows.Forms.TextBox txtDevelopmentBranch;
        private System.Windows.Forms.Button btnCreateDevelopmentBranch;
        private System.Windows.Forms.LinkLabel lnkOpenVisualStudio;
    }
}

