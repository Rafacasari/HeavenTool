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
            button1 = new System.Windows.Forms.Button();
            fileInfoBar = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            fileInfoBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            propertyGrid1.Location = new System.Drawing.Point(531, 12);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(221, 476);
            propertyGrid1.TabIndex = 2;
            propertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            propertyGrid1.ViewBorderColor = System.Drawing.Color.FromArgb(60, 60, 60);
            propertyGrid1.ViewForeColor = System.Drawing.Color.WhiteSmoke;
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button1.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(10, 10, 10);
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(10, 10, 10);
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            button1.Location = new System.Drawing.Point(531, 494);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(221, 30);
            button1.TabIndex = 3;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = false;
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
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.Color.Black;
            pictureBox1.Location = new System.Drawing.Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(512, 512);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // PBCEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(764, 559);
            Controls.Add(fileInfoBar);
            Controls.Add(button1);
            Controls.Add(propertyGrid1);
            Controls.Add(pictureBox1);
            ForeColor = System.Drawing.Color.White;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "PBCEditor";
            Text = "PBCEditor";
            fileInfoBar.ResumeLayout(false);
            fileInfoBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip fileInfoBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}