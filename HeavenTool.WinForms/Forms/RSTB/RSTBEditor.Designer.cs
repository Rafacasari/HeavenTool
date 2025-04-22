namespace HeavenTool.Forms.RSTB
{
    partial class RSTBEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RSTBEditor));
            dataGrid = new System.Windows.Forms.DataGridView();
            fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            fileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DLC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            TopMenu = new Components.DarkMenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsButton = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            addNewEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            updateFromModdedRomFs = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            closeFileButton = new System.Windows.Forms.ToolStripMenuItem();
            optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            associateRstbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            compareDifferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            updateHashListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusBar = new Components.DarkStatusStrip();
            statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
            TopMenu.SuspendLayout();
            statusBar.SuspendLayout();
            SuspendLayout();
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGrid.ColumnHeadersHeight = 25;
            dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { fileName, fileSize, DLC });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dataGrid.Location = new System.Drawing.Point(0, 24);
            dataGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGrid.Name = "dataGrid";
            dataGrid.ReadOnly = true;
            dataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGrid.RowHeadersWidth = 25;
            dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGrid.Size = new System.Drawing.Size(538, 321);
            dataGrid.StandardTab = true;
            dataGrid.TabIndex = 5;
            dataGrid.CellFormatting += mainDataGridView_CellFormatting;
            // 
            // fileName
            // 
            fileName.HeaderText = "File Name";
            fileName.Name = "fileName";
            fileName.ReadOnly = true;
            fileName.Width = 350;
            // 
            // fileSize
            // 
            fileSize.HeaderText = "Size";
            fileSize.Name = "fileSize";
            fileSize.ReadOnly = true;
            fileSize.ToolTipText = "File size in bytes";
            fileSize.Width = 80;
            // 
            // DLC
            // 
            DLC.HeaderText = "DLC";
            DLC.Name = "DLC";
            DLC.ReadOnly = true;
            DLC.ToolTipText = "DLC Number";
            DLC.Width = 80;
            // 
            // TopMenu
            // 
            TopMenu.BackColor = System.Drawing.Color.Transparent;
            TopMenu.ForeColor = System.Drawing.Color.White;
            TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, devToolStripMenuItem });
            TopMenu.Location = new System.Drawing.Point(0, 0);
            TopMenu.Name = "TopMenu";
            TopMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            TopMenu.Size = new System.Drawing.Size(538, 24);
            TopMenu.TabIndex = 6;
            TopMenu.Text = "Top Menu";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveAsButton, toolStripSeparator1, addNewEntriesToolStripMenuItem, updateFromModdedRomFs, toolStripSeparator2, closeFileButton });
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
            openToolStripMenuItem.ShortcutKeyDisplayString = "";
            openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            openToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // saveAsButton
            // 
            saveAsButton.ForeColor = System.Drawing.Color.White;
            saveAsButton.Image = Properties.Resources.save_as;
            saveAsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            saveAsButton.Name = "saveAsButton";
            saveAsButton.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            saveAsButton.Size = new System.Drawing.Size(245, 30);
            saveAsButton.Text = "&Save as...";
            saveAsButton.Click += SaveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // addNewEntriesToolStripMenuItem
            // 
            addNewEntriesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            addNewEntriesToolStripMenuItem.Name = "addNewEntriesToolStripMenuItem";
            addNewEntriesToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            addNewEntriesToolStripMenuItem.Text = "Add new entries";
            addNewEntriesToolStripMenuItem.Click += AddNewEntriesToolStripMenuItem_Click;
            // 
            // updateFromModdedRomFs
            // 
            updateFromModdedRomFs.ForeColor = System.Drawing.Color.White;
            updateFromModdedRomFs.Image = Properties.Resources.update;
            updateFromModdedRomFs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            updateFromModdedRomFs.Name = "updateFromModdedRomFs";
            updateFromModdedRomFs.Size = new System.Drawing.Size(245, 30);
            updateFromModdedRomFs.Text = "Update from modded RomFs...";
            updateFromModdedRomFs.Click += UpdateFromModdedRomFs_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(242, 6);
            // 
            // closeFileButton
            // 
            closeFileButton.ForeColor = System.Drawing.Color.White;
            closeFileButton.Image = Properties.Resources.cancel;
            closeFileButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            closeFileButton.Name = "closeFileButton";
            closeFileButton.Size = new System.Drawing.Size(245, 30);
            closeFileButton.Text = "Close File";
            closeFileButton.Click += CloseFileToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { searchToolStripMenuItem, associateRstbToolStripMenuItem, compareDifferenceToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F;
            searchToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            searchToolStripMenuItem.Text = "Search";
            searchToolStripMenuItem.Click += SearchToolStripMenuItem_Click;
            // 
            // associateRstbToolStripMenuItem
            // 
            associateRstbToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            associateRstbToolStripMenuItem.Name = "associateRstbToolStripMenuItem";
            associateRstbToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            associateRstbToolStripMenuItem.Text = "Associate srsizetable";
            associateRstbToolStripMenuItem.Click += AssociateSrsizetableToolStripMenuItem_Click;
            // 
            // compareDifferenceToolStripMenuItem
            // 
            compareDifferenceToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            compareDifferenceToolStripMenuItem.Name = "compareDifferenceToolStripMenuItem";
            compareDifferenceToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            compareDifferenceToolStripMenuItem.Text = "Compare Difference";
            compareDifferenceToolStripMenuItem.Click += CompareDifferenceToolStripMenuItem_Click;
            // 
            // devToolStripMenuItem
            // 
            devToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { updateHashListToolStripMenuItem });
            devToolStripMenuItem.Name = "devToolStripMenuItem";
            devToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            devToolStripMenuItem.Text = "Dev";
            // 
            // updateHashListToolStripMenuItem
            // 
            updateHashListToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            updateHashListToolStripMenuItem.Name = "updateHashListToolStripMenuItem";
            updateHashListToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            updateHashListToolStripMenuItem.Text = "Update Hash List";
            updateHashListToolStripMenuItem.Click += UpdateHashListToolStripMenuItem_Click;
            // 
            // statusBar
            // 
            statusBar.AllowDrop = true;
            statusBar.AllowMerge = false;
            statusBar.BackColor = System.Drawing.Color.Transparent;
            statusBar.ForeColor = System.Drawing.Color.White;
            statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusProgressBar, statusLabel });
            statusBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusBar.Location = new System.Drawing.Point(0, 323);
            statusBar.Name = "statusBar";
            statusBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            statusBar.Size = new System.Drawing.Size(538, 22);
            statusBar.SizingGrip = false;
            statusBar.TabIndex = 7;
            statusBar.Text = "darkStatusStrip1";
            // 
            // statusProgressBar
            // 
            statusProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            statusProgressBar.Name = "statusProgressBar";
            statusProgressBar.Size = new System.Drawing.Size(100, 16);
            statusProgressBar.Value = 25;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(16, 17);
            statusLabel.Text = "...";
            // 
            // RSTBEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(538, 345);
            Controls.Add(statusBar);
            Controls.Add(dataGrid);
            Controls.Add(TopMenu);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "RSTBEditor";
            Text = "RSTB Editor";
            ((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
            TopMenu.ResumeLayout(false);
            TopMenu.PerformLayout();
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.DataGridView dataGrid;
        private Components.DarkMenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateFromModdedRomFs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLC;
        private System.Windows.Forms.ToolStripMenuItem saveAsButton;
        private System.Windows.Forms.ToolStripMenuItem devToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateHashListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeFileButton;
        private Components.DarkStatusStrip statusBar;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem associateRstbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareDifferenceToolStripMenuItem;
    }
}