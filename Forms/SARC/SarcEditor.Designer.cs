namespace HeavenTool.Forms.SARC
{
    partial class SarcEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SarcEditor));
            darkMenuStrip1 = new Components.DarkMenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            filesTreeView = new System.Windows.Forms.TreeView();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            darkMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // darkMenuStrip1
            // 
            darkMenuStrip1.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            darkMenuStrip1.ForeColor = System.Drawing.Color.White;
            darkMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
            darkMenuStrip1.Location = new System.Drawing.Point(0, 0);
            darkMenuStrip1.Name = "darkMenuStrip1";
            darkMenuStrip1.Size = new System.Drawing.Size(636, 24);
            darkMenuStrip1.TabIndex = 0;
            darkMenuStrip1.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            openToolStripMenuItem.Image = Properties.Resources.open_file;
            openToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            openToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // filesTreeView
            // 
            filesTreeView.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            filesTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            filesTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            filesTreeView.ForeColor = System.Drawing.Color.White;
            filesTreeView.Location = new System.Drawing.Point(0, 24);
            filesTreeView.Name = "filesTreeView";
            filesTreeView.Size = new System.Drawing.Size(233, 373);
            filesTreeView.TabIndex = 1;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            saveAsToolStripMenuItem.Image = Properties.Resources.save;
            saveAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            saveAsToolStripMenuItem.Text = "Save as...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // SarcEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(636, 397);
            Controls.Add(filesTreeView);
            Controls.Add(darkMenuStrip1);
            ForeColor = System.Drawing.Color.White;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = darkMenuStrip1;
            Name = "SarcEditor";
            Text = "SARC Editor";
            darkMenuStrip1.ResumeLayout(false);
            darkMenuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Components.DarkMenuStrip darkMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TreeView filesTreeView;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}