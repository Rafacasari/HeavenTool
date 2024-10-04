using HeavenTool.Forms.Components;

namespace HeavenTool
{
    partial class HeavenMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeavenMain));
            this.bcsvEditorButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new HeavenTool.Forms.Components.DarkMenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBFEVFLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rstbEditorButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bcsvEditorButton
            // 
            this.bcsvEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bcsvEditorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bcsvEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.bcsvEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bcsvEditorButton.ForeColor = System.Drawing.Color.White;
            this.bcsvEditorButton.Location = new System.Drawing.Point(323, 38);
            this.bcsvEditorButton.Name = "bcsvEditorButton";
            this.bcsvEditorButton.Size = new System.Drawing.Size(206, 28);
            this.bcsvEditorButton.TabIndex = 0;
            this.bcsvEditorButton.Text = "Open BCSV Editor";
            this.bcsvEditorButton.UseVisualStyleBackColor = false;
            this.bcsvEditorButton.Click += new System.EventHandler(this.bcsvEditorButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(541, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBCSVToolStripMenuItem,
            this.openBFEVFLToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openBCSVToolStripMenuItem
            // 
            this.openBCSVToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openBCSVToolStripMenuItem.Name = "openBCSVToolStripMenuItem";
            this.openBCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openBCSVToolStripMenuItem.Text = "Open BCSV...";
            // 
            // openBFEVFLToolStripMenuItem
            // 
            this.openBFEVFLToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openBFEVFLToolStripMenuItem.Name = "openBFEVFLToolStripMenuItem";
            this.openBFEVFLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openBFEVFLToolStripMenuItem.Text = "Open BFEVFL...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HeavenTool.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // rstbEditorButton
            // 
            this.rstbEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rstbEditorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rstbEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.rstbEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rstbEditorButton.ForeColor = System.Drawing.Color.White;
            this.rstbEditorButton.Location = new System.Drawing.Point(323, 72);
            this.rstbEditorButton.Name = "rstbEditorButton";
            this.rstbEditorButton.Size = new System.Drawing.Size(206, 28);
            this.rstbEditorButton.TabIndex = 8;
            this.rstbEditorButton.Text = "Open RSTB Editor";
            this.rstbEditorButton.UseVisualStyleBackColor = false;
            this.rstbEditorButton.Click += new System.EventHandler(this.rstbEditorButton_Click);
            // 
            // HeavenMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(541, 254);
            this.Controls.Add(this.rstbEditorButton);
            this.Controls.Add(this.bcsvEditorButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "HeavenMain";
            this.Text = "ACNH Heaven Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bcsvEditorButton;
        private DarkMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem openBCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBFEVFLToolStripMenuItem;
        private System.Windows.Forms.Button rstbEditorButton;
    }
}