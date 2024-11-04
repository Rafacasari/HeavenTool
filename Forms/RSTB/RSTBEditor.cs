using HeavenTool.Properties;
using HeavenTool.Utility;
using HeavenTool.Utility.FileTypes.RSTB;
using HeavenTool.Utility.IO;
using HeavenTool.Utility.IO.Compression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTool.Forms.RSTB;

public partial class RSTBEditor : Form
{
    public RSTBEditor()
    {
        InitializeComponent();

        Text = $"Heaven Tool | {Program.VERSION} | RSTB Editor";

        // TODO: Use .CellTemplate instead. And make template for every needed type for both BCSV and RSTB (uint, hash, etc)
        // That way we don't need to parse user value every time to check if it is valid

        mainDataGridView.Columns["fileName"].ValueType = typeof(string);
        mainDataGridView.Columns["fileSize"].ValueType = typeof(uint);
        mainDataGridView.Columns["DLC"].ValueType = typeof(uint);
        mainDataGridView.ShowCellToolTips = false;

        statusProgressBar.Visible = false;
        statusBar.Visible = false;

        RefreshMenuButtons();
    }

    public ResourceTable LoadedFile { get; set; }

    private void RefreshMenuButtons()
    {
        saveAsToolStripMenuItem.Enabled = LoadedFile?.IsLoaded == true;
        updateFromModdedRomFs.Enabled = LoadedFile?.IsLoaded == true;
        closeFileToolStripMenuItem.Enabled = LoadedFile?.IsLoaded == true;
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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
            LoadFile(filePath);
        }
    }

    public void LoadFile(string path)
    {
        LoadedFile = new ResourceTable(path);
        PopulateGridView();

        RefreshMenuButtons();
    }

    private void PopulateGridView()
    {
        mainDataGridView.Rows.Clear();
        if (LoadedFile?.IsLoaded != true) return;

        DrawingControl.SuspendDrawing(mainDataGridView);

        var uniqueEntries = LoadedFile.Dictionary.Values.Where(x => !x.IsCollided).ToList();
        var nonUniqueEntries = LoadedFile.Dictionary.Values.Where(x => x.IsCollided).ToList();

        foreach (var entry in uniqueEntries)
        {
            var values = new List<object>() { entry.FileName, entry.FileSize };

            if (LoadedFile.IsRSTC)
                values.Add(entry.DLC);

            mainDataGridView.Rows.Add([.. values]);
        }

        foreach (var entry in nonUniqueEntries)
        {
            var values = new List<object>() { entry.FileName, entry.FileSize };

            if (LoadedFile.HEADER == "RSTC")
                values.Add(entry.DLC);

            var row = mainDataGridView.Rows.Add([.. values]);
        }

        statusBar.Visible = true;
        statusLabel.Text = $"Entries: {LoadedFile.Dictionary.Count} ({LoadedFile.UniqueEntries().Count}/{LoadedFile.NonUniqueEntries().Count})";
        
        DrawingControl.ResumeDrawing(mainDataGridView);
    }

    private void UpdateHashesToolStripMenuItem_Click(object sender, EventArgs e)
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

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null) return;

        var saveFileDialog = new SaveFileDialog
        {
            Filter = "RSTB/RSTC (*.srsizetable)|*.srsizetable",
            FilterIndex = 1,
            RestoreDirectory = true,
            OverwritePrompt = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
            LoadedFile.SaveTo(saveFileDialog.FileName);
    }

    internal static uint GetFileSize(string fileName)
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

    private void CheckIfFileSizesAreMatchingToolStripMenuItem_Click(object sender, EventArgs e)
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

    private void UpdateFromModdedRomFs_Click(object sender, EventArgs e)
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

            TopMenu.Enabled = false;

            statusLabel.Text = $"";
            statusProgressBar.Visible = true;
            statusProgressBar.Maximum = allFiles.Length;
            statusProgressBar.Value = 0;

            void updateStatus(int quantity, string fileName)
            {
                if (IsDisposed || Disposing) return;

                if (statusBar.InvokeRequired)
                    statusBar.BeginInvoke(() => statusLabel.Text = $"Getting file size... {fileName} ({quantity}/{allFiles.Length})");

                else
                    statusLabel.Text = $"Getting file size... ({quantity}/{allFiles.Length})";
            }

            void updateProgress(int quantity)
            {
                if (IsDisposed || Disposing) return;

                if (statusBar.InvokeRequired)
                    statusBar.BeginInvoke(() => statusProgressBar.Value = quantity);
                
                else
                    statusProgressBar.Value= quantity;
            }


            Task.Factory.StartNew(() =>
            {
                int currentPosition = 1;
                foreach (var originalFile in allFiles)
                {
                    if (IsDisposed || Disposing) break;

                    var path = Path.GetRelativePath(romFsPath, originalFile).Replace('\\', '/');
                    updateStatus(currentPosition, path);

                    // Remove .zs extension
                    if (path.EndsWith(".zs"))
                        path = path[..^3];

                    var fileSize = GetFileSize(originalFile);

                    if (IsDisposed || Disposing) break;
                    if (LoadedFile.Dictionary.TryGetValue(path, out var result) && fileSize != result.FileSize)
                    {
                        result.FileSize = fileSize;
                        changedFiles.Add(path);
                    }
                    else if (!LoadedFile.Dictionary.ContainsKey(path))
                    {
                        LoadedFile.AddEntry(new ResourceTable.ResourceTableEntry(path, fileSize, false));

                        addedFiles.Add(path);
                    }

                    updateProgress(currentPosition);
                    currentPosition++;
                }

                if (IsDisposed || Disposing) return;

                BeginInvoke(() =>
                {
                    TopMenu.Enabled = true;
                    statusProgressBar.Visible = false;

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

                    foreach (DataGridViewRow row in mainDataGridView.Rows)
                    {
                        var name = row.Cells[0].Value.ToString();

                        if (changedFiles.Contains(name))
                            row.DefaultCellStyle.BackColor = Color.Yellow;

                        if (addedFiles.Contains(name))
                            row.DefaultCellStyle.BackColor = Color.Green;
                    }
                });
                
            });
        }
    }

    private void UpdateHashListToolStripMenuItem_Click(object sender, EventArgs e)
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

    private void CloseFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LoadedFile.Dispose();
        LoadedFile = null;

        mainDataGridView.Rows.Clear();
        mainDataGridView.Dispose();

        RefreshMenuButtons();
    }
}
