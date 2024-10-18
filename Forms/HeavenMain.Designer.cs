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
            bcsvEditorButton = new System.Windows.Forms.Button();
            menuStrip1 = new DarkMenuStrip();
            optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            rstbEditorButton = new System.Windows.Forms.Button();
            sarcEditorButton = new System.Windows.Forms.Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // bcsvEditorButton
            // 
            bcsvEditorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bcsvEditorButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            bcsvEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            bcsvEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bcsvEditorButton.ForeColor = System.Drawing.Color.White;
            bcsvEditorButton.Location = new System.Drawing.Point(377, 44);
            bcsvEditorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bcsvEditorButton.Name = "bcsvEditorButton";
            bcsvEditorButton.Size = new System.Drawing.Size(240, 32);
            bcsvEditorButton.TabIndex = 0;
            bcsvEditorButton.Text = "Open BCSV Editor";
            bcsvEditorButton.UseVisualStyleBackColor = false;
            bcsvEditorButton.Click += bcsvEditorButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.Color.Transparent;
            menuStrip1.ForeColor = System.Drawing.Color.White;
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(631, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new System.Drawing.Point(14, 44);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(356, 234);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // rstbEditorButton
            // 
            rstbEditorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rstbEditorButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            rstbEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            rstbEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            rstbEditorButton.ForeColor = System.Drawing.Color.White;
            rstbEditorButton.Location = new System.Drawing.Point(377, 83);
            rstbEditorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rstbEditorButton.Name = "rstbEditorButton";
            rstbEditorButton.Size = new System.Drawing.Size(240, 32);
            rstbEditorButton.TabIndex = 8;
            rstbEditorButton.Text = "Open RSTB Editor";
            rstbEditorButton.UseVisualStyleBackColor = false;
            rstbEditorButton.Click += rstbEditorButton_Click;
            // 
            // sarcEditorButton
            // 
            sarcEditorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sarcEditorButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            sarcEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            sarcEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            sarcEditorButton.ForeColor = System.Drawing.Color.White;
            sarcEditorButton.Location = new System.Drawing.Point(377, 121);
            sarcEditorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            sarcEditorButton.Name = "sarcEditorButton";
            sarcEditorButton.Size = new System.Drawing.Size(240, 32);
            sarcEditorButton.TabIndex = 9;
            sarcEditorButton.Text = "Open SARC Editor";
            sarcEditorButton.UseVisualStyleBackColor = false;
            sarcEditorButton.Click += sarcEditorButton_Click;
            // 
            // HeavenMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(631, 293);
            Controls.Add(sarcEditorButton);
            Controls.Add(rstbEditorButton);
            Controls.Add(bcsvEditorButton);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "HeavenMain";
            Text = "ACNH Heaven Tool";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button bcsvEditorButton;
        private DarkMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button rstbEditorButton;
        private System.Windows.Forms.Button sarcEditorButton;
    }
}