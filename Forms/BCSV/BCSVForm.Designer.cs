using HeavenTool.Forms.Components;
using System.Windows.Forms;

namespace HeavenTool
{
    partial class BCSVForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCSVForm));
            this.topMenuStrip = new HeavenTool.Forms.Components.DarkMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToCSVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.associatebcsvWithThisProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportValidHashesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLabelFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEnumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOnFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMenu = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.versionNumberLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.openBCSVFile = new System.Windows.Forms.OpenFileDialog();
            this.validHeaderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerHashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportValuesAstxtFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s32u32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.int32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hshCstringRefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.murmurHashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dragInfo = new System.Windows.Forms.Label();
            this.topMenuStrip.SuspendLayout();
            this.statusStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            this.validHeaderContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenuStrip
            // 
            this.topMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.topMenuStrip.ForeColor = System.Drawing.Color.White;
            this.topMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.devToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.topMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.topMenuStrip.Name = "topMenuStrip";
            this.topMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.topMenuStrip.Size = new System.Drawing.Size(586, 24);
            this.topMenuStrip.TabIndex = 1;
            this.topMenuStrip.Text = "Top Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.importFromFileToolStripMenuItem,
            this.exportSelectionToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToCSVFileToolStripMenuItem,
            this.unloadFileToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.open_file;
            this.openToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveAsToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.save_as;
            this.saveAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // importFromFileToolStripMenuItem
            // 
            this.importFromFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.importFromFileToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.import;
            this.importFromFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importFromFileToolStripMenuItem.Name = "importFromFileToolStripMenuItem";
            this.importFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.importFromFileToolStripMenuItem.Text = "Import from file...";
            this.importFromFileToolStripMenuItem.ToolTipText = "Import lines from a BCSV file (headers must match)";
            this.importFromFileToolStripMenuItem.Click += new System.EventHandler(this.importFromFileToolStripMenuItem_Click);
            // 
            // exportSelectionToolStripMenuItem
            // 
            this.exportSelectionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportSelectionToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.export;
            this.exportSelectionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportSelectionToolStripMenuItem.Name = "exportSelectionToolStripMenuItem";
            this.exportSelectionToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.exportSelectionToolStripMenuItem.Text = "Export selection...";
            this.exportSelectionToolStripMenuItem.ToolTipText = "Export the selected lines to a new BCSV file";
            this.exportSelectionToolStripMenuItem.Click += new System.EventHandler(this.exportSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // exportToCSVFileToolStripMenuItem
            // 
            this.exportToCSVFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportToCSVFileToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.export_csv;
            this.exportToCSVFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportToCSVFileToolStripMenuItem.Name = "exportToCSVFileToolStripMenuItem";
            this.exportToCSVFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.exportToCSVFileToolStripMenuItem.Text = "Export as .CSV";
            this.exportToCSVFileToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVFileToolStripMenuItem_Click);
            // 
            // unloadFileToolStripMenuItem
            // 
            this.unloadFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.unloadFileToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.cancel;
            this.unloadFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unloadFileToolStripMenuItem.Name = "unloadFileToolStripMenuItem";
            this.unloadFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.unloadFileToolStripMenuItem.Text = "Unload File";
            this.unloadFileToolStripMenuItem.Click += new System.EventHandler(this.unloadFileToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.newEntryToolStripMenuItem,
            this.duplicateRowToolStripMenuItem,
            this.deleteRowsToolStripMenuItem,
            this.compareRowsToolStripMenuItem});
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.searchToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.search;
            this.searchToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.ToolTipText = "Search for a line on this file";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // newEntryToolStripMenuItem
            // 
            this.newEntryToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newEntryToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.new_entry;
            this.newEntryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newEntryToolStripMenuItem.Name = "newEntryToolStripMenuItem";
            this.newEntryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newEntryToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.newEntryToolStripMenuItem.Text = "New Entry";
            this.newEntryToolStripMenuItem.ToolTipText = "Add a new entry into this file";
            this.newEntryToolStripMenuItem.Click += new System.EventHandler(this.newEntryToolStripMenuItem_Click);
            // 
            // duplicateRowToolStripMenuItem
            // 
            this.duplicateRowToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.duplicateRowToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.duplicate;
            this.duplicateRowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.duplicateRowToolStripMenuItem.Name = "duplicateRowToolStripMenuItem";
            this.duplicateRowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.duplicateRowToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.duplicateRowToolStripMenuItem.Text = "Duplicate Row(s)";
            this.duplicateRowToolStripMenuItem.ToolTipText = "Duplicate the selected row(s)";
            this.duplicateRowToolStripMenuItem.Click += new System.EventHandler(this.duplicateRowToolStripMenuItem_Click);
            // 
            // deleteRowsToolStripMenuItem
            // 
            this.deleteRowsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteRowsToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.remove;
            this.deleteRowsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteRowsToolStripMenuItem.Name = "deleteRowsToolStripMenuItem";
            this.deleteRowsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteRowsToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.deleteRowsToolStripMenuItem.Text = "Delete Row(s)";
            this.deleteRowsToolStripMenuItem.ToolTipText = "Delete the selected row(s)";
            this.deleteRowsToolStripMenuItem.Click += new System.EventHandler(this.deleteRowsToolStripMenuItem_Click);
            // 
            // compareRowsToolStripMenuItem
            // 
            this.compareRowsToolStripMenuItem.Enabled = false;
            this.compareRowsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.compareRowsToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.compare;
            this.compareRowsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.compareRowsToolStripMenuItem.Name = "compareRowsToolStripMenuItem";
            this.compareRowsToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            this.compareRowsToolStripMenuItem.Text = "Compare Rows";
            this.compareRowsToolStripMenuItem.ToolTipText = "Compare selected rows in a new window (read-only)";
            this.compareRowsToolStripMenuItem.Click += new System.EventHandler(this.compareRowsToolStripMenuItem_Click);
            // 
            // devToolStripMenuItem
            // 
            this.devToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.associatebcsvWithThisProgramToolStripMenuItem,
            this.exportValidHashesToolStripMenuItem,
            this.exportLabelFieldsToolStripMenuItem,
            this.exportEnumsToolStripMenuItem});
            this.devToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.devToolStripMenuItem.Name = "devToolStripMenuItem";
            this.devToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.devToolStripMenuItem.Text = "Options";
            // 
            // associatebcsvWithThisProgramToolStripMenuItem
            // 
            this.associatebcsvWithThisProgramToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.associatebcsvWithThisProgramToolStripMenuItem.Name = "associatebcsvWithThisProgramToolStripMenuItem";
            this.associatebcsvWithThisProgramToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.associatebcsvWithThisProgramToolStripMenuItem.Text = "Associate .BCSV files with this program";
            this.associatebcsvWithThisProgramToolStripMenuItem.Click += new System.EventHandler(this.associatebcsvWithThisProgramToolStripMenuItem_Click);
            // 
            // exportValidHashesToolStripMenuItem
            // 
            this.exportValidHashesToolStripMenuItem.Enabled = false;
            this.exportValidHashesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportValidHashesToolStripMenuItem.Name = "exportValidHashesToolStripMenuItem";
            this.exportValidHashesToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.exportValidHashesToolStripMenuItem.Text = "Export only used hashes of selected folder";
            this.exportValidHashesToolStripMenuItem.Visible = false;
            this.exportValidHashesToolStripMenuItem.Click += new System.EventHandler(this.exportValidHashesToolStripMenuItem_Click);
            // 
            // exportLabelFieldsToolStripMenuItem
            // 
            this.exportLabelFieldsToolStripMenuItem.Enabled = false;
            this.exportLabelFieldsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportLabelFieldsToolStripMenuItem.Name = "exportLabelFieldsToolStripMenuItem";
            this.exportLabelFieldsToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.exportLabelFieldsToolStripMenuItem.Text = "Export Label Fields";
            this.exportLabelFieldsToolStripMenuItem.Visible = false;
            this.exportLabelFieldsToolStripMenuItem.Click += new System.EventHandler(this.exportLabelFieldsToolStripMenuItem_Click);
            // 
            // exportEnumsToolStripMenuItem
            // 
            this.exportEnumsToolStripMenuItem.Enabled = false;
            this.exportEnumsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportEnumsToolStripMenuItem.Name = "exportEnumsToolStripMenuItem";
            this.exportEnumsToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.exportEnumsToolStripMenuItem.Text = "[Dev] Export Enum(s)";
            this.exportEnumsToolStripMenuItem.Visible = false;
            this.exportEnumsToolStripMenuItem.Click += new System.EventHandler(this.exportEnumsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchOnFilesToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // searchOnFilesToolStripMenuItem
            // 
            this.searchOnFilesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.searchOnFilesToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.search;
            this.searchOnFilesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchOnFilesToolStripMenuItem.Name = "searchOnFilesToolStripMenuItem";
            this.searchOnFilesToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.searchOnFilesToolStripMenuItem.Text = "Search on files";
            this.searchOnFilesToolStripMenuItem.Click += new System.EventHandler(this.searchOnFilesToolStripMenuItem_Click);
            // 
            // statusStripMenu
            // 
            this.statusStripMenu.BackColor = System.Drawing.Color.Transparent;
            this.statusStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel,
            this.versionNumberLabel});
            this.statusStripMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripMenu.Location = new System.Drawing.Point(0, 313);
            this.statusStripMenu.Name = "statusStripMenu";
            this.statusStripMenu.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStripMenu.Size = new System.Drawing.Size(586, 22);
            this.statusStripMenu.SizingGrip = false;
            this.statusStripMenu.TabIndex = 2;
            // 
            // infoLabel
            // 
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(28, 17);
            this.infoLabel.Text = "Info";
            // 
            // versionNumberLabel
            // 
            this.versionNumberLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.versionNumberLabel.Name = "versionNumberLabel";
            this.versionNumberLabel.Size = new System.Drawing.Size(31, 17);
            this.versionNumberLabel.Text = "1.0.0";
            // 
            // mainDataGridView
            // 
            this.mainDataGridView.AllowUserToAddRows = false;
            this.mainDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.mainDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.mainDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.mainDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.mainDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.mainDataGridView.ColumnHeadersHeight = 25;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mainDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.mainDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGridView.EnableHeadersVisualStyles = false;
            this.mainDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.mainDataGridView.Location = new System.Drawing.Point(0, 24);
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.mainDataGridView.RowHeadersWidth = 25;
            this.mainDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            this.mainDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.mainDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGridView.Size = new System.Drawing.Size(586, 289);
            this.mainDataGridView.StandardTab = true;
            this.mainDataGridView.TabIndex = 4;
            this.mainDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.mainDataGridView_CellFormatting);
            this.mainDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDataGridView_CellValueChanged);
            this.mainDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mainDataGridView_ColumnHeaderMouseClick);
            this.mainDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.mainDataGridView_EditingControlShowing);
            this.mainDataGridView.SelectionChanged += new System.EventHandler(this.mainDataGridView_SelectionChanged);
            this.mainDataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.mainDataGridView_SortCompare);
            // 
            // openBCSVFile
            // 
            this.openBCSVFile.DefaultExt = "*.bcsv";
            this.openBCSVFile.Filter = "BCSV|*.bcsv";
            this.openBCSVFile.Title = "Select a BCSV file...";
            this.openBCSVFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openBCSVFile_FileOk);
            // 
            // validHeaderContextMenu
            // 
            this.validHeaderContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.exportValuesAstxtFileToolStripMenuItem,
            this.viewAsToolStripMenuItem});
            this.validHeaderContextMenu.Name = "validHeaderContextMenu";
            this.validHeaderContextMenu.Size = new System.Drawing.Size(198, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.headerNameToolStripMenuItem,
            this.headerHashToolStripMenuItem});
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // headerNameToolStripMenuItem
            // 
            this.headerNameToolStripMenuItem.Name = "headerNameToolStripMenuItem";
            this.headerNameToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.headerNameToolStripMenuItem.Text = "Header Name";
            this.headerNameToolStripMenuItem.Click += new System.EventHandler(this.headerNameToolStripMenuItem_Click);
            // 
            // headerHashToolStripMenuItem
            // 
            this.headerHashToolStripMenuItem.Name = "headerHashToolStripMenuItem";
            this.headerHashToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.headerHashToolStripMenuItem.Text = "Header Hash";
            this.headerHashToolStripMenuItem.Click += new System.EventHandler(this.headerHashToolStripMenuItem_Click);
            // 
            // exportValuesAstxtFileToolStripMenuItem
            // 
            this.exportValuesAstxtFileToolStripMenuItem.Name = "exportValuesAstxtFileToolStripMenuItem";
            this.exportValuesAstxtFileToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportValuesAstxtFileToolStripMenuItem.Text = "Export values as .txt file";
            this.exportValuesAstxtFileToolStripMenuItem.Click += new System.EventHandler(this.exportValuesAstxtFileToolStripMenuItem_Click);
            // 
            // viewAsToolStripMenuItem
            // 
            this.viewAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.s32u32ToolStripMenuItem,
            this.int32ToolStripMenuItem,
            this.f32ToolStripMenuItem,
            this.stringToolStripMenuItem,
            this.hshCstringRefToolStripMenuItem,
            this.murmurHashToolStripMenuItem});
            this.viewAsToolStripMenuItem.Name = "viewAsToolStripMenuItem";
            this.viewAsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.viewAsToolStripMenuItem.Text = "View as";
            // 
            // s32u32ToolStripMenuItem
            // 
            this.s32u32ToolStripMenuItem.Name = "s32u32ToolStripMenuItem";
            this.s32u32ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.s32u32ToolStripMenuItem.Text = "UInt";
            this.s32u32ToolStripMenuItem.Click += new System.EventHandler(this.s32u32ToolStripMenuItem_Click);
            // 
            // int32ToolStripMenuItem
            // 
            this.int32ToolStripMenuItem.Name = "int32ToolStripMenuItem";
            this.int32ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.int32ToolStripMenuItem.Text = "Int";
            this.int32ToolStripMenuItem.Click += new System.EventHandler(this.int32ToolStripMenuItem_Click);
            // 
            // f32ToolStripMenuItem
            // 
            this.f32ToolStripMenuItem.Name = "f32ToolStripMenuItem";
            this.f32ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.f32ToolStripMenuItem.Text = "Float";
            this.f32ToolStripMenuItem.Click += new System.EventHandler(this.f32ToolStripMenuItem_Click);
            // 
            // stringToolStripMenuItem
            // 
            this.stringToolStripMenuItem.Name = "stringToolStripMenuItem";
            this.stringToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.stringToolStripMenuItem.Text = "String";
            this.stringToolStripMenuItem.Click += new System.EventHandler(this.stringToolStripMenuItem_Click);
            // 
            // hshCstringRefToolStripMenuItem
            // 
            this.hshCstringRefToolStripMenuItem.Name = "hshCstringRefToolStripMenuItem";
            this.hshCstringRefToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.hshCstringRefToolStripMenuItem.Text = "Crc32";
            this.hshCstringRefToolStripMenuItem.Click += new System.EventHandler(this.hshCstringRefToolStripMenuItem_Click);
            // 
            // murmurHashToolStripMenuItem
            // 
            this.murmurHashToolStripMenuItem.Name = "murmurHashToolStripMenuItem";
            this.murmurHashToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.murmurHashToolStripMenuItem.Text = "Murmur3";
            this.murmurHashToolStripMenuItem.Click += new System.EventHandler(this.murmurHashToolStripMenuItem_Click);
            // 
            // dragInfo
            // 
            this.dragInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dragInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.dragInfo.ForeColor = System.Drawing.Color.White;
            this.dragInfo.Location = new System.Drawing.Point(12, 252);
            this.dragInfo.Name = "dragInfo";
            this.dragInfo.Size = new System.Drawing.Size(182, 52);
            this.dragInfo.TabIndex = 5;
            this.dragInfo.Text = "Drag a file here\r\nor use File > Open";
            this.dragInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BCSVForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(586, 335);
            this.Controls.Add(this.dragInfo);
            this.Controls.Add(this.mainDataGridView);
            this.Controls.Add(this.statusStripMenu);
            this.Controls.Add(this.topMenuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "BCSVForm";
            this.Text = "ACNH Heaven Tool | v1.0.0 | BCSV Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFrm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFrm_DragEnter);
            this.topMenuStrip.ResumeLayout(false);
            this.topMenuStrip.PerformLayout();
            this.statusStripMenu.ResumeLayout(false);
            this.statusStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            this.validHeaderContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private DarkMenuStrip topMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private StatusStrip statusStripMenu;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private DataGridView mainDataGridView;
        private OpenFileDialog openBCSVFile;
        private ToolStripStatusLabel infoLabel;
        private ToolStripMenuItem searchOnFilesToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem newEntryToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem unloadFileToolStripMenuItem;
        private ToolStripMenuItem devToolStripMenuItem;
        private ToolStripMenuItem exportValidHashesToolStripMenuItem;
        private ToolStripMenuItem duplicateRowToolStripMenuItem;
        private ToolStripMenuItem associatebcsvWithThisProgramToolStripMenuItem;
        private ToolStripMenuItem deleteRowsToolStripMenuItem;
        private ContextMenuStrip validHeaderContextMenu;
        private ToolStripMenuItem exportValuesAstxtFileToolStripMenuItem;
        private ToolStripMenuItem exportLabelFieldsToolStripMenuItem;
        private ToolStripStatusLabel versionNumberLabel;
        private ToolStripMenuItem exportToCSVFileToolStripMenuItem;
        private ToolStripMenuItem compareRowsToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem importFromFileToolStripMenuItem;
        private ToolStripMenuItem exportSelectionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem headerNameToolStripMenuItem;
        private ToolStripMenuItem headerHashToolStripMenuItem;
        private ToolStripMenuItem viewAsToolStripMenuItem;
        private ToolStripMenuItem stringToolStripMenuItem;
        private ToolStripMenuItem s32u32ToolStripMenuItem;
        private ToolStripMenuItem f32ToolStripMenuItem;
        private ToolStripMenuItem hshCstringRefToolStripMenuItem;
        private ToolStripMenuItem murmurHashToolStripMenuItem;
        private ToolStripMenuItem int32ToolStripMenuItem;
        private Label dragInfo;
        private ToolStripMenuItem exportEnumsToolStripMenuItem;
    }
}

