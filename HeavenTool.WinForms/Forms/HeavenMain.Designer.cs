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
            topMenu = new DarkMenuStrip();
            toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            compressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            yaz0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            yaz0DecompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            devToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            bCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportEnumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportUsedHashesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            rstbEditorButton = new System.Windows.Forms.Button();
            sarcEditorButton = new System.Windows.Forms.Button();
            itemParamHelperButton = new System.Windows.Forms.Button();
            topMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // bcsvEditorButton
            // 
            bcsvEditorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bcsvEditorButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            bcsvEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            bcsvEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bcsvEditorButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            bcsvEditorButton.ForeColor = System.Drawing.Color.White;
            bcsvEditorButton.Location = new System.Drawing.Point(377, 44);
            bcsvEditorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bcsvEditorButton.Name = "bcsvEditorButton";
            bcsvEditorButton.Size = new System.Drawing.Size(254, 41);
            bcsvEditorButton.TabIndex = 0;
            bcsvEditorButton.Text = "Open BCSV Editor";
            bcsvEditorButton.UseVisualStyleBackColor = false;
            bcsvEditorButton.Click += BcsvEditorButton_Click;
            // 
            // topMenu
            // 
            topMenu.BackColor = System.Drawing.Color.FromArgb(42, 42, 42);
            topMenu.ForeColor = System.Drawing.Color.White;
            topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolsToolStripMenuItem, devToolsToolStripMenuItem });
            topMenu.Location = new System.Drawing.Point(0, 0);
            topMenu.Name = "topMenu";
            topMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            topMenu.Size = new System.Drawing.Size(645, 24);
            topMenu.TabIndex = 6;
            topMenu.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { compressionToolStripMenuItem });
            toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // compressionToolStripMenuItem
            // 
            compressionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { yaz0ToolStripMenuItem });
            compressionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            compressionToolStripMenuItem.Name = "compressionToolStripMenuItem";
            compressionToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            compressionToolStripMenuItem.Text = "Compression";
            // 
            // yaz0ToolStripMenuItem
            // 
            yaz0ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { yaz0DecompressToolStripMenuItem });
            yaz0ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            yaz0ToolStripMenuItem.Name = "yaz0ToolStripMenuItem";
            yaz0ToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            yaz0ToolStripMenuItem.Text = "Yaz0";
            // 
            // yaz0DecompressToolStripMenuItem
            // 
            yaz0DecompressToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            yaz0DecompressToolStripMenuItem.Name = "yaz0DecompressToolStripMenuItem";
            yaz0DecompressToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            yaz0DecompressToolStripMenuItem.Text = "Decompress";
            yaz0DecompressToolStripMenuItem.Click += yaz0DecompressToolStripMenuItem_Click;
            // 
            // devToolsToolStripMenuItem
            // 
            devToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { bCSVToolStripMenuItem });
            devToolsToolStripMenuItem.Name = "devToolsToolStripMenuItem";
            devToolsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            devToolsToolStripMenuItem.Text = "Dev Tools";
            // 
            // bCSVToolStripMenuItem
            // 
            bCSVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportLabelsToolStripMenuItem, exportEnumsToolStripMenuItem, exportUsedHashesToolStripMenuItem });
            bCSVToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            bCSVToolStripMenuItem.Name = "bCSVToolStripMenuItem";
            bCSVToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            bCSVToolStripMenuItem.Text = "BCSV";
            // 
            // exportLabelsToolStripMenuItem
            // 
            exportLabelsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportLabelsToolStripMenuItem.Name = "exportLabelsToolStripMenuItem";
            exportLabelsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            exportLabelsToolStripMenuItem.Text = "Export Labels";
            exportLabelsToolStripMenuItem.Click += ExportLabelsToolStripMenuItem_Click;
            // 
            // exportEnumsToolStripMenuItem
            // 
            exportEnumsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportEnumsToolStripMenuItem.Name = "exportEnumsToolStripMenuItem";
            exportEnumsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            exportEnumsToolStripMenuItem.Text = "Export Enums";
            exportEnumsToolStripMenuItem.Click += ExportEnumsToolStripMenuItem_Click;
            // 
            // exportUsedHashesToolStripMenuItem
            // 
            exportUsedHashesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportUsedHashesToolStripMenuItem.Name = "exportUsedHashesToolStripMenuItem";
            exportUsedHashesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            exportUsedHashesToolStripMenuItem.Text = "Export Used Hashes";
            exportUsedHashesToolStripMenuItem.Click += exportUsedHashesToolStripMenuItem_Click;
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
            rstbEditorButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            rstbEditorButton.ForeColor = System.Drawing.Color.White;
            rstbEditorButton.Location = new System.Drawing.Point(377, 91);
            rstbEditorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rstbEditorButton.Name = "rstbEditorButton";
            rstbEditorButton.Size = new System.Drawing.Size(254, 41);
            rstbEditorButton.TabIndex = 8;
            rstbEditorButton.Text = "Open RSTB Editor";
            rstbEditorButton.UseVisualStyleBackColor = false;
            rstbEditorButton.Click += RstbEditorButton_Click;
            // 
            // sarcEditorButton
            // 
            sarcEditorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sarcEditorButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            sarcEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            sarcEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            sarcEditorButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            sarcEditorButton.ForeColor = System.Drawing.Color.White;
            sarcEditorButton.Location = new System.Drawing.Point(377, 138);
            sarcEditorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            sarcEditorButton.Name = "sarcEditorButton";
            sarcEditorButton.Size = new System.Drawing.Size(254, 41);
            sarcEditorButton.TabIndex = 9;
            sarcEditorButton.Text = "Open SARC Editor";
            sarcEditorButton.UseVisualStyleBackColor = false;
            sarcEditorButton.Click += SarcEditorButton_Click;
            // 
            // itemParamHelperButton
            // 
            itemParamHelperButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            itemParamHelperButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            itemParamHelperButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            itemParamHelperButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            itemParamHelperButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            itemParamHelperButton.ForeColor = System.Drawing.Color.White;
            itemParamHelperButton.Location = new System.Drawing.Point(377, 185);
            itemParamHelperButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            itemParamHelperButton.Name = "itemParamHelperButton";
            itemParamHelperButton.Size = new System.Drawing.Size(254, 41);
            itemParamHelperButton.TabIndex = 10;
            itemParamHelperButton.Text = "Open ItemParam Helper";
            itemParamHelperButton.UseVisualStyleBackColor = false;
            itemParamHelperButton.Click += ItemParamHelperButton_Click;
            // 
            // HeavenMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(645, 293);
            Controls.Add(itemParamHelperButton);
            Controls.Add(sarcEditorButton);
            Controls.Add(rstbEditorButton);
            Controls.Add(bcsvEditorButton);
            Controls.Add(pictureBox1);
            Controls.Add(topMenu);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = topMenu;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "HeavenMain";
            Text = "Heaven Tool v1.0";
            topMenu.ResumeLayout(false);
            topMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button bcsvEditorButton;
        private DarkMenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button rstbEditorButton;
        private System.Windows.Forms.Button sarcEditorButton;
        private System.Windows.Forms.Button itemParamHelperButton;
        private System.Windows.Forms.ToolStripMenuItem compressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaz0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaz0DecompressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportEnumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportUsedHashesToolStripMenuItem;
    }
}