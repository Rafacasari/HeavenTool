namespace HeavenTool.Forms.PBC
{
    partial class PBCEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PBCEditor));
            propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            saveButton = new System.Windows.Forms.Button();
            fileInfoBar = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            darkMenuStrip1 = new Components.DarkMenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            currentZoomMenu = new System.Windows.Forms.ToolStripMenuItem();
            zoomPlusButton = new System.Windows.Forms.ToolStripMenuItem();
            zoomMinusButton = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            collisionMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            heightMap1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            heightMap2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            heightMap3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            viewIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pbcPreview = new TileEditor();
            fileInfoBar.SuspendLayout();
            darkMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // propertyGrid1
            // 
            propertyGrid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            propertyGrid1.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            propertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(230, 230, 230);
            propertyGrid1.CategorySplitterColor = System.Drawing.Color.FromArgb(40, 40, 40);
            propertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(127, 245, 245, 245);
            propertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            propertyGrid1.HelpBorderColor = System.Drawing.Color.FromArgb(60, 60, 60);
            propertyGrid1.HelpForeColor = System.Drawing.Color.White;
            propertyGrid1.HelpVisible = false;
            propertyGrid1.LineColor = System.Drawing.Color.FromArgb(40, 40, 40);
            propertyGrid1.Location = new System.Drawing.Point(531, 342);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(221, 146);
            propertyGrid1.TabIndex = 2;
            propertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            propertyGrid1.ViewBorderColor = System.Drawing.Color.FromArgb(60, 60, 60);
            propertyGrid1.ViewForeColor = System.Drawing.Color.WhiteSmoke;
            // 
            // saveButton
            // 
            saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            saveButton.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(10, 10, 10);
            saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(10, 10, 10);
            saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            saveButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            saveButton.Location = new System.Drawing.Point(531, 494);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(221, 30);
            saveButton.TabIndex = 3;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;
            // 
            // fileInfoBar
            // 
            fileInfoBar.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            fileInfoBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
            fileInfoBar.Location = new System.Drawing.Point(0, 537);
            fileInfoBar.Name = "fileInfoBar";
            fileInfoBar.Size = new System.Drawing.Size(764, 22);
            fileInfoBar.SizingGrip = false;
            fileInfoBar.TabIndex = 4;
            fileInfoBar.Text = "Information";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(121, 17);
            statusLabel.Text = "Width: -1 | Height: -1 ";
            // 
            // darkMenuStrip1
            // 
            darkMenuStrip1.BackColor = System.Drawing.Color.Transparent;
            darkMenuStrip1.ForeColor = System.Drawing.Color.White;
            darkMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, currentZoomMenu, viewToolStripMenuItem });
            darkMenuStrip1.Location = new System.Drawing.Point(0, 0);
            darkMenuStrip1.Name = "darkMenuStrip1";
            darkMenuStrip1.Size = new System.Drawing.Size(764, 24);
            darkMenuStrip1.TabIndex = 5;
            darkMenuStrip1.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // currentZoomMenu
            // 
            currentZoomMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { zoomPlusButton, zoomMinusButton });
            currentZoomMenu.Name = "currentZoomMenu";
            currentZoomMenu.Size = new System.Drawing.Size(68, 20);
            currentZoomMenu.Text = "Zoom: 5x";
            // 
            // zoomPlusButton
            // 
            zoomPlusButton.ForeColor = System.Drawing.Color.White;
            zoomPlusButton.Name = "zoomPlusButton";
            zoomPlusButton.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus;
            zoomPlusButton.Size = new System.Drawing.Size(172, 22);
            zoomPlusButton.Text = "+";
            zoomPlusButton.Click += zoomPlusButton_Click;
            // 
            // zoomMinusButton
            // 
            zoomMinusButton.ForeColor = System.Drawing.Color.White;
            zoomMinusButton.Name = "zoomMinusButton";
            zoomMinusButton.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus;
            zoomMinusButton.Size = new System.Drawing.Size(172, 22);
            zoomMinusButton.Text = "-";
            zoomMinusButton.Click += zoomMinusButton_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { collisionMapToolStripMenuItem, heightMap1ToolStripMenuItem, heightMap2ToolStripMenuItem, heightMap3ToolStripMenuItem, toolStripSeparator1, viewIDToolStripMenuItem, gridToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // collisionMapToolStripMenuItem
            // 
            collisionMapToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            collisionMapToolStripMenuItem.Name = "collisionMapToolStripMenuItem";
            collisionMapToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            collisionMapToolStripMenuItem.Text = "Collision Map";
            collisionMapToolStripMenuItem.Click += collisionMapToolStripMenuItem_Click;
            // 
            // heightMap1ToolStripMenuItem
            // 
            heightMap1ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            heightMap1ToolStripMenuItem.Name = "heightMap1ToolStripMenuItem";
            heightMap1ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            heightMap1ToolStripMenuItem.Text = "Height Map 1";
            heightMap1ToolStripMenuItem.Click += heightMap1ToolStripMenuItem_Click;
            // 
            // heightMap2ToolStripMenuItem
            // 
            heightMap2ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            heightMap2ToolStripMenuItem.Name = "heightMap2ToolStripMenuItem";
            heightMap2ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            heightMap2ToolStripMenuItem.Text = "Height Map 2";
            heightMap2ToolStripMenuItem.Click += heightMap2ToolStripMenuItem_Click;
            // 
            // heightMap3ToolStripMenuItem
            // 
            heightMap3ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            heightMap3ToolStripMenuItem.Name = "heightMap3ToolStripMenuItem";
            heightMap3ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            heightMap3ToolStripMenuItem.Text = "Height Map 3";
            heightMap3ToolStripMenuItem.Click += heightMap3ToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // viewIDToolStripMenuItem
            // 
            viewIDToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            viewIDToolStripMenuItem.Name = "viewIDToolStripMenuItem";
            viewIDToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            viewIDToolStripMenuItem.Text = "Show ID";
            viewIDToolStripMenuItem.Click += viewIDToolStripMenuItem_Click;
            // 
            // gridToolStripMenuItem
            // 
            gridToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            gridToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            gridToolStripMenuItem.Text = "Show Grid";
            gridToolStripMenuItem.Click += gridToolStripMenuItem_Click;
            // 
            // pbcPreview
            // 
            pbcPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pbcPreview.BackColor = System.Drawing.Color.Black;
            pbcPreview.Location = new System.Drawing.Point(12, 27);
            pbcPreview.Name = "pbcPreview";
            pbcPreview.PBCFile = null;
            pbcPreview.Size = new System.Drawing.Size(513, 497);
            pbcPreview.TabIndex = 6;
            pbcPreview.Text = "tileEditor1";
            pbcPreview.Zoom = 10;
            // 
            // PBCEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(764, 559);
            Controls.Add(pbcPreview);
            Controls.Add(fileInfoBar);
            Controls.Add(darkMenuStrip1);
            Controls.Add(saveButton);
            Controls.Add(propertyGrid1);
            ForeColor = System.Drawing.Color.White;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = darkMenuStrip1;
            Name = "PBCEditor";
            Text = "PBCEditor";
            fileInfoBar.ResumeLayout(false);
            fileInfoBar.PerformLayout();
            darkMenuStrip1.ResumeLayout(false);
            darkMenuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.StatusStrip fileInfoBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private Components.DarkMenuStrip darkMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem currentZoomMenu;
        private System.Windows.Forms.ToolStripMenuItem zoomPlusButton;
        private System.Windows.Forms.ToolStripMenuItem zoomMinusButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collisionMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heightMap1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heightMap2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heightMap3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem viewIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private TileEditor pbcPreview;
    }
}