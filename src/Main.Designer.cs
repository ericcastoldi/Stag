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
            this.btnCleanBranches = new System.Windows.Forms.Button();
            this.btnCreateNewEnvironment = new System.Windows.Forms.Button();
            this.lblCurrentWorkspace = new System.Windows.Forms.Label();
            this.txtWorkspace = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCleanBranches
            // 
            this.btnCleanBranches.Location = new System.Drawing.Point(16, 56);
            this.btnCleanBranches.Name = "btnCleanBranches";
            this.btnCleanBranches.Size = new System.Drawing.Size(145, 23);
            this.btnCleanBranches.TabIndex = 4;
            this.btnCleanBranches.Text = "Limpar branches...";
            this.btnCleanBranches.UseVisualStyleBackColor = true;
            this.btnCleanBranches.Click += new System.EventHandler(this.btnCleanBranches_Click);
            // 
            // btnCreateNewEnvironment
            // 
            this.btnCreateNewEnvironment.Location = new System.Drawing.Point(167, 56);
            this.btnCreateNewEnvironment.Name = "btnCreateNewEnvironment";
            this.btnCreateNewEnvironment.Size = new System.Drawing.Size(217, 23);
            this.btnCreateNewEnvironment.TabIndex = 5;
            this.btnCreateNewEnvironment.Text = "Criar ambiente de desenvolvimento...";
            this.btnCreateNewEnvironment.UseVisualStyleBackColor = true;
            this.btnCreateNewEnvironment.Click += new System.EventHandler(this.btnCreateNewEnvironment_Click);
            // 
            // lblCurrentWorkspace
            // 
            this.lblCurrentWorkspace.AutoSize = true;
            this.lblCurrentWorkspace.Location = new System.Drawing.Point(13, 13);
            this.lblCurrentWorkspace.Name = "lblCurrentWorkspace";
            this.lblCurrentWorkspace.Size = new System.Drawing.Size(65, 13);
            this.lblCurrentWorkspace.TabIndex = 6;
            this.lblCurrentWorkspace.Text = "Workspace:";
            // 
            // txtWorkspace
            // 
            this.txtWorkspace.Location = new System.Drawing.Point(16, 30);
            this.txtWorkspace.Name = "txtWorkspace";
            this.txtWorkspace.Size = new System.Drawing.Size(368, 20);
            this.txtWorkspace.TabIndex = 7;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 91);
            this.Controls.Add(this.txtWorkspace);
            this.Controls.Add(this.lblCurrentWorkspace);
            this.Controls.Add(this.btnCreateNewEnvironment);
            this.Controls.Add(this.btnCleanBranches);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Stag";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCleanBranches;
        private System.Windows.Forms.Button btnCreateNewEnvironment;
        private System.Windows.Forms.Label lblCurrentWorkspace;
        private System.Windows.Forms.TextBox txtWorkspace;
    }
}

