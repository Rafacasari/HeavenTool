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
            this.selectGameDirectoryButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new HeavenTool.Forms.Components.DarkMenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBFEVFLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.optionsGroupBox.SuspendLayout();
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
            this.bcsvEditorButton.Location = new System.Drawing.Point(323, 27);
            this.bcsvEditorButton.Name = "bcsvEditorButton";
            this.bcsvEditorButton.Size = new System.Drawing.Size(206, 28);
            this.bcsvEditorButton.TabIndex = 0;
            this.bcsvEditorButton.Text = "Open BCSV Editor";
            this.bcsvEditorButton.UseVisualStyleBackColor = false;
            this.bcsvEditorButton.Click += new System.EventHandler(this.bcsvEditorButton_Click);
            // 
            // selectGameDirectoryButton
            // 
            this.selectGameDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectGameDirectoryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.selectGameDirectoryButton.FlatAppearance.BorderSize = 0;
            this.selectGameDirectoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectGameDirectoryButton.ForeColor = System.Drawing.Color.White;
            this.selectGameDirectoryButton.Location = new System.Drawing.Point(231, 35);
            this.selectGameDirectoryButton.Name = "selectGameDirectoryButton";
            this.selectGameDirectoryButton.Size = new System.Drawing.Size(68, 20);
            this.selectGameDirectoryButton.TabIndex = 1;
            this.selectGameDirectoryButton.Text = "Select";
            this.selectGameDirectoryButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "RomFs Directory";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(219, 20);
            this.textBox1.TabIndex = 3;
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGroupBox.Controls.Add(this.selectGameDirectoryButton);
            this.optionsGroupBox.Controls.Add(this.textBox1);
            this.optionsGroupBox.Controls.Add(this.label1);
            this.optionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 236);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(305, 69);
            this.optionsGroupBox.TabIndex = 4;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
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
            this.openBCSVToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openBCSVToolStripMenuItem.Text = "Open BCSV...";
            // 
            // openBFEVFLToolStripMenuItem
            // 
            this.openBFEVFLToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openBFEVFLToolStripMenuItem.Name = "openBFEVFLToolStripMenuItem";
            this.openBFEVFLToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openBFEVFLToolStripMenuItem.Text = "Open BFEVFL...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HeavenTool.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // HeavenMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(541, 316);
            this.Controls.Add(this.bcsvEditorButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "HeavenMain";
            this.Text = "HeavenMain";
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bcsvEditorButton;
        private System.Windows.Forms.Button selectGameDirectoryButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private DarkMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem openBCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBFEVFLToolStripMenuItem;
    }
}