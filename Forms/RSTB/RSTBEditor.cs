using HeavenTool.Properties;
using HeavenTool.Utility;
using HeavenTool.Utility.FileTypes.RSTB;
using HeavenTool.Utility.IO;
using HeavenTool.Utility.IO.Compression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace HeavenTool.Forms.RSTB
{
    public partial class RSTBEditor : Form
    {
        public RSTBEditor()
        {
            InitializeComponent();

            Text = $"Heaven Tool | {Program.VERSION} | RSTB Editor";

            //var fileNameColumn = mainDataGridView.Columns.Add("fileName", "File Name");
            mainDataGridView.Columns["fileName"].ValueType = typeof(string);
            mainDataGridView.Columns["fileSize"].ValueType = typeof(uint);
            mainDataGridView.Columns["DLC"].ValueType = typeof(uint);
            //var sizeColumn = mainDataGridView.Columns.Add("Size", "Size");
            //var dlcColumn = mainDataGridView.Columns.Add("DLC", "DLC");
        }

        ResourceTable LoadedFile;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainDataGridView.Rows.Clear();

            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a .srsizetable file",
                Filter = "RSTB (*.srsizetable)|*.srsizetable",
                DefaultExt = "*.srsizetable",
                FilterIndex = 1,
                RestoreDirectory = true,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                LoadedFile = new ResourceTable(filePath);
                PopulateGridView();
            }
        }

        private void PopulateGridView()
        {
            mainDataGridView.Rows.Clear();
            if (!LoadedFile.IsLoaded) return;

            DrawingControl.SuspendDrawing(mainDataGridView);

            foreach (var entry in LoadedFile.UniqueEntries)
            {
                var values = new List<object>() { entry.FileName, entry.FileSize };

                if (LoadedFile.HEADER == "RSTC")
                    values.Add(entry.DLC);

                mainDataGridView.Rows.Add([.. values]);
            }

            foreach (var entry in LoadedFile.RepeatedHashesEntries)
            {
                var values = new List<object>() { entry.FileName, entry.FileSize };

                if (LoadedFile.HEADER == "RSTC")
                    values.Add(entry.DLC);

                mainDataGridView.Rows.Add([.. values]);
            }

            DrawingControl.ResumeDrawing(mainDataGridView);
        }

        private void updateHashesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var choose = MessageBox.Show("This action need the entire RomFs dump! (Including game-updates and DLC)\nIf you don't have all these files CANCEL the operation.", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (choose == DialogResult.Cancel) return;

            var openFolderDialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = false,
                Description = "Select a RomFs directory",
                SelectedPath = Settings.Default.LastSelectedRomFsDirectory ?? ""
            };

            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = openFolderDialog.SelectedPath;
                Settings.Default.LastSelectedRomFsDirectory = selectedPath;
                Settings.Default.Save();

                var files = Directory.GetFiles(selectedPath, "*", SearchOption.AllDirectories);

                files = files.Select(x =>
                {
                    var text = Path.GetRelativePath(selectedPath, x).Replace('\\', '/');
                    //text = text.Substring(selectedPath.Length + 1);

                    if (text.EndsWith(".zs"))
                        text = text[..^3]; // Same as text.Substring(0, text.Length - 3);

                    if (text.EndsWith(".srsizetable"))
                        return null;

                    return text;
                }).Where(x => x != null).ToArray();

                // Save
                RomFsNameManager.Update(files);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null) return;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "RSTB/RSTC (*.srsizetable)|*.srsizetable",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            //LoadedFile.Entries.Clear();

            //foreach (DataGridViewRow row in mainDataGridView.Rows)
            //{
            //    var cells = row.Cells;

            //    if (cells.Count != 3) continue;
            //    if (row.Cells[0].Value is string name && row.Cells[1].Value is uint size && row.Cells[2].Value is uint dlc)
            //    {
            //        if (name.StartsWith("0x"))
            //        {
            //            bool parsedSuccessfully = uint.TryParse(name[2..], NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint enumHash);

            //            if (parsedSuccessfully)
            //            {
            //                LoadedFile.Entries.Add(new ResourceTable.ResourceTableEntry()
            //                {
            //                    CRCHash = enumHash,
            //                    DLC = enumHash
            //                });
            //            }

            //        }
            //        else
            //        {
            //            LoadedFile.Entries.Add(new ResourceTable.ResourceTableEntry()
            //            {
            //                FileName = name,
            //                DLC = dlc
            //            });
            //        }
            //    }
            //}

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                LoadedFile.SaveTo(saveFileDialog.FileName);
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileOpen = new OpenFileDialog();

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                var filePath = fileOpen.FileName;

                var size = GetFileSize(filePath);

                MessageBox.Show($"Size: {size}");
            }
        }

        internal uint GetFileSize(string fileName)
        {
            long size;

            // If it's a zstd file
            if (fileName.EndsWith(".zs"))
            {
                var decompressed = ZstdCompressionAlgorithm.Decompress(fileName);
                size = decompressed.Length;
            }
            else size = new FileInfo(fileName).Length;

            // Round up to the next number divisible by 32
            size = (size + 31) & -32;

            if (fileName.EndsWith(".zs"))
            {
                // Unclear where this padidng came from... but seems to be a constant for every zs file lol
                size += 9264;
            }
            else
            {
                // Seems to be a constant for non-zs files 
                size += 416;
            }

            if (size > uint.MaxValue)
                throw new Exception($"{fileName} is too big!");

            return Convert.ToUInt32(size);
        }

        private void checkIfFileSizesAreMatchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null || !LoadedFile.IsLoaded) return;

            var folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "Select your RomFs"
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var romFsPath = folderBrowserDialog.SelectedPath;

                var allFiles = Directory.GetFiles(romFsPath, "*", SearchOption.AllDirectories);
                bool success = true;

                foreach (var file in allFiles)
                {
                    var path = Path.GetRelativePath(romFsPath, file).Replace('\\', '/');

                    //MessageBox.Show(path);

                    if (LoadedFile.Dictionary.TryGetValue(path, out var result))
                    {
                        var fileSize = GetFileSize(file);
                        if (fileSize != result.FileSize)
                        {
                            MessageBox.Show($"Opsss! {path} have a different size!\nOriginal size: {result.FileSize}\nOur size: {fileSize}");
                            success = false;
                            break;
                        }
                    }
                    else
                    {

                    }
                }

                if (success) MessageBox.Show("Success validated all entries!");
            }
        }

        private void updateSizesFromModdedRomFsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null || !LoadedFile.IsLoaded) return;

            var folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "Select your RomFs"
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var romFsPath = folderBrowserDialog.SelectedPath;

                var allFiles = Directory.GetFiles(romFsPath, "*", SearchOption.AllDirectories);

                List<string> changedFiles = [];
                List<string> addedFiles = [];

                foreach (var originalFile in allFiles)
                {
                    var path = Path.GetRelativePath(romFsPath, originalFile).Replace('\\', '/');

                    // Remove .zs extension
                    if (path.EndsWith(".zs"))
                        path = path[..^3];

                    var fileSize = GetFileSize(originalFile);

                    if (LoadedFile.Dictionary.TryGetValue(path, out var result) && fileSize != result.FileSize)
                    {
                        result.FileSize = fileSize;
                        changedFiles.Add(path); 
                    }
                    else if(!LoadedFile.Dictionary.ContainsKey(path))
                    {
                        LoadedFile.AddEntry(new ResourceTable.ResourceTableEntry()
                        {
                            FileName = path,
                            CRCHash = path.ToCRC32(),
                            DLC = 0,
                            FileSize = fileSize
                        });

                        addedFiles.Add(path);
                    }
                }

                if (changedFiles.Count > 0 || addedFiles.Count > 0)
                {
                    MessageBox.Show($"Successfully updated table values!" +
                        (changedFiles.Count > 0 ? $"\nUpdated {changedFiles.Count} files." : "") +
                        (addedFiles.Count > 0 ? $"\nAdded {addedFiles.Count} files." : "") + 
                        "\n\nYou need to manually save your file in File > Save as...",
                        "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                // Repopulate grid view with up-to-date information
                PopulateGridView();

                foreach(DataGridViewRow row in mainDataGridView.Rows)
                {
                    var name = row.Cells[0].Value.ToString();

                    if (changedFiles.Contains(name))
                        row.DefaultCellStyle.BackColor = Color.Yellow;

                    if (addedFiles.Contains(name))
                        row.DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }
    }
}
