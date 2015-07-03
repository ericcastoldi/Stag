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
            this.cmbTasks.Size = new System.Drawing.Size(361, 21);
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
            this.grpGit.Size = new System.Drawing.Size(410, 245);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 773);
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
        private System.Windows.Forms.TextBox txtMergeBranch;
        private System.Windows.Forms.Label lblMergeBranch;
        private System.Windows.Forms.Button btnCreateMergeBranch;
        private System.Windows.Forms.Button btnCreateDevelopmentBranch;
    }
}

