namespace Stag
{
    partial class CleanBranches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CleanBranches));
            this.lstBranches = new System.Windows.Forms.CheckedListBox();
            this.btnCleanBranches = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstBranches
            // 
            this.lstBranches.FormattingEnabled = true;
            this.lstBranches.Location = new System.Drawing.Point(13, 13);
            this.lstBranches.Name = "lstBranches";
            this.lstBranches.Size = new System.Drawing.Size(259, 199);
            this.lstBranches.TabIndex = 0;
            // 
            // btnCleanBranches
            // 
            this.btnCleanBranches.AccessibleDescription = "Exclui os branches que não estão mais sendo utilizados.";
            this.btnCleanBranches.Location = new System.Drawing.Point(197, 227);
            this.btnCleanBranches.Name = "btnCleanBranches";
            this.btnCleanBranches.Size = new System.Drawing.Size(75, 23);
            this.btnCleanBranches.TabIndex = 1;
            this.btnCleanBranches.Text = "Limpar branches";
            this.btnCleanBranches.UseVisualStyleBackColor = true;
            this.btnCleanBranches.Click += new System.EventHandler(this.btnCleanBranches_Click);
            // 
            // CleanBranches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnCleanBranches);
            this.Controls.Add(this.lstBranches);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CleanBranches";
            this.Text = "Limpar branches...";
            this.Load += new System.EventHandler(this.CleanBranches_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstBranches;
        private System.Windows.Forms.Button btnCleanBranches;
    }
}