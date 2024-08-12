using Force.Crc32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HeavenTool.BCSV;
using HeavenTool.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using HeavenTool.DataTable;

namespace HeavenTool
{
    public partial class MainFrm : Form
    {
        public List<DataEntry> Entries = new List<DataEntry>();
        public static Dictionary<uint, string> LoadedHashes = new Dictionary<uint, string>();

        private void CalculateHashes()
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "Hashes");
            Directory.CreateDirectory(dir);

            foreach (var file in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(file) != ".txt")
                    continue;

                foreach (string hashStr in File.ReadAllLines(file))
                    CheckHash(hashStr);

            }

            ReloadInfo();
        }

        private static void CheckHash(string hashStr)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(hashStr);
            uint hash = Crc32Algorithm.Compute(bytes);

            if (!LoadedHashes.ContainsKey(hash))
                LoadedHashes.Add(hash, hashStr);
        }

        private string originalName = "";
        public MainFrm()
        {
            InitializeComponent();
            

            ReloadInfo();

            CalculateHashes();
            KnownHashValueManager.Load();

            DrawingControl.SetDoubleBuffered(mainDataGridView);

            mainDataGridView.RowTemplate = new IndexRow();
            mainDataGridView.Rows.CollectionChanged += Rows_CollectionChanged;

            versionNumberLabel.Text = ProductVersion;
            Text = $"ACNH Heaven Tool | v{ProductVersion} | BCSV Editor";
            originalName = Text;
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
                statusStripMenu.Visible = false;
                editToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                unloadFileToolStripMenuItem.Enabled = false;
                return;
            }

            var infos = new List<string> {
                $"Loaded Hashes: {LoadedHashes.Count}"
            };

            if (mainDataGridView.Columns.Count > 0)
                infos.Add("Columns: " + mainDataGridView.Columns.Count);

            if (mainDataGridView.Columns.Count > 0)
                infos.Add("Rows: " + mainDataGridView.Rows.Count);

            infoLabel.Text = string.Join(" | ", infos);

            statusStripMenu.Visible = true;
            editToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            // TODO: Maybe implement save (beside save as) idk, isn't really a reccomended thing, is always good to have a backup
            saveToolStripMenuItem.Enabled = false;
            unloadFileToolStripMenuItem.Enabled = true;
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

        private BCSVFile LoadedFile;
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
            ClearDataGrid();

            LoadedFile = new BCSVFile(path);

            if (LoadedFile == null) return;

            Text = $"{originalName}: {Path.GetFileName(path)}";

            DrawingControl.SuspendDrawing(mainDataGridView);
            foreach (var fieldHeader in LoadedFile.Fields)
            {
                var columnId = mainDataGridView.Columns.Add(fieldHeader.Hash.ToString("x"), fieldHeader.GetTranslatedNameOrHash());
                var toolTip = $"0x{fieldHeader.Hash:x} (Type: {fieldHeader.DataType} | Size: {fieldHeader.Size})";

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
                    var addedRowId = mainDataGridView.Rows.Add(entry.Fields.Values.ToArray());
                    var addedRow = mainDataGridView.Rows[addedRowId];
                }
            }
            DrawingControl.ResumeDrawing(mainDataGridView);

            ReloadInfo();
        }

        BruteForce bruteForceWindow;
        private void bruteForceMissingHashesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bruteForceWindow == null)
                bruteForceWindow = new BruteForce();

            bruteForceWindow.Show();
            bruteForceWindow.BringToFront();
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
            var oldFieldValue = LoadedFile.Entries[indexRow.OriginalIndex].Fields[field.HashedName];

            var newValue = indexRow.Cells[e.ColumnIndex].Value;

            object formattedValue = null;
            bool invalidValue = false;
            string specificError = "";

            switch (field.DataType)
            {
                case DataType.String:
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

                case DataType.HashedCsc32:
                    {
                        if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        {
                            formattedValue = (uint)0;
                        }
                        else
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(newValue.ToString());
                            uint hash = Crc32Algorithm.Compute(bytes);

                            if (!LoadedHashes.ContainsKey(hash))
                            {
                                specificError = "This hash was not found on your hashlist. If you're sure that this value is correct, then put it on your /hashes/ folder and re-open the program.";
                                invalidValue = true;
                            }
                            else formattedValue = hash;
                        }
                        break;
                    }

                case DataType.S8:
                    {
                        if (sbyte.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                        break;
                    }

                case DataType.MultipleU8:
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

                case DataType.U8:
                    {
                        if (byte.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                        break;
                    }

                case DataType.UInt32:
                    {
                        if (uint.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;

                        break;
                    }

                case DataType.UInt16:
                    {
                        if (short.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;

                        break;
                    }

                case DataType.Float32:
                    {
                        if (float.TryParse(newValue.ToString(), out var value))
                            formattedValue = value;
                        else invalidValue = true;
                        break;
                    }

                case DataType.Int32:
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
                ignoreNextChangeEvent = true;
                indexRow.Cells[e.ColumnIndex].Value = oldFieldValue;

            }
            else
            {
                // Assign the formatted value to the loaded bcsv file
                LoadedFile.Entries[indexRow.OriginalIndex].Fields[field.HashedName] = formattedValue;

                // We have to set this again otherwise types will mismatch eventually (cause the field is read as a string while un-edited values are numbers)
                indexRow.Cells[e.ColumnIndex].Value = formattedValue;
            }
        }

        private void mainDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                var field = LoadedFile.Fields[e.ColumnIndex];
                var originalValue = e.Value;
                //e.FormattingApplied = true;

                switch (field.DataType)
                {
                    case DataType.String:
                        e.Value = originalValue.ToString();
                        break;

                    case DataType.MultipleU8:
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

                    case DataType.HashedCsc32:
                        {
                            if (originalValue is uint hashValue)
                            {
                                if (hashValue == 0)
                                    e.Value = "";
                                else
                                {
                                    var containsKey = LoadedHashes.ContainsKey(hashValue);
                                    e.Value = containsKey ? LoadedHashes[hashValue] : hashValue.ToString("x");
                                    if (!containsKey)
                                        e.CellStyle.BackColor = Color.LightCyan;
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

                var newEntry = new DataEntry()
                {
                    Fields = LoadedFile.Fields.ToDictionary(pair => pair.HashedName, value => value.GetFieldDefaultValue())
                };

                LoadedFile.Entries.Add(newEntry);

               mainDataGridView.Rows.Add(newEntry.Fields.Values.ToArray());
            }
            else
            {
                var lastEntry = LoadedFile.Entries.Last();
                var newEntry = new DataEntry()
                {
                    Fields = new Dictionary<string, object>()
                };

                // Copy fields
                foreach (var entryField in lastEntry.Fields)
                    newEntry.Fields.Add(entryField.Key, entryField.Value);

                // TODO: Check UniqueId and assign one?
                LoadedFile.Entries.Add(newEntry);

                var newEntryRow = mainDataGridView.Rows.Add(newEntry.Fields.Values.ToArray());
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

                var newEntry = new DataEntry()
                {
                    Fields = new Dictionary<string, object>()
                };

                // Copy fields
                foreach (var entryField in selectedEntry.Fields)
                    newEntry.Fields.Add(entryField.Key, entryField.Value);

                // TODO: Check UniqueId and assign one?
                LoadedFile.Entries.Add(newEntry);

                var newEntryRow = mainDataGridView.Rows.Add(newEntry.Fields.Values.ToArray());
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
        private void mainDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LoadedFile == null || LoadedFile.Fields.Length <= e.ColumnIndex) return;

            //var column = mainDataGridView.Columns[e.ColumnIndex];

            if (e.Button == MouseButtons.Right)
            {
                var field = LoadedFile.Fields[e.ColumnIndex];

                lastSelectedColumn = e.ColumnIndex;
                lastSelectedHash = field.HashedName;

                if (field.IsMissingHash() && field.Size == 4)
                {
                    columnHeaderContextMenu.Show(Cursor.Position);
                }
                else
                {
                    validHeaderContextMenu.Show(Cursor.Position);
                }

                lastSelectedColumn = e.ColumnIndex; 
                lastSelectedHash = field.HashedName;
            }
        }

        private void hshCstringRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, DataType.HashedCsc32);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void f32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, DataType.Float32);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void s32u32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, DataType.UInt32);

            MessageBox.Show("Re-open your file to update values!");
        }

        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSelectedHash == null) return;

            KnownHashValueManager.AddOrEdit(lastSelectedHash, DataType.String);

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
            try
            {
                var extension = ".bcsv";
                var keyName = "BCSV_Editor";
                var fileDescription = "BCSV File";
                var openWith = Application.ExecutablePath;

                using (var BaseKey = Registry.ClassesRoot.CreateSubKey(extension))
                {
                    BaseKey.SetValue("", keyName);

                    using (var OpenMethod = Registry.ClassesRoot.CreateSubKey(keyName))
                    {
                        OpenMethod.SetValue("", fileDescription);
                        OpenMethod.CreateSubKey("DefaultIcon").SetValue("", $"\"{openWith}\",0");

                        using (var Shell = OpenMethod.CreateSubKey("Shell"))
                        {
                            Shell.CreateSubKey("edit").CreateSubKey("command").SetValue("", $"\"{openWith}\" \"%1\"");
                            Shell.CreateSubKey("open").CreateSubKey("command").SetValue("", $"\"{openWith}\" \"%1\"");
                        }
                    }
                }

                // Delete the key instead of trying to change it
                using (var CurrentUser = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + extension, true))
                {
                    CurrentUser.DeleteSubKey("UserChoice", false);
                }

                // Tell explorer the file association has been changed
                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You need to open the program as administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception error)
            {
                MessageBox.Show(error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

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

                    var bcsvFile = new BCSVFile(file);
                    foreach (var item in bcsvFile.Fields)
                    {
                        if (LoadedHashes.ContainsKey(item.Hash))
                        {
                            var hashName = LoadedHashes[item.Hash];
                            if (!usedHashesHeaders.Contains(hashName)) usedHashesHeaders.Add(hashName);
                        }
                    }

                    foreach (var entry in bcsvFile.Entries)
                        foreach (var entryField in entry.Fields)
                            if (entryField.Value is uint hashValue && (hashValue > 10000000))
                            {
                                if (LoadedHashes.ContainsKey(hashValue))
                                {
                                    var hashName = LoadedHashes[hashValue];
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
                var exportedValues =new List<string>();
                foreach (var row in LoadedFile.Entries)
                {
                    var cell = row.Fields[lastSelectedHash];
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

                        var bcsvFile = new BCSVFile(file);
                        var list = new List<string>();

                        if (bcsvFile.Fields.Any(x => x.GetTranslatedNameOrHash().StartsWith("Label string")))
                        {
                            var fieldId = bcsvFile.Fields.First(x => x.GetTranslatedNameOrHash().StartsWith("Label string"));
                            foreach (var entry in bcsvFile.Entries)
                                list.Add(entry.Fields[fieldId.HashedName].ToString());

                            File.WriteAllLines(outputFile, list);
                        }
                    }

                    Process.Start(outputDirectory);
                }
            }
        }  
    }
}
