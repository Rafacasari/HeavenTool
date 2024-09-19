using Force.Crc32;
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
using HeavenTool.Utility.FileTypes.BCSV;
using System.Globalization;
using HeavenTool.Utility.IO;

namespace HeavenTool
{
    public partial class BCSVForm : Form
    {
        public List<BCSVEntry> Entries = new List<BCSVEntry>();
        public static Dictionary<uint, string> CRCHashes = new Dictionary<uint, string>();
        public static Dictionary<uint, string> MurmurHashes = new Dictionary<uint, string>();
        public static Dictionary<uint, List<CRC32Value>> EnumHashes = new Dictionary<uint, List<CRC32Value>>();

        public class CRC32Value
        {
            public CRC32Value(uint val) 
            { 
                Value = val;
            }

            public uint Value;

            public override string ToString()
            {
                return CRCHashes.ContainsKey(Value) ? CRCHashes[Value] : Value.ToString("x");
            }
        }

        private void CalculateHashes()
        {
            string crcHashes = Path.Combine(AppContext.BaseDirectory, "hashes");
            Directory.CreateDirectory(crcHashes);

            string murmurHashes = Path.Combine(AppContext.BaseDirectory, "murmur3-hashes");
            Directory.CreateDirectory(murmurHashes);

            foreach (var file in Directory.GetFiles(crcHashes))
            {
                if (Path.GetExtension(file) != ".txt")
                    continue;

                foreach (string hashStr in File.ReadAllLines(file))
                    ChechCrc32Hash(hashStr);

            }

            foreach (var file in Directory.GetFiles(murmurHashes))
            {
                if (Path.GetExtension(file) != ".txt")
                    continue;

                foreach (string hashStr in File.ReadAllLines(file))
                    CheckMurmurHash(hashStr);

            }

            LoadEnumHashes();
            ReloadInfo();
        }

        private void LoadEnumHashes()
        {
            string enumFolder = Path.Combine(AppContext.BaseDirectory, "hashes/enum");
            Directory.CreateDirectory(enumFolder);

            foreach (var file in Directory.GetFiles(enumFolder, "*.txt", SearchOption.AllDirectories))
            {
                if (Path.GetExtension(file) != ".txt")
                    continue;

                var fileName = Path.GetFileNameWithoutExtension(file);
                if (fileName.StartsWith("0x"))
                    fileName = fileName.Substring(2);

                bool parsedSuccessfully = uint.TryParse(fileName, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint enumHash);
                if (parsedSuccessfully)
                {
                    if (!EnumHashes.ContainsKey(enumHash))
                        EnumHashes.Add(enumHash, new List<CRC32Value>());

                    var collection = EnumHashes[enumHash];
                    foreach (string hashStr in File.ReadAllLines(file))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(hashStr);
                        uint hash = Crc32Algorithm.Compute(bytes);

                        if (!collection.Any(x => x.Value == hash))
                            collection.Add(new CRC32Value(hash));

                        if (!CRCHashes.ContainsKey(hash))
                            CRCHashes.Add(hash, hashStr);
                    }
                }
            }
        }

        private static void ChechCrc32Hash(string hashStr)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(hashStr);
            uint hash = Crc32Algorithm.Compute(bytes);

