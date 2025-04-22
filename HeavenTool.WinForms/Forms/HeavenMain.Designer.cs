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
            compressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            yaz0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            yaz0DecompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            devToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            bCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportEnumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportCFGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            rstbEditorButton = new System.Windows.Forms.Button();
            sarcEditorButton = new System.Windows.Forms.Button();
            itemParamHelperButton = new System.Windows.Forms.Button();
            bcsvReworkButton = new System.Windows.Forms.Button();
            exportUsedHashesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            bcsvEditorButton.Click += BcsvEditorButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.Color.Transparent;
            menuStrip1.ForeColor = System.Drawing.Color.White;
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { optionsToolStripMenuItem, devToolsToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(631, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { compressionToolStripMenuItem });
            optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
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
            bCSVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportLabelsToolStripMenuItem, exportEnumsToolStripMenuItem, exportCFGToolStripMenuItem, exportUsedHashesToolStripMenuItem });
            bCSVToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            bCSVToolStripMenuItem.Name = "bCSVToolStripMenuItem";
            bCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            bCSVToolStripMenuItem.Text = "BCSV";
            // 
            // exportLabelsToolStripMenuItem
            // 
            exportLabelsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportLabelsToolStripMenuItem.Name = "exportLabelsToolStripMenuItem";
            exportLabelsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            exportLabelsToolStripMenuItem.Text = "Export Labels";
            exportLabelsToolStripMenuItem.Click += ExportLabelsToolStripMenuItem_Click;
            // 
            // exportEnumsToolStripMenuItem
            // 
            exportEnumsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportEnumsToolStripMenuItem.Name = "exportEnumsToolStripMenuItem";
            exportEnumsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            exportEnumsToolStripMenuItem.Text = "Export Enums";
            exportEnumsToolStripMenuItem.Click += ExportEnumsToolStripMenuItem_Click;
            // 
            // exportCFGToolStripMenuItem
            // 
            exportCFGToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportCFGToolStripMenuItem.Name = "exportCFGToolStripMenuItem";
            exportCFGToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            exportCFGToolStripMenuItem.Text = "Export CFG (Aeon's Editor)";
            exportCFGToolStripMenuItem.Click += exportCFGToolStripMenuItem_Click;
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
            rstbEditorButton.Click += RstbEditorButton_Click;
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
            sarcEditorButton.Click += SarcEditorButton_Click;
            // 
            // itemParamHelperButton
            // 
            itemParamHelperButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            itemParamHelperButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            itemParamHelperButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            itemParamHelperButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            itemParamHelperButton.ForeColor = System.Drawing.Color.White;
            itemParamHelperButton.Location = new System.Drawing.Point(377, 159);
            itemParamHelperButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            itemParamHelperButton.Name = "itemParamHelperButton";
            itemParamHelperButton.Size = new System.Drawing.Size(240, 32);
            itemParamHelperButton.TabIndex = 10;
            itemParamHelperButton.Text = "Open ItemParam Helper";
            itemParamHelperButton.UseVisualStyleBackColor = false;
            itemParamHelperButton.Click += ItemParamHelperButton_Click;
            // 
            // bcsvReworkButton
            // 
            bcsvReworkButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bcsvReworkButton.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            bcsvReworkButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
            bcsvReworkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bcsvReworkButton.ForeColor = System.Drawing.Color.White;
            bcsvReworkButton.Location = new System.Drawing.Point(378, 250);
            bcsvReworkButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bcsvReworkButton.Name = "bcsvReworkButton";
            bcsvReworkButton.Size = new System.Drawing.Size(240, 28);
            bcsvReworkButton.TabIndex = 11;
            bcsvReworkButton.Text = "New BCSV Editor";
            bcsvReworkButton.UseVisualStyleBackColor = false;
            bcsvReworkButton.Click += BcsvReworkButton_Click;
            // 
            // exportUsedHashesToolStripMenuItem
            // 
            exportUsedHashesToolStripMenuItem.Name = "exportUsedHashesToolStripMenuItem";
            exportUsedHashesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            exportUsedHashesToolStripMenuItem.Text = "Export Used Hashes";
            exportUsedHashesToolStripMenuItem.Click += exportUsedHashesToolStripMenuItem_Click;
            // 
            // HeavenMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(631, 293);
            Controls.Add(bcsvReworkButton);
            Controls.Add(itemParamHelperButton);
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
        private System.Windows.Forms.Button itemParamHelperButton;
        private System.Windows.Forms.ToolStripMenuItem compressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaz0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaz0DecompressToolStripMenuItem;
        private System.Windows.Forms.Button bcsvReworkButton;
        private System.Windows.Forms.ToolStripMenuItem devToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportEnumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCFGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportUsedHashesToolStripMenuItem;
    }
}