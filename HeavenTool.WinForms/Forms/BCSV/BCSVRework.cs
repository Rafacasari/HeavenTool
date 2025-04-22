using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HeavenTool.Forms;
using System.Diagnostics;
using HeavenTool.DataTable;
using HeavenTool.Utility;
using HeavenTool.Utility.IO;
using HeavenTool.Forms.Search;
using HeavenTool.Forms.Components;
using HeavenTool.Forms.BCSV.Templates;
using HeavenTool.IO.FileFormats.BCSV;
using BCSVHashing = HeavenTool.Utility.FileTypes.BCSV.BCSVHashing;
using HeavenTool.IO;

namespace HeavenTool;

public partial class BCSVRework : Form, ISearchable
{
    public static Dictionary<uint, string> CRCHashes => BCSVHashing.CRCHashes;
    public static Dictionary<uint, string> MurmurHashes => BCSVHashing.MurmurHashes;

    private static readonly string OriginalFormName = "Heaven Tool - New BCSV Editor";
    private BinaryCSV LoadedFile { get; set; }
    public BCSVRework()
    {
        InitializeComponent();

        // Dock dragInfo (cause having it docked by default makes everything hard when editing the form)
        dragInfo.Dock = DockStyle.Fill;
        dragInfo.AutoSize = false;

        // Initialize Hashes
        BCSVHashing.InitializeHashes();
        ReloadInfo();

        KnownHashValueManager.Load();

        validHeaderContextMenu.Renderer = new ToolStripProfessionalRenderer(new DarkColorTable());

        // Fixes a visual glitch when scrolling too fast
        DrawingControl.SetDoubleBuffered(mainDataGridView);

        mainDataGridView.VirtualMode = true;
        mainDataGridView.CellValueNeeded += MainDataGridView_CellValueNeeded;
        mainDataGridView.CellFormatting += MainDataGridView_CellFormatting;
        mainDataGridView.CellValuePushed += MainDataGridView_CellValuePushed;

        mainDataGridView.CellBeginEdit += MainDataGridView_CellBeginEdit;
        mainDataGridView.CellEndEdit += MainDataGridView_CellEndEdit;
        mainDataGridView.SelectionChanged += MainDataGridView_SelectionChanged;

        versionNumberLabel.Text = Program.VERSION;
        Text = OriginalFormName;

        associateBcsvToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".bcsv") == Application.ExecutablePath;
    }


    bool waitingForNextSelection = false;
    private void MainDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        mainDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        waitingForNextSelection = true;
    }

    private void MainDataGridView_SelectionChanged(object sender, EventArgs e)
    {
        if (waitingForNextSelection)
        {
            waitingForNextSelection = false;

            mainDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mainDataGridView.ClearSelection();
            mainDataGridView.CurrentCell = selectedCellOnEdit;

            foreach (DataGridViewCell dataGridViewCell in mainDataGridView.SelectedCells)
                dataGridViewCell.Selected = false;

            foreach (DataGridViewRow current in selectedRowsOnEdit)
                current.Selected = true;

            selectedRowsOnEdit = null;
            selectedCellOnEdit = null;
        }

        if (mainDataGridView.SelectedRows.Count > 1)
            compareRowsToolStripMenuItem.Enabled = true;
        else
            compareRowsToolStripMenuItem.Enabled = false;
    }

    private DataGridViewCell selectedCellOnEdit;
    private DataGridViewSelectedRowCollection selectedRowsOnEdit;
    private void MainDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        selectedCellOnEdit = mainDataGridView[e.ColumnIndex, e.RowIndex];
        selectedRowsOnEdit = mainDataGridView.SelectedRows;
        var currentCell = mainDataGridView.CurrentCell.ColumnIndex;

        mainDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;

        foreach (DataGridViewRow current in selectedRowsOnEdit) 
        {
            mainDataGridView[currentCell, current.Index].Selected = true;
        }
    }

    internal static string GetFormattedValue(object[] values, int index, Field field)
    {
        if (field == null || values == null || index < 0 || index > values.Length)
            return "Invalid";

        var val = values[index];

        switch (field.DataType)
        {
            case DataType.CRC32:
                {
                    if (val is not uint hashValue) return "Invalid";

                    var containsKey = BCSVHashing.CRCHashes.ContainsKey(hashValue);
                    return containsKey ? BCSVHashing.CRCHashes[hashValue] : hashValue.ToString("x");
                }

            case DataType.MMH3:
                {
                    if (val is not uint hashValue) return "Invalid";

                    var containsKey = BCSVHashing.MurmurHashes.ContainsKey(hashValue);
                    return containsKey ? BCSVHashing.MurmurHashes[hashValue] : hashValue.ToString("x");
                }

            default:
                return val.ToString();
        }
    }

    private void MainDataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
    {
        if (LoadedFile == null) return;

        if (e.RowIndex < 0 || e.RowIndex >= LoadedFile.Length)
            return;

        Console.WriteLine($"Pushed update to LoadedFile[{e.RowIndex}][{e.ColumnIndex}]");
        LoadedFile.Entries[e.RowIndex][e.ColumnIndex] = e.Value;

        var selectedCells = mainDataGridView.SelectedCells;
        if (selectedCells.Count > 0)
        {
            Console.WriteLine($"Pushing values to {selectedCells.Count} selected cells");
            foreach (DataGridViewCell selectedCell in selectedCells)
                if (selectedCell.ColumnIndex == e.ColumnIndex && selectedCell.RowIndex != e.RowIndex)
                    LoadedFile.Entries[selectedCell.RowIndex][e.ColumnIndex] = e.Value;
        }
    }

    private void MainDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (LoadedFile == null) return;

        if (e.RowIndex < 0 || e.RowIndex >= LoadedFile.Length)
            return;

        var entry = LoadedFile.Entries[e.RowIndex];
        var field = LoadedFile.Fields[e.ColumnIndex];

        var formattedValue = GetFormattedValue(entry, e.ColumnIndex, field);

        if (formattedValue == "Invalid")
            e.CellStyle.BackColor = Color.Red;

        e.Value = formattedValue;
        e.FormattingApplied = true;
        
    }

    private void MainDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= LoadedFile.Length)
            return;

        var entry = LoadedFile.Entries[e.RowIndex];

        e.Value = entry[e.ColumnIndex];
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
            $"CRC32 Hashes: {BCSVHashing.CRCHashes.Count} | Murmur Hashes: {BCSVHashing.MurmurHashes.Count}"
        };

        if (mainDataGridView.Columns.Count > 0)
            infos.Add("Columns: " + mainDataGridView.Columns.Count);

        if (mainDataGridView.Columns.Count > 0)
            infos.Add("Rows: " + mainDataGridView.Rows.Count);

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
        Text = OriginalFormName;
        mainDataGridView.ClearSelection();
        mainDataGridView.Columns.Clear();
        mainDataGridView.RowCount = 0;
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

    internal void LoadBCSVFile(string path)
    {
        ClearSearchCache();
        ClearDataGrid();

        using (var reader = File.OpenRead(path))
        LoadedFile = new BinaryCSV(reader.ToArray());

        if (LoadedFile == null) 
            return;

        Text = $"{OriginalFormName}: {Path.GetFileName(path)}";

        foreach (var fieldHeader in LoadedFile.Fields)
        {
            var columnName = fieldHeader.GetTranslatedNameOrHash();
           
            if (columnName.Contains('.'))
                columnName = columnName.Split('.')[0];
            else if (columnName.Contains(' '))
                columnName = columnName.Split(' ')[0];

            int columnId = mainDataGridView.Columns.Add(fieldHeader.Hash.ToString("x"), columnName);
            var column = mainDataGridView.Columns[columnId];
            column.ValueType = fieldHeader.DataType.ToType();

            if (fieldHeader.DataType == DataType.CRC32)
                mainDataGridView.Columns[columnId].CellTemplate = new CRCTemplate();

            //var toolTip = $"0x{fieldHeader.Hash:x}{(fieldHeader.IsMissingHash() ? "" : $"\nName: {fieldHeader.GetTranslatedNameOrNull()}")}\nType: {fieldHeader.DataType}\nSize: {fieldHeader.Size}";

            if (fieldHeader.IsMissingHash)
            {
                if (fieldHeader.Size == 4)
                    column.HeaderCell.Style.BackColor = Color.PaleVioletRed;
                else
                    column.HeaderCell.Style.BackColor = Color.Orange;        
            }

            //if (fieldHeader.Size == 8)
            //    mainDataGridView.Columns[columnId].HeaderCell.Style.BackColor = Color.Red;

            //mainDataGridView.Columns[columnId].ToolTipText = toolTip;
        }

        mainDataGridView.RowCount = LoadedFile.Length;

        ReloadInfo();
    }

    BCSVDirectorySearch directorySearchWindow;
    private void SearchOnFilesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Create directory search window if needed
        directorySearchWindow ??= new BCSVDirectorySearch();

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
            ClearDataGrid();
            LoadedFile.Dispose();
            LoadedFile = null;

            ReloadInfo();
        }
    }

    private void NewEntryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
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
            {
                File.WriteAllBytes(saveFileDialog.FileName, LoadedFile.Save());
            }

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
        foreach (DataGridViewRow selectedRow in mainDataGridView.SelectedRows)
        {
            if (selectedRow.Index >= LoadedFile.Length) continue;

            var selectedEntry = LoadedFile.Entries[selectedRow.Index];
            var newEntry = new object[selectedEntry.Length];

            // Copy fields
            for (var i = 0; i < selectedEntry.Length; i++)
                newEntry[i] = selectedEntry[i];


            // TODO: Check UniqueId and assign one?
            
            LoadedFile.Entries.Add(newEntry);

            mainDataGridView.RowCount = LoadedFile.Length;
            //var newEntryRow = mainDataGridView.Rows.Add([.. newEntry.Values]);
            //mainDataGridView.Rows[newEntryRow].Selected = true;
            //mainDataGridView.FirstDisplayedScrollingRowIndex = newEntryRow;
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
                indexesToRemove.Add(item.Index);
                mainDataGridView.Rows.Remove(item);
            }

            foreach (var index in indexesToRemove.OrderByDescending(x => x))
                LoadedFile.Entries.RemoveAt(index);

            mainDataGridView.RowCount = LoadedFile.Entries.Count;
        }

        ReloadInfo();
    }

    private int lastSelectedColumn = -1;
    private uint lastSelectedHashUint = 0;
    private void MainDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (LoadedFile == null || LoadedFile.Fields.Length <= e.ColumnIndex) return;

        if (e.Button == MouseButtons.Right)
        {
            var field = LoadedFile.Fields[e.ColumnIndex];

            viewAsToolStripMenuItem.Enabled = field.IsMissingHash && field.Size <= 6;
            validHeaderContextMenu.Show(Cursor.Position);

            lastSelectedColumn = e.ColumnIndex;
            lastSelectedHashUint = field.Hash;
        }
    }

    private void HshCstringRefToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1) return;

        //KnownHashValueManager.AddOrEdit(lastSelectedHash, DataType.CRC32);

        MessageBox.Show("Re-open your file to update values!");
    }

    private void F32ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1) return;

        //KnownHashValueManager.AddOrEdit(lastSelectedHash, DataType.Float32);

        MessageBox.Show("Re-open your file to update values!");
    }

    private void S32u32ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1) return;

        //KnownHashValueManager.AddOrEdit(lastSelectedHash, BCSVDataType.UInt32);

        MessageBox.Show("Re-open your file to update values!");
    }

    private void StringToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1) return;

        //KnownHashValueManager.AddOrEdit(lastSelectedHash, BCSVDataType.String);

        MessageBox.Show("Re-open your file to update values!");
    }

    private void MurmurHashToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1) return;
        //KnownHashValueManager.AddOrEdit(lastSelectedHash, BCSVDataType.Murmur3);

        MessageBox.Show("Re-open your file to update values!");
    }

    private void Int32ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedColumn == -1) return;
        //KnownHashValueManager.AddOrEdit(lastSelectedHash, BCSVDataType.Int32);

        MessageBox.Show("Re-open your file to update values!");
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
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
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
                                if (cell is uint hashValue && BCSVHashing.CRCHashes.TryGetValue(hashValue, out string value))
                                    return value;

                                break;
                            }

                        case DataType.MMH3:
                            {
                                if (cell is uint hashValue && BCSVHashing.MurmurHashes.TryGetValue(hashValue, out string value))
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
        if (LoadedFile == null || lastSelectedColumn == -1) return;

        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt",
            FilterIndex = 1,
            RestoreDirectory = true,
            OverwritePrompt = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            if (lastSelectedColumn < 0 || lastSelectedColumn > LoadedFile.Fields.Length)
                return;

            var field = LoadedFile.Fields[lastSelectedColumn];

            var exportedValues = new List<string>();
            foreach (var row in LoadedFile.Entries)
            {
                var cell = row[lastSelectedColumn];
                if (field.DataType == DataType.CRC32)
                {
                    if (cell is uint hashValue)
                    {
                        var containsKey = CRCHashes.ContainsKey(hashValue);
                        if (containsKey)
                            exportedValues.AddIfNotExist(CRCHashes[hashValue]);
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
            var rows = mainDataGridView.Rows.Cast<DataGridViewRow>();
            var cells = rows.SelectMany(x => x.Cells.Cast<DataGridViewCell>())
                .Where(cell =>
                {
                    var formattedValue = cell.FormattedValue.ToString();

                    if (!caseSensitive)
                    {
                        formattedValue = formattedValue.ToLower();
                        search = search.ToLower();
                    }

                    return (searchType == SearchType.Contains && formattedValue.Contains(search)) || (searchType == SearchType.Exactly && formattedValue == search);
                });

            searchCache = cells.ToArray();

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

        newFile.Entries = LoadedFile.Entries.Where((_, index) => selectedRows.Any(y => y.Index == index)).ToList();

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
                mainDataGridView.RowCount = LoadedFile.Length;
                //lastEntry = mainDataGridView.Rows.Add([.. entry.Values]);
            }

            if (lastEntry != -1)
                mainDataGridView.FirstDisplayedScrollingRowIndex = lastEntry;

        }
    }

    private void HeaderNameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedHashUint == 0) return;

        Clipboard.SetText(CRCHashes.TryGetValue(lastSelectedHashUint, out string value) ? value : $"0x{lastSelectedHashUint:x}");
    }

    private void HeaderHashToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lastSelectedHashUint == 0) return;

        Clipboard.SetText($"0x{lastSelectedHashUint:x}");
    }


    private bool sortByAscending = true;
    private void sortByRowIdToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //mainDataGridView.Sort(new DefaultSort(sortByAscending ? SortOrder.Ascending : SortOrder.Descending));
        mainDataGridView.Sort(new DefaultSort(SortOrder.Ascending));
        sortByAscending = !sortByAscending;
    }

    private class DefaultSort : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;

        public DefaultSort(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            IndexRow DataGridViewRow1 = (IndexRow)x;
            IndexRow DataGridViewRow2 = (IndexRow)y;

            // Try to sort based on the Last Name column.

            int CompareResult = DataGridViewRow1.OriginalIndex.CompareTo(DataGridViewRow2.OriginalIndex);

            return CompareResult * sortOrderModifier;
        }
    }
}
