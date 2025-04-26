using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HeavenTool.Forms;
using System.Diagnostics;
using HeavenTool.Forms.Search;
using HeavenTool.Forms.Components;
using HeavenTool.IO.FileFormats.BCSV;
using ProgramAssociation = HeavenTool.Utility.ProgramAssociation;
using HeavenTool.IO;
using HeavenTool.Forms.BCSV.Controls;
using HeavenTool.Forms.BCSV.Templates;

namespace HeavenTool;

public partial class BCSVForm : Form, ISearchable
{
    private static readonly string originalFormName = "Heaven Tool - BCSV Editor";
    private BinaryCSV LoadedFile { get; set; }
    public BCSVForm()
    {
        InitializeComponent();

        // Dock dragInfo (cause having it docked by default makes everything hard when editing the form)
        dragInfo.Dock = DockStyle.Fill;
        dragInfo.AutoSize = false;

        // Initialize Hashes
        HashManager.InitializeHashes();
        ReloadInfo();

        validHeaderContextMenu.Renderer = new ToolStripProfessionalRenderer(new DarkColorTable());

        // smooth scrolling
        DrawingControl.SetDoubleBuffered(mainDataGridView);

        mainDataGridView.VirtualMode = true;
        mainDataGridView.CellValueNeeded += MainDataGridView_CellValueNeeded;
        mainDataGridView.CellFormatting += MainDataGridView_CellFormatting;
        mainDataGridView.CellValuePushed += MainDataGridView_CellValuePushed;

        mainDataGridView.SelectionChanged += MainDataGridView_SelectionChanged;

        mainDataGridView.ColumnHeaderMouseClick += MainDataGridView_ColumnHeaderMouseClick;
        mainDataGridView.ColumnStateChanged += MainDataGridView_ColumnStateChanged;
        mainDataGridView.RowPostPaint += MainDataGridView_RowPostPaint;
        mainDataGridView.RowHeadersWidth = 50;
        mainDataGridView.EditMode = DataGridViewEditMode.EditOnF2;
        
        versionNumberLabel.Text = Program.VERSION;
        Text = originalFormName;

        associateBcsvToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".bcsv") == Application.ExecutablePath;

        viewColumnsMenuItem.DropDownItemClicked += ViewColumnsMenuItem_DropDownItemClicked;
        viewColumnsMenuItem.DropDown.Closing += ViewColumnsMenuItem_DropDown_Closing;

