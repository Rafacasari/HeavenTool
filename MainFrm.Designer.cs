using System;
using System.Drawing;
using System.Windows.Forms;

namespace HeavenTool
{
    partial class MainFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.topMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bruteForceMissingHashesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOnFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMenu = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.versionNumberLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.openBCSVFile = new System.Windows.Forms.OpenFileDialog();
            this.columnHeaderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hshCstringRefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s32u32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validHeaderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportValuesAstxtFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.topMenuStrip.SuspendLayout();
            this.statusStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            this.columnHeaderContextMenu.SuspendLayout();
            this.validHeaderContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenuStrip
            // 
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
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.importFromFileToolStripMenuItem,
            this.exportSelectionToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToCSVFileToolStripMenuItem,
            this.unloadFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.open_file;
            this.openToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.save;
            this.saveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // saveAsToolStripMenuItem
            // 
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
            // exportToCSVFileToolStripMenuItem
            // 
            this.exportToCSVFileToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.export_csv;
            this.exportToCSVFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportToCSVFileToolStripMenuItem.Name = "exportToCSVFileToolStripMenuItem";
            this.exportToCSVFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.exportToCSVFileToolStripMenuItem.Text = "Export as .CSV";
            this.exportToCSVFileToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVFileToolStripMenuItem_Click);
            // 
            // unloadFileToolStripMenuItem
            // 
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
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // searchToolStripMenuItem
            // 
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
            this.exportLabelFieldsToolStripMenuItem});
            this.devToolStripMenuItem.Name = "devToolStripMenuItem";
            this.devToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.devToolStripMenuItem.Text = "Options";
            // 
            // associatebcsvWithThisProgramToolStripMenuItem
            // 
            this.associatebcsvWithThisProgramToolStripMenuItem.Name = "associatebcsvWithThisProgramToolStripMenuItem";
            this.associatebcsvWithThisProgramToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.associatebcsvWithThisProgramToolStripMenuItem.Text = "Associate .BCSV files with this program";
            this.associatebcsvWithThisProgramToolStripMenuItem.Click += new System.EventHandler(this.associatebcsvWithThisProgramToolStripMenuItem_Click);
            // 
            // exportValidHashesToolStripMenuItem
            // 
            this.exportValidHashesToolStripMenuItem.Name = "exportValidHashesToolStripMenuItem";
            this.exportValidHashesToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.exportValidHashesToolStripMenuItem.Text = "Export only used hashes of selected folder";
            this.exportValidHashesToolStripMenuItem.Visible = false;
            this.exportValidHashesToolStripMenuItem.Click += new System.EventHandler(this.exportValidHashesToolStripMenuItem_Click);
            // 
            // exportLabelFieldsToolStripMenuItem
            // 
            this.exportLabelFieldsToolStripMenuItem.Name = "exportLabelFieldsToolStripMenuItem";
            this.exportLabelFieldsToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.exportLabelFieldsToolStripMenuItem.Text = "Export Label Fields";
            this.exportLabelFieldsToolStripMenuItem.Visible = false;
            this.exportLabelFieldsToolStripMenuItem.Click += new System.EventHandler(this.exportLabelFieldsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bruteForceMissingHashesToolStripMenuItem,
            this.searchOnFilesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // bruteForceMissingHashesToolStripMenuItem
            // 
            this.bruteForceMissingHashesToolStripMenuItem.Enabled = false;
            this.bruteForceMissingHashesToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.brute_force;
            this.bruteForceMissingHashesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bruteForceMissingHashesToolStripMenuItem.Name = "bruteForceMissingHashesToolStripMenuItem";
            this.bruteForceMissingHashesToolStripMenuItem.Size = new System.Drawing.Size(227, 30);
            this.bruteForceMissingHashesToolStripMenuItem.Text = "Brute Force Missing Hashes";
            this.bruteForceMissingHashesToolStripMenuItem.Click += new System.EventHandler(this.bruteForceMissingHashesToolStripMenuItem_Click);
            // 
            // searchOnFilesToolStripMenuItem
            // 
            this.searchOnFilesToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.search;
            this.searchOnFilesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchOnFilesToolStripMenuItem.Name = "searchOnFilesToolStripMenuItem";
            this.searchOnFilesToolStripMenuItem.Size = new System.Drawing.Size(227, 30);
            this.searchOnFilesToolStripMenuItem.Text = "Search on files";
            this.searchOnFilesToolStripMenuItem.Click += new System.EventHandler(this.searchOnFilesToolStripMenuItem_Click);
            // 
            // statusStripMenu
            // 
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
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.mainDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.mainDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.mainDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.mainDataGridView.ColumnHeadersHeight = 25;
            this.mainDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGridView.EnableHeadersVisualStyles = false;
            this.mainDataGridView.Location = new System.Drawing.Point(0, 24);
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.mainDataGridView.RowHeadersWidth = 25;
            this.mainDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.mainDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGridView.Size = new System.Drawing.Size(586, 289);
            this.mainDataGridView.StandardTab = true;
            this.mainDataGridView.TabIndex = 4;
            this.mainDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.mainDataGridView_CellFormatting);
            this.mainDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDataGridView_CellValueChanged);
            this.mainDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mainDataGridView_ColumnHeaderMouseClick);
            this.mainDataGridView.SelectionChanged += new System.EventHandler(this.mainDataGridView_SelectionChanged);
            // 
            // openBCSVFile
            // 
            this.openBCSVFile.DefaultExt = "*.bcsv";
            this.openBCSVFile.Filter = "BCSV|*.bcsv";
            this.openBCSVFile.Title = "Select a BCSV file...";
            this.openBCSVFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openBCSVFile_FileOk);
            // 
            // columnHeaderContextMenu
            // 
            this.columnHeaderContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hshCstringRefToolStripMenuItem,
            this.f32ToolStripMenuItem,
            this.s32u32ToolStripMenuItem,
            this.stringToolStripMenuItem});
            this.columnHeaderContextMenu.Name = "contextMenuStrip1";
            this.columnHeaderContextMenu.Size = new System.Drawing.Size(149, 92);
            // 
            // hshCstringRefToolStripMenuItem
            // 
            this.hshCstringRefToolStripMenuItem.Name = "hshCstringRefToolStripMenuItem";
            this.hshCstringRefToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.hshCstringRefToolStripMenuItem.Text = "hshCstringRef";
            this.hshCstringRefToolStripMenuItem.Click += new System.EventHandler(this.hshCstringRefToolStripMenuItem_Click);
            // 
            // f32ToolStripMenuItem
            // 
            this.f32ToolStripMenuItem.Name = "f32ToolStripMenuItem";
            this.f32ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.f32ToolStripMenuItem.Text = "f32";
            this.f32ToolStripMenuItem.Click += new System.EventHandler(this.f32ToolStripMenuItem_Click);
            // 
            // s32u32ToolStripMenuItem
            // 
            this.s32u32ToolStripMenuItem.Name = "s32u32ToolStripMenuItem";
            this.s32u32ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.s32u32ToolStripMenuItem.Text = "s32/u32";
            this.s32u32ToolStripMenuItem.Click += new System.EventHandler(this.s32u32ToolStripMenuItem_Click);
            // 
            // stringToolStripMenuItem
            // 
            this.stringToolStripMenuItem.Name = "stringToolStripMenuItem";
            this.stringToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.stringToolStripMenuItem.Text = "string";
            this.stringToolStripMenuItem.Click += new System.EventHandler(this.stringToolStripMenuItem_Click);
            // 
            // validHeaderContextMenu
            // 
            this.validHeaderContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportValuesAstxtFileToolStripMenuItem});
            this.validHeaderContextMenu.Name = "validHeaderContextMenu";
            this.validHeaderContextMenu.Size = new System.Drawing.Size(198, 26);
            // 
            // exportValuesAstxtFileToolStripMenuItem
            // 
            this.exportValuesAstxtFileToolStripMenuItem.Name = "exportValuesAstxtFileToolStripMenuItem";
            this.exportValuesAstxtFileToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportValuesAstxtFileToolStripMenuItem.Text = "Export values as .txt file";
            this.exportValuesAstxtFileToolStripMenuItem.Click += new System.EventHandler(this.exportValuesAstxtFileToolStripMenuItem_Click);
            // 
            // exportSelectionToolStripMenuItem
            // 
            this.exportSelectionToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.export;
            this.exportSelectionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportSelectionToolStripMenuItem.Name = "exportSelectionToolStripMenuItem";
            this.exportSelectionToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.exportSelectionToolStripMenuItem.Text = "Export selection...";
            this.exportSelectionToolStripMenuItem.ToolTipText = "Export the selected lines to a new BCSV file";
            this.exportSelectionToolStripMenuItem.Click += new System.EventHandler(this.exportSelectionToolStripMenuItem_Click);
            // 
            // importFromFileToolStripMenuItem
            // 
            this.importFromFileToolStripMenuItem.Image = global::HeavenTool.Properties.Resources.import;
            this.importFromFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importFromFileToolStripMenuItem.Name = "importFromFileToolStripMenuItem";
            this.importFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            this.importFromFileToolStripMenuItem.Text = "Import from file...";
            this.importFromFileToolStripMenuItem.ToolTipText = "Import lines from a BCSV file (headers must match)";
            this.importFromFileToolStripMenuItem.Click += new System.EventHandler(this.importFromFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // MainFrm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 335);
            this.Controls.Add(this.mainDataGridView);
            this.Controls.Add(this.statusStripMenu);
            this.Controls.Add(this.topMenuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "MainFrm";
            this.Text = "ACNH Heaven Tool | v1.0.0 | BCSV Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFrm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFrm_DragEnter);
            this.topMenuStrip.ResumeLayout(false);
            this.topMenuStrip.PerformLayout();
            this.statusStripMenu.ResumeLayout(false);
            this.statusStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            this.columnHeaderContextMenu.ResumeLayout(false);
            this.validHeaderContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private MenuStrip topMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private StatusStrip statusStripMenu;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private DataGridView mainDataGridView;
        private ToolStripMenuItem bruteForceMissingHashesToolStripMenuItem;
        private OpenFileDialog openBCSVFile;
        private ToolStripStatusLabel infoLabel;
        private ContextMenuStrip columnHeaderContextMenu;
        private ToolStripMenuItem searchOnFilesToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem newEntryToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem unloadFileToolStripMenuItem;
        private ToolStripMenuItem devToolStripMenuItem;
        private ToolStripMenuItem exportValidHashesToolStripMenuItem;
        private ToolStripMenuItem duplicateRowToolStripMenuItem;
        private ToolStripMenuItem hshCstringRefToolStripMenuItem;
        private ToolStripMenuItem f32ToolStripMenuItem;
        private ToolStripMenuItem s32u32ToolStripMenuItem;
        private ToolStripMenuItem stringToolStripMenuItem;
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
    }
}

