using HeavenTool.Forms.Components;
using System.Windows.Forms;

namespace HeavenTool
{
    partial class BCSVRework
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCSVRework));
            topMenuStrip = new DarkMenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            importFromFileToolStripMenuItem = new ToolStripMenuItem();
            exportSelectionToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exportToCSVFileToolStripMenuItem = new ToolStripMenuItem();
            unloadFileToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            newEntryToolStripMenuItem = new ToolStripMenuItem();
            duplicateRowToolStripMenuItem = new ToolStripMenuItem();
            deleteRowsToolStripMenuItem = new ToolStripMenuItem();
            compareRowsToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            viewColumnsMenuItem = new ToolStripMenuItem();
            devToolStripMenuItem = new ToolStripMenuItem();
            associateBcsvToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            searchOnFilesToolStripMenuItem = new ToolStripMenuItem();
            statusStripMenu = new StatusStrip();
            infoLabel = new ToolStripStatusLabel();
            versionNumberLabel = new ToolStripStatusLabel();
            mainDataGridView = new DataGridView();
            openBCSVFile = new OpenFileDialog();
            validHeaderContextMenu = new ContextMenuStrip(components);
            copyToolStripMenuItem = new ToolStripMenuItem();
            headerNameToolStripMenuItem = new ToolStripMenuItem();
            headerHashToolStripMenuItem = new ToolStripMenuItem();
            exportValuesAstxtFileToolStripMenuItem = new ToolStripMenuItem();
            viewAsToolStripMenuItem = new ToolStripMenuItem();
            s32u32ToolStripMenuItem = new ToolStripMenuItem();
            int32ToolStripMenuItem = new ToolStripMenuItem();
            f32ToolStripMenuItem = new ToolStripMenuItem();
            stringToolStripMenuItem = new ToolStripMenuItem();
            hshCstringRefToolStripMenuItem = new ToolStripMenuItem();
            murmurHashToolStripMenuItem = new ToolStripMenuItem();
            dragInfo = new Label();
            topMenuStrip.SuspendLayout();
            statusStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainDataGridView).BeginInit();
            validHeaderContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // topMenuStrip
            // 
            topMenuStrip.BackColor = System.Drawing.Color.Transparent;
            topMenuStrip.ForeColor = System.Drawing.Color.White;
            topMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, devToolStripMenuItem, toolsToolStripMenuItem });
            topMenuStrip.Location = new System.Drawing.Point(0, 0);
            topMenuStrip.Name = "topMenuStrip";
            topMenuStrip.Size = new System.Drawing.Size(684, 24);
            topMenuStrip.TabIndex = 1;
            topMenuStrip.Text = "Top Menu";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator2, importFromFileToolStripMenuItem, exportSelectionToolStripMenuItem, toolStripSeparator1, exportToCSVFileToolStripMenuItem, unloadFileToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            openToolStripMenuItem.Image = Properties.Resources.open_file;
            openToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            saveAsToolStripMenuItem.Image = Properties.Resources.save_as;
            saveAsToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            saveAsToolStripMenuItem.Text = "Save as...";
            saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // importFromFileToolStripMenuItem
            // 
            importFromFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            importFromFileToolStripMenuItem.Image = Properties.Resources.import;
            importFromFileToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            importFromFileToolStripMenuItem.Name = "importFromFileToolStripMenuItem";
            importFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            importFromFileToolStripMenuItem.Text = "Import from file...";
            importFromFileToolStripMenuItem.ToolTipText = "Import lines from a BCSV file (headers must match)";
            importFromFileToolStripMenuItem.Click += ImportFromFileToolStripMenuItem_Click;
            // 
            // exportSelectionToolStripMenuItem
            // 
            exportSelectionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportSelectionToolStripMenuItem.Image = Properties.Resources.export;
            exportSelectionToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            exportSelectionToolStripMenuItem.Name = "exportSelectionToolStripMenuItem";
            exportSelectionToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            exportSelectionToolStripMenuItem.Text = "Export selection...";
            exportSelectionToolStripMenuItem.ToolTipText = "Export the selected lines to a new BCSV file";
            exportSelectionToolStripMenuItem.Click += ExportSelectionToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // exportToCSVFileToolStripMenuItem
            // 
            exportToCSVFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportToCSVFileToolStripMenuItem.Image = Properties.Resources.export_csv;
            exportToCSVFileToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            exportToCSVFileToolStripMenuItem.Name = "exportToCSVFileToolStripMenuItem";
            exportToCSVFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            exportToCSVFileToolStripMenuItem.Text = "Export as .CSV";
            exportToCSVFileToolStripMenuItem.Click += ExportToCSVFileToolStripMenuItem_Click;
            // 
            // unloadFileToolStripMenuItem
            // 
            unloadFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            unloadFileToolStripMenuItem.Image = Properties.Resources.cancel;
            unloadFileToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            unloadFileToolStripMenuItem.Name = "unloadFileToolStripMenuItem";
            unloadFileToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
            unloadFileToolStripMenuItem.Text = "Unload File";
            unloadFileToolStripMenuItem.Click += UnloadFileToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { searchToolStripMenuItem, newEntryToolStripMenuItem, duplicateRowToolStripMenuItem, deleteRowsToolStripMenuItem, compareRowsToolStripMenuItem });
            editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.DropDownOpening += EditToolStripMenuItem_DropDownOpening;
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            searchToolStripMenuItem.Image = Properties.Resources.search;
            searchToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            searchToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            searchToolStripMenuItem.Text = "Search";
            searchToolStripMenuItem.ToolTipText = "Search for a line on this file";
            searchToolStripMenuItem.Click += SearchToolStripMenuItem_Click;
            // 
            // newEntryToolStripMenuItem
            // 
            newEntryToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            newEntryToolStripMenuItem.Image = Properties.Resources.new_entry;
            newEntryToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            newEntryToolStripMenuItem.Name = "newEntryToolStripMenuItem";
            newEntryToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newEntryToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            newEntryToolStripMenuItem.Text = "New Entry";
            newEntryToolStripMenuItem.ToolTipText = "Add a new entry into this file";
            newEntryToolStripMenuItem.Click += NewEntryToolStripMenuItem_Click;
            // 
            // duplicateRowToolStripMenuItem
            // 
            duplicateRowToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            duplicateRowToolStripMenuItem.Image = Properties.Resources.duplicate;
            duplicateRowToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            duplicateRowToolStripMenuItem.Name = "duplicateRowToolStripMenuItem";
            duplicateRowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            duplicateRowToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            duplicateRowToolStripMenuItem.Text = "Duplicate Row(s)";
            duplicateRowToolStripMenuItem.ToolTipText = "Duplicate the selected row(s)";
            duplicateRowToolStripMenuItem.Click += DuplicateRowToolStripMenuItem_Click;
            // 
            // deleteRowsToolStripMenuItem
            // 
            deleteRowsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            deleteRowsToolStripMenuItem.Image = Properties.Resources.remove;
            deleteRowsToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            deleteRowsToolStripMenuItem.Name = "deleteRowsToolStripMenuItem";
            deleteRowsToolStripMenuItem.ShortcutKeys = Keys.Delete;
            deleteRowsToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            deleteRowsToolStripMenuItem.Text = "Delete Row(s)";
            deleteRowsToolStripMenuItem.ToolTipText = "Delete the selected row(s)";
            deleteRowsToolStripMenuItem.Click += DeleteRowsToolStripMenuItem_Click;
            // 
            // compareRowsToolStripMenuItem
            // 
            compareRowsToolStripMenuItem.Enabled = false;
            compareRowsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            compareRowsToolStripMenuItem.Image = Properties.Resources.compare;
            compareRowsToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            compareRowsToolStripMenuItem.Name = "compareRowsToolStripMenuItem";
            compareRowsToolStripMenuItem.Size = new System.Drawing.Size(213, 30);
            compareRowsToolStripMenuItem.Text = "Compare Rows";
            compareRowsToolStripMenuItem.ToolTipText = "Compare selected rows in a new window (read-only)";
            compareRowsToolStripMenuItem.Click += CompareRowsToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewColumnsMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // viewColumnsMenuItem
            // 
            viewColumnsMenuItem.ForeColor = System.Drawing.Color.White;
            viewColumnsMenuItem.Name = "viewColumnsMenuItem";
            viewColumnsMenuItem.Size = new System.Drawing.Size(180, 22);
            viewColumnsMenuItem.Text = "View Columns";
            // 
            // devToolStripMenuItem
            // 
            devToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { associateBcsvToolStripMenuItem });
            devToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            devToolStripMenuItem.Name = "devToolStripMenuItem";
            devToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            devToolStripMenuItem.Text = "Options";
            // 
            // associateBcsvToolStripMenuItem
            // 
            associateBcsvToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            associateBcsvToolStripMenuItem.Name = "associateBcsvToolStripMenuItem";
            associateBcsvToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            associateBcsvToolStripMenuItem.Text = "Associate .BCSV files with this program";
            associateBcsvToolStripMenuItem.Click += AssociateBCSVToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { searchOnFilesToolStripMenuItem });
            toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // searchOnFilesToolStripMenuItem
            // 
            searchOnFilesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            searchOnFilesToolStripMenuItem.Image = Properties.Resources.search;
            searchOnFilesToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            searchOnFilesToolStripMenuItem.Name = "searchOnFilesToolStripMenuItem";
            searchOnFilesToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            searchOnFilesToolStripMenuItem.Text = "Search on files";
            searchOnFilesToolStripMenuItem.Click += SearchOnFilesToolStripMenuItem_Click;
            // 
            // statusStripMenu
            // 
            statusStripMenu.BackColor = System.Drawing.Color.Transparent;
            statusStripMenu.Items.AddRange(new ToolStripItem[] { infoLabel, versionNumberLabel });
            statusStripMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStripMenu.Location = new System.Drawing.Point(0, 365);
            statusStripMenu.Name = "statusStripMenu";
            statusStripMenu.Size = new System.Drawing.Size(684, 22);
            statusStripMenu.SizingGrip = false;
            statusStripMenu.TabIndex = 2;
            // 
            // infoLabel
            // 
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new System.Drawing.Size(28, 17);
            infoLabel.Text = "Info";
            // 
            // versionNumberLabel
            // 
            versionNumberLabel.Alignment = ToolStripItemAlignment.Right;
            versionNumberLabel.Name = "versionNumberLabel";
            versionNumberLabel.Size = new System.Drawing.Size(31, 17);
            versionNumberLabel.Text = "1.0.0";
            // 
            // mainDataGridView
            // 
            mainDataGridView.AllowUserToAddRows = false;
            mainDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            mainDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            mainDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(60, 60, 60);
            mainDataGridView.BorderStyle = BorderStyle.None;
            mainDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            mainDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            mainDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            mainDataGridView.ColumnHeadersHeight = 25;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            mainDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            mainDataGridView.Dock = DockStyle.Fill;
            mainDataGridView.EnableHeadersVisualStyles = false;
            mainDataGridView.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            mainDataGridView.Location = new System.Drawing.Point(0, 24);
            mainDataGridView.Margin = new Padding(4, 3, 4, 3);
            mainDataGridView.Name = "mainDataGridView";
            mainDataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            mainDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            mainDataGridView.RowHeadersWidth = 25;
            mainDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            mainDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            mainDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mainDataGridView.Size = new System.Drawing.Size(684, 341);
            mainDataGridView.StandardTab = true;
            mainDataGridView.TabIndex = 4;
            // 
            // openBCSVFile
            // 
            openBCSVFile.DefaultExt = "*.bcsv";
            openBCSVFile.Filter = "BCSV|*.bcsv";
            openBCSVFile.Title = "Select a BCSV file...";
            openBCSVFile.FileOk += OpenBCSVFile_FileOk;
            // 
            // validHeaderContextMenu
            // 
            validHeaderContextMenu.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            validHeaderContextMenu.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem, exportValuesAstxtFileToolStripMenuItem, viewAsToolStripMenuItem });
            validHeaderContextMenu.Name = "validHeaderContextMenu";
            validHeaderContextMenu.Size = new System.Drawing.Size(196, 70);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { headerNameToolStripMenuItem, headerHashToolStripMenuItem });
            copyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            copyToolStripMenuItem.Text = "Copy";
            // 
            // headerNameToolStripMenuItem
            // 
            headerNameToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            headerNameToolStripMenuItem.Name = "headerNameToolStripMenuItem";
            headerNameToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            headerNameToolStripMenuItem.Text = "Header Name";
            headerNameToolStripMenuItem.Click += HeaderNameToolStripMenuItem_Click;
            // 
            // headerHashToolStripMenuItem
            // 
            headerHashToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            headerHashToolStripMenuItem.Name = "headerHashToolStripMenuItem";
            headerHashToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            headerHashToolStripMenuItem.Text = "Header Hash";
            headerHashToolStripMenuItem.Click += HeaderHashToolStripMenuItem_Click;
            // 
            // exportValuesAstxtFileToolStripMenuItem
            // 
            exportValuesAstxtFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            exportValuesAstxtFileToolStripMenuItem.Name = "exportValuesAstxtFileToolStripMenuItem";
            exportValuesAstxtFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            exportValuesAstxtFileToolStripMenuItem.Text = "Export values as .txt file";
            exportValuesAstxtFileToolStripMenuItem.Click += ExportValuesAstxtFileToolStripMenuItem_Click;
            // 
            // viewAsToolStripMenuItem
            // 
            viewAsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { s32u32ToolStripMenuItem, int32ToolStripMenuItem, f32ToolStripMenuItem, stringToolStripMenuItem, hshCstringRefToolStripMenuItem, murmurHashToolStripMenuItem });
            viewAsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            viewAsToolStripMenuItem.Name = "viewAsToolStripMenuItem";
            viewAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            viewAsToolStripMenuItem.Text = "View as";
            // 
            // s32u32ToolStripMenuItem
            // 
            s32u32ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            s32u32ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            s32u32ToolStripMenuItem.Name = "s32u32ToolStripMenuItem";
            s32u32ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            s32u32ToolStripMenuItem.Text = "UInt";
            s32u32ToolStripMenuItem.Click += S32u32ToolStripMenuItem_Click;
            // 
            // int32ToolStripMenuItem
            // 
            int32ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            int32ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            int32ToolStripMenuItem.Name = "int32ToolStripMenuItem";
            int32ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            int32ToolStripMenuItem.Text = "Int";
            int32ToolStripMenuItem.Click += Int32ToolStripMenuItem_Click;
            // 
            // f32ToolStripMenuItem
            // 
            f32ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            f32ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            f32ToolStripMenuItem.Name = "f32ToolStripMenuItem";
            f32ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            f32ToolStripMenuItem.Text = "Float";
            f32ToolStripMenuItem.Click += F32ToolStripMenuItem_Click;
            // 
            // stringToolStripMenuItem
            // 
            stringToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            stringToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            stringToolStripMenuItem.Name = "stringToolStripMenuItem";
            stringToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            stringToolStripMenuItem.Text = "String";
            stringToolStripMenuItem.Click += StringToolStripMenuItem_Click;
            // 
            // hshCstringRefToolStripMenuItem
            // 
            hshCstringRefToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            hshCstringRefToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            hshCstringRefToolStripMenuItem.Name = "hshCstringRefToolStripMenuItem";
            hshCstringRefToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            hshCstringRefToolStripMenuItem.Text = "Crc32";
            hshCstringRefToolStripMenuItem.Click += HshCstringRefToolStripMenuItem_Click;
            // 
            // murmurHashToolStripMenuItem
            // 
            murmurHashToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            murmurHashToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            murmurHashToolStripMenuItem.Name = "murmurHashToolStripMenuItem";
            murmurHashToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            murmurHashToolStripMenuItem.Text = "Murmur3";
            murmurHashToolStripMenuItem.Click += MurmurHashToolStripMenuItem_Click;
            // 
            // dragInfo
            // 
            dragInfo.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dragInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            dragInfo.ForeColor = System.Drawing.Color.White;
            dragInfo.Location = new System.Drawing.Point(14, 291);
            dragInfo.Margin = new Padding(4, 0, 4, 0);
            dragInfo.Name = "dragInfo";
            dragInfo.Size = new System.Drawing.Size(212, 60);
            dragInfo.TabIndex = 5;
            dragInfo.Text = "Drag a file here\r\nor use File > Open";
            dragInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BCSVRework
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(684, 387);
            Controls.Add(dragInfo);
            Controls.Add(mainDataGridView);
            Controls.Add(statusStripMenu);
            Controls.Add(topMenuStrip);
            ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = topMenuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "BCSVRework";
            Text = "ACNH Heaven Tool | v1.0.0 | BCSV Editor";
            FormClosing += MainFrm_FormClosing;
            DragDrop += MainFrm_DragDrop;
            DragEnter += MainFrm_DragEnter;
            topMenuStrip.ResumeLayout(false);
            topMenuStrip.PerformLayout();
            statusStripMenu.ResumeLayout(false);
            statusStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainDataGridView).EndInit();
            validHeaderContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem duplicateRowToolStripMenuItem;
        private ToolStripMenuItem associateBcsvToolStripMenuItem;
        private ToolStripMenuItem deleteRowsToolStripMenuItem;
        private ContextMenuStrip validHeaderContextMenu;
        private ToolStripMenuItem exportValuesAstxtFileToolStripMenuItem;
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
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem viewColumnsMenuItem;
    }
}