        foreach (var dataType in Enum.GetValues<DataType>())
        {
            viewAsToolStripMenuItem.DropDown.Items.Add(new ToolStripMenuItem(dataType.ToString(), null, new EventHandler((_, _) =>
            {
                if (LoadedFile == null ||
                  lastSelectedColumn == -1 ||
                  lastSelectedColumn >= mainDataGridView.ColumnCount ||
                  mainDataGridView.Columns[lastSelectedColumn] is not IndexableDataGridColumn indexableDataGridColumn ||
                  indexableDataGridColumn.HeaderIndex >= LoadedFile.Fields.Length) return;

                var column = LoadedFile.Fields[indexableDataGridColumn.HeaderIndex];

                HashManager.AddOrEditForcedType(column.HEX, dataType);

                MessageBox.Show("Re-open your file to update values!");
            }))
            {
                ForeColor = Color.White
            });
        }
    }

    private void MainDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
        var grid = sender as DataGridView;
        var rowIdx = (e.RowIndex + 1).ToString();

        var centerFormat = new StringFormat()
        {
            // right alignment might actually make more sense for numbers
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
        e.Graphics.DrawString(rowIdx, this.Font, new SolidBrush(Color.FromArgb(110, 110, 110)), headerBounds, centerFormat);
    }

    public void ChangedRowCount(int count)
    {
        // reset ordering column (remove icon)
        ResetOrdering();

        reorderableIndexDictionary = [];

        for (int i = 0; i < count; i++)
            reorderableIndexDictionary.Add(i);

        mainDataGridView.RowCount = count;
        mainDataGridView.Invalidate();
    }

    private void MainDataGridView_SelectionChanged(object sender, EventArgs e)
    {
        if (mainDataGridView.SelectedRows.Count > 1)
            compareRowsToolStripMenuItem.Enabled = true;
        else
            compareRowsToolStripMenuItem.Enabled = false;

        ReloadInfo();
    }

    private void MainDataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
    {
        if (LoadedFile == null 
            || e.ColumnIndex >= mainDataGridView.ColumnCount 
            || e.RowIndex >= reorderableIndexDictionary.Count 
            || mainDataGridView.Columns[e.ColumnIndex] is not IndexableDataGridColumn indexableColumn) 
            return;

        var entryIndex = reorderableIndexDictionary[e.RowIndex];
        var fieldIndex = indexableColumn.HeaderIndex;

        if (entryIndex >= LoadedFile.Length || fieldIndex >= LoadedFile.Fields.Length) return;

        var targetField = LoadedFile.Fields[fieldIndex];
        LoadedFile.Entries[entryIndex][fieldIndex] = e.Value;

        if (mainDataGridView.SelectionMode == DataGridViewSelectionMode.CellSelect)
        {
            // update entries from selected cells, if DataType matches the targetField
            var selectedCells = mainDataGridView.SelectedCells;
            if (selectedCells.Count > 0)
            {
                foreach (DataGridViewCell selectedCell in selectedCells)
                {
                    if (selectedCell.RowIndex == e.RowIndex && selectedCell.ColumnIndex == e.ColumnIndex) continue; 

                    if (selectedCell.OwningColumn is not IndexableDataGridColumn cellColumn || 
                        cellColumn.HeaderIndex >= LoadedFile.Fields.Length) continue;

                    var selectedCellField = LoadedFile.Fields[cellColumn.HeaderIndex];

                    if (targetField.DataType != selectedCellField.DataType) continue;

                    if (selectedCell.RowIndex >= reorderableIndexDictionary.Count) return;
                    var selectedIndex = reorderableIndexDictionary[selectedCell.RowIndex];

                    LoadedFile[selectedIndex, selectedCellField] = e.Value;
                }
            }
        }
        else if (mainDataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
        {
            // update entries from the same column
            var selectedRows = mainDataGridView.SelectedRows;
            if (selectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in selectedRows)
                {
                    if (selectedRow.Index == e.RowIndex) continue;

                    if (selectedRow.Index >= reorderableIndexDictionary.Count) return;
                    var selectedIndex = reorderableIndexDictionary[selectedRow.Index];

                    LoadedFile.Entries[selectedIndex][indexableColumn.HeaderIndex] = e.Value;
                }
            }
        }
    }

    internal static (string, bool) GetFormattedValue(object[] values, int index, Field field)
    {
        if (field == null || values == null || index < 0 || index >= values.Length)
            return ("Invalid", false);

        var val = values[index];

        switch (field.DataType)
        {
            case DataType.CRC32:
                {
                    if (val is not uint hashValue) return ("Invalid", false);

                    var containsKey = HashManager.CRC32_Hashes.ContainsKey(hashValue);
                    return (containsKey ? HashManager.CRC32_Hashes[hashValue] : hashValue.ToString("x"), containsKey);
                }

            case DataType.MMH3:
                {
                    if (val is not uint hashValue) return ("Invalid", false);

                    var containsKey = HashManager.MMH3_Hashes.ContainsKey(hashValue);
                    return (containsKey ? HashManager.MMH3_Hashes[hashValue] : hashValue.ToString("x"), containsKey);
                }

            case DataType.U8Array:
                {
                    if (val is not byte[] bitField) return ("Invalid", false);

                    return (string.Join(" ", bitField), true);
                }

            default:
                return (val.ToString(), true);
        }
    }

    private void MainDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (LoadedFile == null || e.RowIndex >= reorderableIndexDictionary.Count || e.ColumnIndex >= mainDataGridView.ColumnCount) return;
        var entryIndex = reorderableIndexDictionary[e.RowIndex];

        if (entryIndex >= LoadedFile.Length || mainDataGridView.Columns[e.ColumnIndex] is not IndexableDataGridColumn column) return;

        var entry = LoadedFile.Entries[entryIndex];
        var field = LoadedFile.Fields[column.HeaderIndex];

        var (formattedValue, isFormatted) = GetFormattedValue(entry, e.ColumnIndex, field);

        if (formattedValue == "Invalid")
            e.CellStyle.BackColor = Color.Red;
        else if (!isFormatted && (field.DataType == DataType.CRC32 || field.DataType == DataType.MMH3))
            e.CellStyle.BackColor = Color.DarkRed;

        e.Value = formattedValue;
        e.FormattingApplied = true;
    }

    private List<int> reorderableIndexDictionary = [];
    public void OrderColumn(int columnIndex, SortOrder order)
    {
        if (LoadedFile == null) return;

        var column = mainDataGridView.Columns[columnIndex];
        if (column == null || column is not IndexableDataGridColumn indexableColumn) return;

        if (columnIndex < 0 || indexableColumn.HeaderIndex >= LoadedFile.Fields.Length) return;

        reorderableIndexDictionary.Sort(delegate (int x, int y)
        {
            if (order == SortOrder.None)
                return x.CompareTo(y);

            var entryX = LoadedFile.Entries[x][indexableColumn.HeaderIndex];
            var entryY = LoadedFile.Entries[y][indexableColumn.HeaderIndex];

            if (order == SortOrder.Ascending)
                return CompareObjects(entryX, entryY);
            else return CompareObjects(entryY, entryX);
        });
    }

    private static int CompareObjects(object entryX, object entryY)
    {
        // Checking both may be useless
        if (entryX is uint xUint && entryY is uint yUint)
            return xUint.CompareTo(yUint);

        else if (entryX is int xInt && entryY is int yInt)
            return xInt.CompareTo(yInt);

        else if (entryX is short xShort && entryY is short yShort)
            return xShort.CompareTo(yShort);

        else if (entryX is ushort xUshort && entryY is ushort yUshort)
            return xUshort.CompareTo(yUshort);

        else if (entryX is string xString && entryY is string yString)
            return xString.CompareTo(yString);

        else if (entryX is float xFloat && entryY is float yFloat)
            return xFloat.CompareTo(yFloat);

        else if (entryX is byte xByte && entryY is byte yByte)
            return xByte.CompareTo(yByte);

        else if (entryX is sbyte xSbyte && entryY is sbyte ySbyte)
            return xSbyte.CompareTo(ySbyte);

        else return entryX.ToString().CompareTo(entryY.ToString());
    }

    private void MainDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= LoadedFile.Length) return;

        if (reorderableIndexDictionary.Count == 0 || e.RowIndex >= reorderableIndexDictionary.Count) return;
        //Console.WriteLine($"Cell value needed {e.RowIndex}");
        var column = mainDataGridView.Columns[e.ColumnIndex];
        var originalIndex = reorderableIndexDictionary[e.RowIndex];

        if (column == null) return;
        if (column is not IndexableDataGridColumn indexableColumn) return;

        var entry = LoadedFile.Entries[originalIndex];

        e.Value = entry[indexableColumn.HeaderIndex];
    }

    private void ReloadInfo()
    {
        if (LoadedFile == null)
        {
            dragInfo.Visible = true;
            statusStripMenu.Visible = false;
            editToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            unloadFileToolStripMenuItem.Enabled = false;
            exportToCSVFileToolStripMenuItem.Enabled = false;
            importFromFileToolStripMenuItem.Enabled = false;
            exportSelectionToolStripMenuItem.Enabled = false;
            return;
        }

        var infos = new List<string> {
            $"CRC32 Hashes: {HashManager.CRC32_Hashes.Count} | Murmur Hashes: {HashManager.MMH3_Hashes.Count}"
        };

        if (mainDataGridView.Columns.Count > 0)
            infos.Add("Columns: " + mainDataGridView.Columns.Count);

        if (mainDataGridView.Columns.Count > 0)
            infos.Add("Rows: " + mainDataGridView.Rows.Count);

        if (mainDataGridView.CurrentCell != null)
            infos.Add("Selected RowId: " + mainDataGridView.CurrentCell.RowIndex);

        infoLabel.Text = string.Join(" | ", infos);

        dragInfo.Visible = false;
        statusStripMenu.Visible = true;
        editToolStripMenuItem.Enabled = true;
        saveAsToolStripMenuItem.Enabled = true;
        unloadFileToolStripMenuItem.Enabled = true;
        exportToCSVFileToolStripMenuItem.Enabled = true;
        importFromFileToolStripMenuItem.Enabled = true;
        exportSelectionToolStripMenuItem.Enabled = true;
    }

    private void ClearDataGrid()
    {
        Text = originalFormName;
        mainDataGridView.ClearSelection();
        mainDataGridView.Columns.Clear();
        ChangedRowCount(0);
        mainDataGridView.Refresh();

        ReloadInfo();
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openBCSVFile.ShowDialog(this);
    }

    private void OpenBCSVFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var path = openBCSVFile.FileName;

        if (!File.Exists(path)) return;

        LoadBCSVFile(path);
    }

    /// <summary>
    /// Handle path as an input; e.g. User double-clicked a .bcsv file or opened it with the editor.
    /// </summary>
    /// <param name="path">File path</param>
    public void HandleInput(string path)
    {
        var extension = Path.GetExtension(path);
        switch (extension)
        {
            case ".bcsv":
                LoadBCSVFile(path);
                break;
        }
    }

    private bool isUniqueColumnAlreadyFound = false;
    internal void LoadBCSVFile(string path)
    {
        ClearSearchCache();
        ClearDataGrid();

        using (var reader = File.OpenRead(path))
            LoadedFile = new BinaryCSV(reader.ToArray());

        if (LoadedFile == null) return;

        Text = $"{originalFormName}: {Path.GetFileName(path)}";

        isUniqueColumnAlreadyFound = false;
        viewColumnsMenuItem.DropDownItems.Clear();

        for (int fieldIndex = 0; fieldIndex < LoadedFile.Fields.Length; fieldIndex++)
        {
            var fieldHeader = LoadedFile.Fields[fieldIndex];
            var translatedName = fieldHeader.GetTranslatedNameOrNull();

            DataGridViewCell cellTemplate = fieldHeader.DataType switch
            {
                DataType.CRC32 => new CRC32DataGridCell(),
                DataType.MMH3 => new MMH3DataGridCell(),
                DataType.U8Array => new BitFieldDataGridCell() { 
                    FieldLength = fieldHeader.Size
                },

                _ => new DataGridViewTextBoxCell(),
            };

            string tooltip = (translatedName != null ? $"Name: {translatedName}\n" : "") +
                $"Hash: {fieldHeader.HEX}\n" +
                $"Type: {fieldHeader.DataType}\n" +
                $"Size: {fieldHeader.Size}";

            int columnId = mainDataGridView.Columns.Add(new IndexableDataGridColumn(fieldIndex)
            {
                Name = fieldHeader.HEX,
                HeaderText = fieldHeader.DisplayName,
                ValueType = fieldHeader.DataType.ToType(),
                CellTemplate = cellTemplate,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ToolTipText = tooltip
            });

            var column = mainDataGridView.Columns[columnId];

            if (fieldHeader.DataType == DataType.CRC32 || fieldHeader.DataType == DataType.MMH3)
                column.DefaultCellStyle.Font = new Font(mainDataGridView.Font, FontStyle.Bold);
            

            viewColumnsMenuItem.DropDownItems.Add(new ToolStripMenuItem()
            {
                Name = $"ViewColumn_{fieldHeader.HEX}",
                Text = fieldHeader.DisplayName,
                ForeColor = Color.White,
                CommandParameter = column.Name,
                Checked = column.Visible
            });

            if (fieldHeader.IsMissingHash)
            {
                if (fieldHeader.Size == 4)
                    column.HeaderCell.Style.BackColor = Color.PaleVioletRed;
                else
                    column.HeaderCell.Style.BackColor = Color.Orange;
            }

            if (!isUniqueColumnAlreadyFound && BinaryCSV.UniqueHashes.Contains(fieldHeader.Hash))
            {
                isUniqueColumnAlreadyFound = true;
                column.DisplayIndex = 0;
                column.HeaderCell.Style.BackColor = Color.DarkGreen;
                column.HeaderCell.Style.Font = new Font(mainDataGridView.Font, FontStyle.Bold);
            }
        }

        ChangedRowCount(LoadedFile.Length);

        ReloadInfo();
    }

    public void UnloadFile()
    {
        ClearDataGrid();
        LoadedFile.Dispose();
        LoadedFile = null;

        ReloadInfo();

        lastSelectedColumn = -1;
    }

    BCSVDirectorySearch directorySearchWindow;
    private void SearchOnFilesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Create directory search window if needed
        if (directorySearchWindow == null || directorySearchWindow.IsDisposed)
            directorySearchWindow = new BCSVDirectorySearch();

        directorySearchWindow.Show();
        directorySearchWindow.BringToFront();
    }

    private void UnloadFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Seems theres a memory leak on DataGridView
        // A possible solution is using mainDataGridView.DataSource with a System.Data.DataTable
        var result = MessageBox.Show("Do you really want to close this file?\nUnsaved changes will be lost!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            UnloadFile();
        }
    }

    private void NewEntryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null) return;

        if (LoadedFile.Length == 0)
        {
            var values = new object[LoadedFile.Fields.Length];

            for (int i = 0; i < LoadedFile.Fields.Length; i++)
            {
                values[i] = LoadedFile.Fields[i].GetFieldDefaultValue();
            }

            LoadedFile.Entries.Add(values);
        }
        else
        {
            var lastEntry = LoadedFile.Entries.Last();
            var newEntry = new object[lastEntry.Length];

            for (int i = 0; i < lastEntry.Length; i++)
                newEntry[i] = lastEntry[i];

            // TODO: Check UniqueId and assign one?
            LoadedFile.Entries.Add(newEntry);
        }

        ChangedRowCount(LoadedFile.Length);
        mainDataGridView.FirstDisplayedScrollingRowIndex = LoadedFile.Length - 1;

        ReloadInfo();
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile != null)
        {
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "BCSV (*.bcsv)|*.bcsv",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllBytes(saveFileDialog.FileName, LoadedFile.Save());
        }
    }

    private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
        if (LoadedFile == null)
        {
            newEntryToolStripMenuItem.Enabled = false;
            duplicateRowToolStripMenuItem.Enabled = false;
        }

        duplicateRowToolStripMenuItem.Enabled = mainDataGridView.SelectedRows.Count > 0;
        deleteRowsToolStripMenuItem.Enabled = mainDataGridView.SelectedRows.Count > 0;
    }

    private void DuplicateRowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null) return;

        foreach (DataGridViewRow selectedRow in mainDataGridView.SelectedRows)
        {
            if (selectedRow.Index >= LoadedFile.Length || selectedRow.Index >= reorderableIndexDictionary.Count) continue;

            var atualIndex = reorderableIndexDictionary[selectedRow.Index];
            var selectedEntry = LoadedFile.Entries[atualIndex];
            var newEntry = new object[selectedEntry.Length];

            for (var i = 0; i < selectedEntry.Length; i++)
                newEntry[i] = selectedEntry[i];

            // TODO: Check UniqueId and assign one?
            LoadedFile.Entries.Add(newEntry);

            ChangedRowCount(LoadedFile.Length);
        }

        ReloadInfo();
    }

    private void DeleteRowsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var count = mainDataGridView.SelectedRows.Count;
        var text = count > 1 ? $"these {count} entries" : "this entry";
        var result = MessageBox.Show($"Do you really want to delete {text}?\nThis action can't be un-done!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            var indexesToRemove = new List<int>();
            foreach (DataGridViewRow item in mainDataGridView.SelectedRows)
            {
                if (item.Index >= LoadedFile.Length || item.Index >= reorderableIndexDictionary.Count) continue;

                var atualIndex = reorderableIndexDictionary[item.Index];
                indexesToRemove.Add(atualIndex);
            }

            foreach (var index in indexesToRemove.OrderByDescending(x => x))
                LoadedFile.Entries.RemoveAt(index);

            ChangedRowCount(LoadedFile.Length);
        }

        ReloadInfo();
    }

    private int lastSelectedColumn = -1;

    private string sortColumn = null;
    private SortOrder sortOrder = SortOrder.None;
    public void ResetOrdering()
    {
        if (sortColumn != null && mainDataGridView.Columns.Contains(sortColumn))
            mainDataGridView.Columns[sortColumn].HeaderCell.SortGlyphDirection = SortOrder.None;

        sortColumn = null;
        sortOrder = SortOrder.None;
    }

    private void MainDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (LoadedFile == null || LoadedFile.Fields.Length <= e.ColumnIndex) return;

        if (e.Button == MouseButtons.Left)
        {
            string clickedColumn = mainDataGridView.Columns[e.ColumnIndex].Name;

            if (sortColumn != null && sortColumn != clickedColumn && mainDataGridView.Columns.Contains(sortColumn))
                mainDataGridView.Columns[sortColumn].HeaderCell.SortGlyphDirection = SortOrder.None;

            if (sortColumn == clickedColumn)
            {
                switch (sortOrder)
                {
                    case SortOrder.None:
                        sortOrder = SortOrder.Descending;
                        break;
                    case SortOrder.Descending:
                        sortOrder = SortOrder.Ascending;
                        break;
                    case SortOrder.Ascending:
                        sortOrder = SortOrder.None;
                        break;
                }
            }
            else
            {
                // A new column was clicked, set ascending order by default.
                sortColumn = clickedColumn;
                sortOrder = SortOrder.Descending;
            }

            mainDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = sortOrder;
            OrderColumn(e.ColumnIndex, sortOrder);

            mainDataGridView.Invalidate();
        }

        if (e.Button == MouseButtons.Right)
        {
            lastSelectedColumn = e.ColumnIndex;
            validHeaderContextMenu.Show(Cursor.Position);
        }
    }

    private void MainFrm_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((string[])e.Data.GetData(DataFormats.FileDrop)).Any(x => Path.GetExtension(x) == ".bcsv"))
            e.Effect = DragDropEffects.Move;

        else
            e.Effect = DragDropEffects.None;
    }

    private void MainFrm_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string filePath in fileList)
            {
                if (Path.GetExtension(filePath) == ".bcsv")
                {
                    LoadBCSVFile(filePath);
                    break;
                }
            }
        }
    }

    private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (LoadedFile != null)
        {
            var result = MessageBox.Show("Do you really want to close this file?\nUnsaved changes will be lost!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                UnloadFile();
            else e.Cancel = true;
        }
    }

    private void AssociateBCSVToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exePath = Application.ExecutablePath;
        var arguments = $"--{(associateBcsvToolStripMenuItem.Checked ? "disassociate" : "associate")} bcsv";

        var process = Process.Start(new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = arguments,
            Verb = "runas",
            UseShellExecute = true
        });

        process.WaitForExit();
        associateBcsvToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".bcsv") == Application.ExecutablePath;
    }


    private void ExportToCSVFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (mainDataGridView.Columns.Count == 0) return;

        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FilterIndex = 1,
            RestoreDirectory = true,
            OverwritePrompt = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            var sb = new StringBuilder();

            var headers = mainDataGridView.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => $"\"{column.HeaderText}\"").ToArray()));

            for (int i = 0; i < LoadedFile.Entries.Count; i++)
            {
                var row = LoadedFile.Entries[i];
                //var cells = row.Cells.Cast<DataGridViewCell>();
                //sb.AppendLine(string.Join(",", cells.Select(cell => $"\"{cell.Value}\"").ToArray()));
                sb.AppendLine(string.Join(",", row.Select(cell =>
                {
                    var parsedValue = $"\"{cell}\"";
                    var field = LoadedFile.Fields[i];

                    if (field == null) return parsedValue;

                    switch (field.DataType)
                    {
                        case DataType.CRC32:
                            {
                                if (cell is uint hashValue && HashManager.CRC32_Hashes.TryGetValue(hashValue, out string value))
                                    return value;

                                break;
                            }

                        case DataType.MMH3:
                            {
                                if (cell is uint hashValue && HashManager.MMH3_Hashes.TryGetValue(hashValue, out string value))
                                    return value;

                                break;
                            }
                    }


                    return parsedValue;
                }).ToArray()));

            }

            File.WriteAllText(saveFileDialog.FileName, sb.ToString());
        }
    }

    private void ExportValuesAstxtFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null ||
            lastSelectedColumn == -1 ||
            lastSelectedColumn >= mainDataGridView.ColumnCount ||
            mainDataGridView.Columns[lastSelectedColumn] is not IndexableDataGridColumn indexableDataGridColumn ||
            indexableDataGridColumn.HeaderIndex >= LoadedFile.Fields.Length) return;

        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt",
            FilterIndex = 1,
            RestoreDirectory = true,
            OverwritePrompt = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {

            var field = LoadedFile.Fields[indexableDataGridColumn.HeaderIndex];

            var exportedValues = new List<string>();
            foreach (var row in LoadedFile.Entries)
            {
                var cell = row[indexableDataGridColumn.HeaderIndex];
                if (field.DataType == DataType.CRC32)
                {
                    if (cell is uint hashValue)
                    {
                        var containsKey = HashManager.CRC32_Hashes.ContainsKey(hashValue);
                        if (containsKey)
                            exportedValues.AddIfNotExist(HashManager.CRC32_Hashes[hashValue]);
                        else
                            exportedValues.AddIfNotExist(hashValue.ToString("x"));

                        continue;
                    }
                } else if (field.DataType == DataType.MMH3)
                {
                    if (cell is uint hashValue)
                    {
                        var containsKey = HashManager.MMH3_Hashes.ContainsKey(hashValue);
                        if (containsKey)
                            exportedValues.AddIfNotExist(HashManager.MMH3_Hashes[hashValue]);
                        else
                            exportedValues.AddIfNotExist(hashValue.ToString("x"));

                        continue;
                    }
                }

                exportedValues.Add(cell.ToString());
            }

            File.WriteAllLines(saveFileDialog.FileName, exportedValues);
        }
    }


    private void CompareRowsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var compareWindow = new BCSVCompareWindow();
        compareWindow.compareDataGrid.Columns.Clear();

        foreach (DataGridViewColumn column in mainDataGridView.Columns)
        {
            var columnId = compareWindow.compareDataGrid.Columns.Add(column.Name, column.HeaderText);
            compareWindow.compareDataGrid.Columns[columnId].HeaderCell.Style = column.HeaderCell.Style;

        }

        compareWindow.compareDataGrid.Rows.Clear();
        foreach (DataGridViewRow row in mainDataGridView.SelectedRows)
        {
            var values = new List<object>();
            foreach (DataGridViewCell entry in row.Cells)
                values.Add(entry.FormattedValue);

            compareWindow.compareDataGrid.Rows.Add([.. values]);
        }

        compareWindow.ShowDialog();
    }


    private SearchBox searchBox;
    private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Create SearchBox Window if needed
        searchBox ??= new SearchBox(this)
        {
            StartPosition = FormStartPosition.CenterParent
        };

        searchBox.Show();

        RestoreSearchCache();
    }


    private string lastSearch = "";
    private bool lastCaseSensitive = false;
    private SearchType lastSearchType = SearchType.Contains;
    private int currentSearchIndex = -1;
    private DataGridViewCell[] searchCache;
    private DataGridViewCell lastSearchCell;
    private Dictionary<DataGridViewCell, Color> oldColorCache = null;

    public static readonly Color HIGHLIGHT_COLOR = Color.FromArgb(180, 180, 10);
    public static readonly Color HIGHLIGHT_COLOR_CURRENT = Color.YellowGreen;

    private void RestoreSearchCache()
    {
        // Create cache if doesn't exist
        oldColorCache ??= [];

        if (searchCache != null)
        {
            foreach (var cell in searchCache)
            {
                if (!oldColorCache.ContainsKey(cell) && cell.Style.BackColor != HIGHLIGHT_COLOR && cell.Style.BackColor != HIGHLIGHT_COLOR_CURRENT)
                    oldColorCache.Add(cell, cell.Style.BackColor);

                cell.Style.BackColor = cell == lastSearchCell ? HIGHLIGHT_COLOR_CURRENT : HIGHLIGHT_COLOR;
            }
        }
    }

    internal void ClearSearchCache()
    {
        if (searchCache != null && searchCache.Length > 0)
        {
            SearchClosing();

            lastSearchCell = null;
            searchCache = null;
        }
    }

    public void SearchClosing()
    {
        if (oldColorCache != null)
        {
            foreach (var cache in oldColorCache)
                cache.Key.Style.BackColor = cache.Value;

            oldColorCache.Clear();
            oldColorCache = null;
        }
    }

    public void Search(string search, SearchType searchType, bool searchBackwards, bool caseSensitive)
    {
        if (lastSearch != search || lastSearchType != searchType || lastCaseSensitive != caseSensitive)
        {
            lastSearch = search;
            lastSearchType = searchType;
            lastCaseSensitive = caseSensitive;

            ClearSearchCache();
        }

        // Create cache if doesn't exist
        oldColorCache ??= [];

        if (searchCache == null || searchCache.Length == 0)
        {

            if (!caseSensitive) search = search.ToLower();

            List<DataGridViewCell> cells = [];
            for (int columnIndex = 0; columnIndex < mainDataGridView.Columns.Count; columnIndex++)
            {
                if (mainDataGridView.Columns[columnIndex] is not IndexableDataGridColumn indexableDataGridColumn) continue;
                if (indexableDataGridColumn.HeaderIndex >= LoadedFile.Fields.Length) continue;

                var field = LoadedFile.Fields[indexableDataGridColumn.HeaderIndex];

                for (int rowIndex = 0; rowIndex < mainDataGridView.Rows.Count; rowIndex++)
                {
                    var actualIndex = reorderableIndexDictionary[rowIndex];
                    var (formattedValue, isFormatted) = GetFormattedValue(LoadedFile.Entries[actualIndex], indexableDataGridColumn.HeaderIndex, field);

                    if (!isFormatted) continue;

                    if (!caseSensitive) formattedValue = formattedValue.ToLower();

                    if ((searchType == SearchType.Contains && formattedValue.Contains(search)) ||
                        (searchType == SearchType.Exactly && formattedValue == search))
                        cells.Add(mainDataGridView[columnIndex, rowIndex]);
                    

                }
            }

            searchCache = [.. cells];

            if (searchCache.Length > 1 && searchBackwards)
                currentSearchIndex = searchCache.Length - 1;
            else
                currentSearchIndex = 0;

            searchBox.UpdateMatchesFound(searchCache.Length, currentSearchIndex);

            if (searchCache.Length == 0)
            {
                MessageBox.Show("No matches found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                foreach (var current in searchCache)
                {
                    if (!oldColorCache.ContainsKey(current) && current.Style.BackColor != HIGHLIGHT_COLOR && current.Style.BackColor != HIGHLIGHT_COLOR_CURRENT)
                        oldColorCache.Add(current, current.Style.BackColor);

                    current.Style.BackColor = HIGHLIGHT_COLOR;
                }
            }

        }


        // If its out of bounds, return to start (in case o backwards return to the last one)
        if (currentSearchIndex >= searchCache.Length || currentSearchIndex < 0)
        {
            currentSearchIndex = searchBackwards ? searchCache.Length - 1 : 0;
        }

        // Safe check, don't should occur but checking again doesn't hurt
        if (searchCache.Length > 0 && currentSearchIndex < searchCache.Length && currentSearchIndex >= 0)
        {
            if (lastSearchCell != null)
            {
                if (!oldColorCache.ContainsKey(lastSearchCell) && lastSearchCell.Style.BackColor != HIGHLIGHT_COLOR && lastSearchCell.Style.BackColor != HIGHLIGHT_COLOR_CURRENT)
                    oldColorCache.Add(lastSearchCell, lastSearchCell.Style.BackColor);

                lastSearchCell.Style.BackColor = HIGHLIGHT_COLOR;
            }

            searchBox.UpdateMatchesFound(searchCache.Length, currentSearchIndex);

            var current = searchCache[currentSearchIndex];

            mainDataGridView.FirstDisplayedScrollingColumnIndex = current.ColumnIndex;
            mainDataGridView.FirstDisplayedScrollingRowIndex = current.RowIndex;

            lastSearchCell = current;

            if (!oldColorCache.ContainsKey(lastSearchCell) && lastSearchCell.Style.BackColor != HIGHLIGHT_COLOR && lastSearchCell.Style.BackColor != HIGHLIGHT_COLOR_CURRENT)
                oldColorCache.Add(lastSearchCell, lastSearchCell.Style.BackColor);

            lastSearchCell.Style.BackColor = Color.YellowGreen;

            if (searchBackwards)
            {
                if (currentSearchIndex == 0)
                    currentSearchIndex = searchCache.Length - 1;
                else
                    currentSearchIndex--;
            }
            else
            {
                if (currentSearchIndex >= searchCache.Length)
                    currentSearchIndex = 0;
                else
                    currentSearchIndex++;
            }
        }
    }

    private void ExportSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null) return;

        var newFile = BinaryCSV.CopyFileWithoutEntries(LoadedFile);

        var selectedRows = new DataGridViewRow[mainDataGridView.SelectedRows.Count];
        mainDataGridView.SelectedRows.CopyTo(selectedRows, 0);

        newFile.Entries = LoadedFile.Entries
            .Where((_, index) => selectedRows.Any(y => y.Index < reorderableIndexDictionary.Count && reorderableIndexDictionary[y.Index] == index
        )).ToList();

        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "BCSV (*.bcsv)|*.bcsv",
            FilterIndex = 1,
            RestoreDirectory = true,
            OverwritePrompt = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
            File.WriteAllBytes(saveFileDialog.FileName, newFile.Save());
    }

    private void ImportFromFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog()
        {
            Title = "Select a BCSV to import",
            Filter = "BCSV|*.bcsv",
            DefaultExt = "*.bcsv"
        };

        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            var file = openFileDialog.FileName;

            using var reader = File.OpenRead(file);
            var bcsvToCopy = new BinaryCSV(reader.ToArray());

            var haveDifferentField = LoadedFile.Fields.Length != bcsvToCopy.Fields.Length;
            if (!haveDifferentField)
            {
                foreach (var field in LoadedFile.Fields)
                    if (!LoadedFile.Fields.Contains(field))
                    {
                        haveDifferentField = true;
                        break;
                    }
            }

            if (haveDifferentField)
            {
                MessageBox.Show("The file you're trying to import have different field headers.", "Failed to import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lastEntry = -1;
            foreach (var entry in bcsvToCopy.Entries)
            {
                LoadedFile.Entries.Add(entry);
                ChangedRowCount(LoadedFile.Length);
                //lastEntry = mainDataGridView.Rows.Add([.. entry.Values]);
            }

            if (lastEntry != -1)
                mainDataGridView.FirstDisplayedScrollingRowIndex = lastEntry;

        }
    }

    private void HeaderNameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null ||
                   lastSelectedColumn == -1 ||
                   lastSelectedColumn >= mainDataGridView.ColumnCount ||
                   mainDataGridView.Columns[lastSelectedColumn] is not IndexableDataGridColumn indexableDataGridColumn ||
                   indexableDataGridColumn.HeaderIndex >= LoadedFile.Fields.Length) return;

        var field = LoadedFile.Fields[indexableDataGridColumn.HeaderIndex];
        Clipboard.SetText(HashManager.CRC32_Hashes.TryGetValue(field.Hash, out string value) ? value : $"0x{field.Hash:x}");
    }

    private void HeaderHashToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null ||
                  lastSelectedColumn == -1 ||
                  lastSelectedColumn >= mainDataGridView.ColumnCount ||
                  mainDataGridView.Columns[lastSelectedColumn] is not IndexableDataGridColumn indexableDataGridColumn ||
                  indexableDataGridColumn.HeaderIndex >= LoadedFile.Fields.Length) return;

        var field = LoadedFile.Fields[indexableDataGridColumn.HeaderIndex];

        Clipboard.SetText($"0x{field.Hash:x}");
    }

    #region column visibility
    private void HideColumnToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1 ||
            lastSelectedColumn >= mainDataGridView.ColumnCount) return;

        mainDataGridView.Columns[lastSelectedColumn].Visible = false;
    }

    private void MainDataGridView_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
    {
        if (e.StateChanged == DataGridViewElementStates.Visible)
        {
            string key = $"ViewColumn_{e.Column.Name}";

            if (viewColumnsMenuItem.DropDownItems.ContainsKey(key) && viewColumnsMenuItem.DropDownItems[key] is ToolStripMenuItem item)
                item.Checked = e.Column.Visible;
        }
    }

    private void ViewColumnsMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
        if (e.ClickedItem == null || e.ClickedItem is not ToolStripMenuItem menuItem) return;
        if (menuItem.CommandParameter == null || menuItem.CommandParameter is not string columnName) return;

        if (mainDataGridView.Columns.Contains(columnName))
            mainDataGridView.Columns[columnName].Visible = !mainDataGridView.Columns[columnName].Visible;

    }

    private void ViewColumnsMenuItem_DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
    {
        if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            e.Cancel = true;
    }
    #endregion

    private void RowSelectToolStripMenuItem_Click(object sender, EventArgs e)
    {
        mainDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        rowSelectToolStripMenuItem.Checked = true;
        cellSelectToolStripMenuItem.Checked = false;
    }

    private void CellSelectToolStripMenuItem_Click(object sender, EventArgs e)
    {
        mainDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
        rowSelectToolStripMenuItem.Checked = false;
        cellSelectToolStripMenuItem.Checked = true;
    }
}