            if (!CRCHashes.ContainsKey(hash))
                CRCHashes.Add(hash, hashStr);
        }

        private static void CheckMurmurHash(string hashStr)
        {
            ReadOnlySpan<byte> inputSpan = Encoding.UTF8.GetBytes(hashStr).AsSpan();

            uint hash = Murmur.Hash32(ref inputSpan, 0);

            if (!MurmurHashes.ContainsKey(hash))
                MurmurHashes.Add(hash, hashStr);
        }

        private string originalName = "";
        public BCSVForm()
        {
            InitializeComponent();
            dragInfo.Dock = DockStyle.Fill;
            dragInfo.AutoSize = false;

            ReloadInfo();

            CalculateHashes();
            KnownHashValueManager.Load();

            DrawingControl.SetDoubleBuffered(mainDataGridView);

            mainDataGridView.RowTemplate = new IndexRow();
            mainDataGridView.Rows.CollectionChanged += Rows_CollectionChanged;

            versionNumberLabel.Text = ProductVersion;
            Text = $"ACNH Heaven Tool | v{ProductVersion} | BCSV Editor";
            originalName = Text;

           
            associatebcsvWithThisProgramToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".bcsv") == Application.ExecutablePath;
        }

        private void Rows_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            // We need this cause DataGrid changes the RowIndex when user use the OrderBy feature (bruh)
            if (e.Action == System.ComponentModel.CollectionChangeAction.Add && e.Element is IndexRow indexRow)
                indexRow.OriginalIndex = indexRow.Index;

        }

        private void ReloadInfo()
        {
            if (LoadedFile == null)
            {
                dragInfo.Visible = true;
                statusStripMenu.Visible = false;
                editToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                unloadFileToolStripMenuItem.Enabled = false;
                exportToCSVFileToolStripMenuItem.Enabled = false;
                importFromFileToolStripMenuItem.Enabled = false;
                exportSelectionToolStripMenuItem.Enabled = false;
                return;
            }

            var infos = new List<string> {
                $"CRC32 Hashes: {CRCHashes.Count} | Murmur Hashes: {MurmurHashes.Count}"
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
            saveToolStripMenuItem.Enabled = false;
            unloadFileToolStripMenuItem.Enabled = true;
            exportToCSVFileToolStripMenuItem.Enabled = true;
            importFromFileToolStripMenuItem.Enabled = true;
            exportSelectionToolStripMenuItem.Enabled = true;
        }

        private void ClearDataGrid()
        {
            Text = originalName;
            mainDataGridView.ClearSelection();
            mainDataGridView.Rows.Clear();
            mainDataGridView.Columns.Clear();
            mainDataGridView.Refresh();

            ReloadInfo();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openBCSVFile.ShowDialog(this);
        }

        private BinaryCSV LoadedFile;
        private void openBCSVFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
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

            LoadedFile = new BinaryCSV(path);

            if (LoadedFile == null) return;

            Text = $"{originalName}: {Path.GetFileName(path)}";

            DrawingControl.SuspendDrawing(mainDataGridView);      

            foreach (var fieldHeader in LoadedFile.Fields)
            {
                var columnName = fieldHeader.GetTranslatedNameOrHash();
                if (!fieldHeader.IsMissingHash())
                {
                    if (columnName.Contains("."))
                        columnName = columnName.Split('.')[0];
                    else if (columnName.Contains(" "))
                        columnName = columnName.Split(' ')[0];
                }

                int columnId = mainDataGridView.Columns.Add(fieldHeader.Hash.ToString("x"), columnName);
                

                var toolTip = $"0x{fieldHeader.Hash:x}{(fieldHeader.IsMissingHash() ? "" : $"\nName: {fieldHeader.GetTranslatedNameOrNull()}")}\nType: {fieldHeader.DataType}\nSize: {fieldHeader.Size}";

                if (fieldHeader.IsMissingHash())
                {
                    if (fieldHeader.Size == 4)
                    {
                        mainDataGridView.Columns[columnId].HeaderCell.Style.BackColor = Color.PaleVioletRed;
                        toolTip += "\nUnknown hash (Value may be wrong, right click and try different possibilities)";
                    }
                    else
                    {
                        mainDataGridView.Columns[columnId].HeaderCell.Style.BackColor = Color.Orange;
                        toolTip += "\nUnknown hash";
                    }
                }

                mainDataGridView.Columns[columnId].ToolTipText = toolTip;
                //mainDataGridView.Columns[columnId].ValueType = fieldHeader.GetValueType();
            }

            if (LoadedFile.Entries != null)
            {
                foreach (var entry in LoadedFile.Entries)
                {
                    var addedRowId = mainDataGridView.Rows.Add(entry.Values.ToArray());
                    var addedRow = mainDataGridView.Rows[addedRowId];
                }
            }
            DrawingControl.ResumeDrawing(mainDataGridView);

            ReloadInfo();
        }

        private void mainDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var hashedName = mainDataGridView.CurrentCell.OwningColumn.Name;

            bool parsedSuccessfully = uint.TryParse(hashedName, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint enumHash);

            if (e.Control is TextBox txtControl) 
            {
                // We have info about this hash, show auto-complete
                if (parsedSuccessfully && EnumHashes.ContainsKey(enumHash))
                {
                    var source = new AutoCompleteStringCollection();
                    source.AddRange(EnumHashes[enumHash].Select(x => x.ToString()).ToArray());

                    txtControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtControl.AutoCompleteCustomSource = source;
                    txtControl.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    
                }

                // We don't have info about this hash, remove auto-complete
                else
                {
                    txtControl.AutoCompleteMode = AutoCompleteMode.None;
                }

            }
            
        }

        DirectorySearch directorySearchWindow;
        private void searchOnFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (directorySearchWindow == null)
                directorySearchWindow = new DirectorySearch();

            directorySearchWindow.Show();
            directorySearchWindow.BringToFront();
        }

        bool ignoreNextChangeEvent = false;
        private void mainDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (ignoreNextChangeEvent)
            {
                ignoreNextChangeEvent = false;
                return;
            }

            if (LoadedFile == null) return;

            if (!(mainDataGridView.Rows[e.RowIndex] is IndexRow indexRow)) return;

            var field = LoadedFile.Fields[e.ColumnIndex];
            var oldFieldValue = LoadedFile.Entries[indexRow.OriginalIndex][field.HEX];

            var newValue = indexRow.Cells[e.ColumnIndex].Value;

            object formattedValue = null;
            bool invalidValue = false;
            string specificError = "";

            switch (field.DataType)
            {
                case BcsvDataType.String:
                    {
                        var bytes = Encoding.UTF8.GetBytes(newValue.ToString());
                        if (bytes.Length <= field.Size) 
                            formattedValue = newValue.ToString();
                        else
                        {
                            specificError = $"Your string exceeded the max bytes size! ({bytes.Length}/{field.Size})\nBe aware that using special characters or japanese letters can consume more bytes!";
                            invalidValue = true;
                        }
                        break;
                    }

                case BcsvDataType.HashedCsc32:
                    {
                        if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        {
                            formattedValue = (uint)0;
                        }
                        else
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(newValue.ToString());
                            uint hash = Crc32Algorithm.Compute(bytes);

                            if (!CRCHashes.ContainsKey(hash))
                            {
                                specificError = "This hash was not found on your hashlist. If you're sure that this value is correct, then put it on your /hashes/ folder and re-open the program.";
                                invalidValue = true;
                            }
                            else formattedValue = hash;
                        }
                        break;
                    }

                case BcsvDataType.Murmur3:
                    {
                        if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        {
                            formattedValue = (uint)0;
                        }
                        else
                        {

                            ReadOnlySpan<byte> inputSpan = Encoding.UTF8.GetBytes(newValue.ToString()).AsSpan();
                            uint hash = Murmur.Hash32(ref inputSpan, 0);

                            if (!MurmurHashes.ContainsKey(hash))
                            {
                                specificError = "This hash was not found on your hashlist. If you're sure that this value is correct, then put it on your /hashes/ folder and re-open the program.";
                                invalidValue = true;
                            }
                            else formattedValue = hash;
                        }
                        break;
                    }

                case BcsvDataType.S8:
                    {
                        if (sbyte.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                        break;
                    }

                case BcsvDataType.MultipleU8:
                    {
                        var failedToReadBytes = false;
                        var bytes = newValue.ToString().Split(' ').Select(x =>
                        {
                            if (byte.TryParse(x, out var value))
                                return value;
                            else failedToReadBytes = true;

                            return (byte)0;
                        }).ToArray();

                        if (!failedToReadBytes)
                            formattedValue = bytes;
                        else invalidValue = true;

                        break;
                    }

                case BcsvDataType.U8:
                    {
                        if (byte.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                        break;
                    }

                case BcsvDataType.UInt32:
                    {
                        if (uint.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;

                        break;
                    }

                case BcsvDataType.UInt16:
                    {
                        if (short.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;

                        break;
                    }

                case BcsvDataType.Float32:
                    {
                        if (float.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                        break;
                    }

                case BcsvDataType.Int32:
                    {
                        if (int.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                    }
                    break;
            }

            // value is null or invalid, rollback to previous value and message an error
            if (formattedValue == null || invalidValue)
            {
                // value is null 
                if (string.IsNullOrEmpty(specificError))
                    MessageBox.Show("Your value is null or invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show(specificError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

               
                if (indexRow.Cells[e.ColumnIndex].Value != oldFieldValue)
                {
                    ignoreNextChangeEvent = true;
                    indexRow.Cells[e.ColumnIndex].Value = oldFieldValue;
                }
               
            }
            else
            {
                // Assign the formatted value to the loaded bcsv file
                LoadedFile.Entries[indexRow.OriginalIndex][field.HEX] = formattedValue;

                // We have to set this again otherwise types will mismatch eventually (cause the field is read as a string while un-edited values are numbers)
                if (indexRow.Cells[e.ColumnIndex].Value != formattedValue)
                {
                    ignoreNextChangeEvent = true;
                    indexRow.Cells[e.ColumnIndex].Value = formattedValue;
                }
            }
        }

        private void mainDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                var field = LoadedFile.Fields[e.ColumnIndex];
                var originalField = mainDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var originalValue = e.Value;
                //e.FormattingApplied = true;

                switch (field.DataType)
                {
                    case BcsvDataType.String:
                        e.Value = originalValue.ToString();
                        break;

                    case BcsvDataType.MultipleU8:
                        {
                            if (originalValue is byte[] bytesValue)
                            {
                                if (bytesValue.Length == 0)
                                    e.Value = "";
                                else
                                    e.Value = string.Join(" ", bytesValue);
                            }
                            else
                                e.Value = originalValue != null ? originalValue.ToString() : "";

                            break;
                        }

                    case BcsvDataType.HashedCsc32:
                        {
                            if (originalValue is uint hashValue)
                            {
                                if (hashValue == 0)
                                    e.Value = "";
                                else
                                {
                                    var containsKey = CRCHashes.ContainsKey(hashValue);
                                    e.Value = containsKey ? CRCHashes[hashValue] : hashValue.ToString("x");
                                    if (!containsKey)
                                    {
                                        originalField.ToolTipText = "Unknown Hash";
                                        e.CellStyle.BackColor = Color.LightCyan;
                                    }
                                    else originalField.ToolTipText = $"Hash: {hashValue:x}";

                                }
                            }
                            else
                                e.Value = originalValue != null ? originalValue.ToString() : "";

                            break;
                        }

                    case BcsvDataType.Murmur3:
                        {
                            if (originalValue is uint hashValue)
                            {
                                if (hashValue == 0)
                                    e.Value = "";
                                else
                                {
                                    var containsKey = MurmurHashes.ContainsKey(hashValue);
                                    e.Value = containsKey ? MurmurHashes[hashValue] : hashValue.ToString("x");
                                    if (!containsKey)
                                    {
                                        originalField.ToolTipText = "Unknown Hash";
                                        e.CellStyle.BackColor = Color.LightCyan;
                                    }
                                    else originalField.ToolTipText = $"Hash: {hashValue:x}";

                                }
                            }
                            else
                                e.Value = originalValue != null ? originalValue.ToString() : "";

                            break;
                        }

                }

                if (e.Value != originalValue)
                    e.FormattingApplied = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in row: {e.RowIndex} | Column {e.ColumnIndex}:\n{ex}");
            }
        }


        private void unloadFileToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void newEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null) return;

            if (LoadedFile.Entries.Count == 0)
            {
                // There is no entry to copy, create using default values
                var newEntry = new BCSVEntry(LoadedFile.Fields.ToDictionary(pair => pair.HEX, value => value.GetFieldDefaultValue()));

                LoadedFile.Entries.Add(newEntry);

                mainDataGridView.Rows.Add(newEntry.Values.ToArray());
            }
            else
            {
                var lastEntry = LoadedFile.Entries.Last();
                var newEntry = new BCSVEntry();

                // Copy fields
                foreach (var entryField in lastEntry)
                    newEntry.Add(entryField.Key, entryField.Value);

                // TODO: Check UniqueId and assign one?
                LoadedFile.Entries.Add(newEntry);

                var newEntryRow = mainDataGridView.Rows.Add(newEntry.Values.ToArray());
                mainDataGridView.Rows[newEntryRow].Selected = true;
                mainDataGridView.FirstDisplayedScrollingRowIndex = newEntryRow;
            }

            ReloadInfo();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "BCSV (*.bcsv)|*.bcsv",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    OverwritePrompt = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    LoadedFile.SaveAs(saveFileDialog.FileName);

            }
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (LoadedFile == null)
            {
                newEntryToolStripMenuItem.Enabled = false;
                duplicateRowToolStripMenuItem.Enabled = false;
            }

            duplicateRowToolStripMenuItem.Enabled = mainDataGridView.SelectedRows.Count > 0;
            deleteRowsToolStripMenuItem.Enabled = mainDataGridView.SelectedRows.Count > 0;
        }

        private void duplicateRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (IndexRow selectedRow in mainDataGridView.SelectedRows)
            {
                if (selectedRow.OriginalIndex >= LoadedFile.Entries.Count) continue;

                var selectedEntry = LoadedFile.Entries[selectedRow.OriginalIndex];

                var newEntry = new BCSVEntry();

                // Copy fields
                foreach (var entryField in selectedEntry)
                    newEntry.Add(entryField.Key, entryField.Value);

                // TODO: Check UniqueId and assign one?
                LoadedFile.Entries.Add(newEntry);

                var newEntryRow = mainDataGridView.Rows.Add(newEntry.Values.ToArray());
                mainDataGridView.Rows[newEntryRow].Selected = true;
                mainDataGridView.FirstDisplayedScrollingRowIndex = newEntryRow;


            }

            ReloadInfo();
        }

        private void deleteRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var count = mainDataGridView.SelectedRows.Count;
            var text = count > 1 ? $"these {count} entries" : "this entry";
            var result = MessageBox.Show($"Do you really want to delete {text}?\nThis action can't be un-done!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var originalIndexes = new List<int>();
                foreach (IndexRow item in mainDataGridView.SelectedRows)
                {
                    originalIndexes.Add(item.OriginalIndex);
                    mainDataGridView.Rows.Remove(item);
                }

                foreach (var index in originalIndexes.OrderByDescending(x => x))
                    LoadedFile.Entries.RemoveAt(index);
            }

            ReloadInfo();
        }

        private string lastSelectedHash = null;
        private int lastSelectedColumn = -1;
        private uint lastSelectedHashUint = 0;
        private void mainDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LoadedFile == null || LoadedFile.Fields.Length <= e.ColumnIndex) return;

            if (e.Button == MouseButtons.Right)
            {
                var field = LoadedFile.Fields[e.ColumnIndex];

                viewAsToolStripMenuItem.Enabled = field.IsMissingHash() && field.Size <= 6;
                validHeaderContextMenu.Show(Cursor.Position);  

                lastSelectedColumn = e.ColumnIndex;
                lastSelectedHash = field.HEX;
                lastSelectedHashUint = field.Hash;
            }
        }

        private void hshCstringRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, BcsvDataType.HashedCsc32);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void f32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, BcsvDataType.Float32);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void s32u32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, BcsvDataType.UInt32);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, BcsvDataType.String);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void murmurHashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;
            KnownHashValueManager.AddOrEdit(lastSelectedHash, BcsvDataType.Murmur3);

            MessageBox.Show("Re-open your file to update values!");
        }


        private void int32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;
            KnownHashValueManager.AddOrEdit(lastSelectedHash, BcsvDataType.Int32);

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

        private void associatebcsvWithThisProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var exePath = Application.ExecutablePath;
            var arguments = $"--{(associatebcsvWithThisProgramToolStripMenuItem.Checked ? "disassociate" : "associate")} bcsv";


            var process = Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = arguments,
                Verb = "runas",
                UseShellExecute = true
            });

            process.WaitForExit();
            associatebcsvWithThisProgramToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".bcsv") == Application.ExecutablePath;
        }


     

        private void exportValidHashesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string dir = Path.Combine(AppContext.BaseDirectory, "Exported Hashes");
                Directory.CreateDirectory(dir);

                var usedHashesHeaders = new List<string>();
                var usedHashesValues = new List<string>();

                var filesPath = folderBrowserDialog.SelectedPath;
                foreach (var file in Directory.GetFiles(filesPath))
                {
                    if (Path.GetExtension(file) != ".bcsv")
                        continue;

                    var bcsvFile = new BinaryCSV(file);
                    foreach (var item in bcsvFile.Fields)
                    {
                        if (CRCHashes.ContainsKey(item.Hash))
                        {
                            var hashName = CRCHashes[item.Hash];
                            if (!usedHashesHeaders.Contains(hashName)) usedHashesHeaders.Add(hashName);
                        }
                    }

                    foreach (var entry in bcsvFile.Entries)
                        foreach (var entryField in entry)
                            if (entryField.Value is uint hashValue && (hashValue > 10000000))
                            {
                                if (CRCHashes.ContainsKey(hashValue))
                                {
                                    var hashName = CRCHashes[hashValue];
                                    if (!usedHashesValues.Contains(hashName)) usedHashesValues.Add(hashName);
                                }
                            }


                }

                File.WriteAllLines(Path.Combine(dir, "headers.txt"), usedHashesHeaders);
                File.WriteAllLines(Path.Combine(dir, "values.txt"), usedHashesValues);

                Process.Start(dir);
            }
        }

        private void exportToCSVFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.Columns.Count == 0) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
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
                sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

                foreach (DataGridViewRow row in mainDataGridView.Rows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));

                }

                File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }
        }

        private void exportValuesAstxtFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null || lastSelectedHash == null) return;


            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var field = LoadedFile.Fields.Single(x => x.HEX == lastSelectedHash);

                var exportedValues = new List<string>();
                foreach (var row in LoadedFile.Entries)
                {
                    var cell = row[lastSelectedHash];
                    if (field.DataType == BcsvDataType.HashedCsc32)
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

        private void exportLabelFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var path = folderBrowserDialog.SelectedPath;
                var outputDirectory = Path.Combine(path, "output");

                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(outputDirectory);
                    foreach (var file in Directory.GetFiles(path))
                    {
                        if (Path.GetExtension(file) != ".bcsv") continue;

                        var outputFile = Path.Combine(outputDirectory, $"{Path.GetFileNameWithoutExtension(file)}-values.txt");

                        var bcsvFile = new BinaryCSV(file);
                        var list = new List<string>();

                        if (bcsvFile.Fields.Any(x => x.GetTranslatedNameOrHash().StartsWith("Label string")))
                        {
                            var fieldId = bcsvFile.Fields.First(x => x.GetTranslatedNameOrHash().StartsWith("Label string"));
                            foreach (var entry in bcsvFile.Entries)
                                list.Add(entry[fieldId.HEX].ToString());

                            File.WriteAllLines(outputFile, list);
                        }
                    }

                    Process.Start(outputDirectory);
                }
            }
        }


        private void compareRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var compareWindow = new BCSVCompareWindow();
            compareWindow.compareDataGrid.Columns.Clear();

            foreach (DataGridViewColumn column in mainDataGridView.Columns)
            {
                var columnId = compareWindow.compareDataGrid.Columns.Add(column.Name, column.HeaderText);
                compareWindow.compareDataGrid.Columns[columnId].HeaderCell.Style = column.HeaderCell.Style;
                
            }
        
            compareWindow.compareDataGrid.Rows.Clear();
            foreach (DataGridViewRow row in mainDataGridView.SelectedRows) {
                var values = new List<object>();
                foreach (DataGridViewCell entry in row.Cells)
                    values.Add(entry.FormattedValue);

                compareWindow.compareDataGrid.Rows.Add(values.ToArray());
            }

            compareWindow.ShowDialog();
        }

        private void mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count > 1)
            {
                compareRowsToolStripMenuItem.Enabled = true;
            }
            else
            {
                compareRowsToolStripMenuItem.Enabled = false;
            }
        }

        private BCSVSearchBox searchBox;
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchBox == null)
            {
                searchBox = new BCSVSearchBox(this)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
            }

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

        public static Color HIGHLIGHT_COLOR = Color.FromArgb(180, 180, 10);
        public static Color HIGHLIGHT_COLOR_CURRENT = Color.YellowGreen;

        private void RestoreSearchCache()
        {
            if (oldColorCache == null)
                oldColorCache = new Dictionary<DataGridViewCell, Color>();

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
                ClearSearchColors();

                lastSearchCell = null;
                searchCache = null;
            }
        }

        internal void ClearSearchColors()
        {
            if (oldColorCache != null)
            {
                foreach (var cache in oldColorCache)
                    cache.Key.Style.BackColor = cache.Value;

                oldColorCache.Clear();
                oldColorCache = null;
            }
        }

        internal void Search(string search, SearchType searchType, bool searchBackwards, bool caseSensitive)
        {
            if (lastSearch != search || lastSearchType != searchType || lastCaseSensitive != caseSensitive)
            {
                lastSearch = search;
                lastSearchType = searchType;
                lastCaseSensitive = caseSensitive;

                ClearSearchCache();
            }

            if (oldColorCache == null)
                oldColorCache = new Dictionary<DataGridViewCell, Color>();

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

        private void exportSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null) return;

            var newFile = BinaryCSV.CopyFileWithoutEntries(LoadedFile);

            var selectedRows = new IndexRow[mainDataGridView.SelectedRows.Count];
            mainDataGridView.SelectedRows.CopyTo(selectedRows, 0);

            newFile.Entries = LoadedFile.Entries.Where((_, index) => selectedRows.Any(y => y.OriginalIndex == index)).ToList();

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "BCSV (*.bcsv)|*.bcsv",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                newFile.SaveAs(saveFileDialog.FileName);
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = "Select a BCSV to import",
                Filter = "BCSV|*.bcsv",
                DefaultExt = "*.bcsv"
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                var bcsvToCopy = new BinaryCSV(file);

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
                foreach(var entry in bcsvToCopy.Entries)
                {
                    LoadedFile.Entries.Add(entry);
                    lastEntry = mainDataGridView.Rows.Add(entry.Values.ToArray());
                }

                if (lastEntry != -1)
                    mainDataGridView.FirstDisplayedScrollingRowIndex = lastEntry;

            }
        }

        private void headerNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHashUint == 0) return;

            Clipboard.SetText(CRCHashes.ContainsKey(lastSelectedHashUint) ? CRCHashes[lastSelectedHashUint] : $"0x{lastSelectedHashUint:x}");
        }

        private void headerHashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHashUint == 0) return;

            Clipboard.SetText($"0x{lastSelectedHashUint:x}");
        }

        private void mainDataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            var field = LoadedFile.GetFieldByHashedName(e.Column.Name);

            if (field.DataType == BcsvDataType.HashedCsc32 || field.DataType == BcsvDataType.Murmur3)
            {
                var formattedValue1 = mainDataGridView.Rows[e.RowIndex1].Cells[e.Column.Name].FormattedValue.ToString();
                var formattedValue2 = mainDataGridView.Rows[e.RowIndex2].Cells[e.Column.Name].FormattedValue.ToString();

                e.SortResult = formattedValue1.CompareTo(formattedValue2);
                e.Handled = true;
            }
        }

        private void exportEnumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var path = folderBrowserDialog.SelectedPath;
                var outputDirectory = Path.Combine(path, "enum-output");

                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(outputDirectory);
                    foreach (var file in Directory.GetFiles(path))
                    {
                        if (Path.GetExtension(file) != ".bcsv") continue;


                        var bcsvFile = new BinaryCSV(file);
                        var hashedFields = bcsvFile.Fields.Where(x => x.DataType == BcsvDataType.HashedCsc32).ToList();
                        if (bcsvFile.Entries.Count > 0 && hashedFields.Count > 0)
                        {
                            var directoryPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(file));
                            Directory.CreateDirectory(directoryPath);

                            foreach (var field in hashedFields)
                            {
                                var outputFile = Path.Combine(directoryPath, $"{field.HEX}.txt");
                                var parsedList = new List<uint>();
                                var registers = new List<string>();

                                foreach (var entry in bcsvFile.Entries)
                                {
                                    if (entry[field.HEX] is uint value && !parsedList.Contains(value))
                                    {
                                        if (CRCHashes.ContainsKey(value))
                                            registers.Add(CRCHashes[value]);
                                        

                                        parsedList.Add(value);
                                    }
                                }

                                File.WriteAllLines(outputFile, registers);
                            }
                        }

                        //if (bcsvFile.Fields.Any(x => x.GetTranslatedNameOrHash().StartsWith("Label string")))
                        //{
                        //    var fieldId = bcsvFile.Fields.First(x => x.GetTranslatedNameOrHash().StartsWith("Label string"));
                        //    foreach (var entry in bcsvFile.Entries)
                        //        list.Add(entry[fieldId.HEX].ToString());

                        //    File.WriteAllLines(outputFile, list);
                        //}
                    }

                    Process.Start(outputDirectory);
                }
            }
        }
    }
}
