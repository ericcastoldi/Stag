namespace Stag
{
    partial class CreateEnvironment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateEnvironment));
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersionBranchName = new System.Windows.Forms.Label();
            this.txtVersionBranchName = new System.Windows.Forms.TextBox();
            this.btnCreateEnvironment = new System.Windows.Forms.Button();
            this.lblTaskBranchName = new System.Windows.Forms.Label();
            this.txtTaskBranchName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 13);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(40, 13);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Versão";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(16, 29);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(301, 20);
            this.txtVersion.TabIndex = 1;
            // 
            // lblVersionBranchName
            // 
            this.lblVersionBranchName.AutoSize = true;
            this.lblVersionBranchName.Location = new System.Drawing.Point(13, 52);
            this.lblVersionBranchName.Name = "lblVersionBranchName";
            this.lblVersionBranchName.Size = new System.Drawing.Size(92, 13);
            this.lblVersionBranchName.TabIndex = 2;
            this.lblVersionBranchName.Text = "Branch da Versão";
            // 
            // txtVersionBranchName
            // 
            this.txtVersionBranchName.Location = new System.Drawing.Point(16, 69);
            this.txtVersionBranchName.Name = "txtVersionBranchName";
            this.txtVersionBranchName.Size = new System.Drawing.Size(301, 20);
            this.txtVersionBranchName.TabIndex = 3;
            
            // 
            // lblTaskBranchName
            // 
            this.lblTaskBranchName.AutoSize = true;
            this.lblTaskBranchName.Location = new System.Drawing.Point(16, 96);
            this.lblTaskBranchName.Name = "lblTaskBranchName";
            this.lblTaskBranchName.Size = new System.Drawing.Size(90, 13);
            this.lblTaskBranchName.TabIndex = 4;
            this.lblTaskBranchName.Text = "Branch da Tarefa";
            // 
            // txtTaskBranchName
            // 
            this.txtTaskBranchName.Location = new System.Drawing.Point(19, 113);
            this.txtTaskBranchName.Name = "txtTaskBranchName";
            this.txtTaskBranchName.Size = new System.Drawing.Size(298, 20);
            this.txtTaskBranchName.TabIndex = 5;
            // 
            // btnCreateEnvironment
            // 
            this.btnCreateEnvironment.Location = new System.Drawing.Point(12, 156);
            this.btnCreateEnvironment.Name = "btnCreateEnvironment";
            this.btnCreateEnvironment.Size = new System.Drawing.Size(305, 23);
            this.btnCreateEnvironment.TabIndex = 6;
            this.btnCreateEnvironment.Text = "Criar Ambiente de Desenvolvimento";
            this.btnCreateEnvironment.UseVisualStyleBackColor = true;
            this.btnCreateEnvironment.Click += new System.EventHandler(this.btnCreateEnvironment_Click);
            // 
            // CreateEnvironment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 191);
            this.Controls.Add(this.txtTaskBranchName);
            this.Controls.Add(this.lblTaskBranchName);
            this.Controls.Add(this.btnCreateEnvironment);
            this.Controls.Add(this.txtVersionBranchName);
            this.Controls.Add(this.lblVersionBranchName);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.lblVersion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateEnvironment";
            this.Text = "Criar ambiente de desenvolvimento";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersionBranchName;
        private System.Windows.Forms.TextBox txtVersionBranchName;
        private System.Windows.Forms.Button btnCreateEnvironment;
        private System.Windows.Forms.Label lblTaskBranchName;
        private System.Windows.Forms.TextBox txtTaskBranchName;
    }
}