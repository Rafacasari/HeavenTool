using HeavenTool.Forms.Search;
using HeavenTool.Properties;
using HeavenTool.Utility.FileTypes.RSTB;
using HeavenTool.Utility.IO;
using HeavenTool.Utility.IO.Compression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTool.Forms.RSTB;

public partial class RSTBEditor : Form, ISearchable
{
    private SearchBox searchBox;
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

        associateSrsizetableToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".srsizetable") == Application.ExecutablePath;
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

        if (LoadedFile == null || !LoadedFile.IsLoaded)
        {
            MessageBox.Show("Failed to load this file!");
            return;
        }

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
            List<object> values = [entry.FileName, entry.FileSize];

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

    internal static long GetFileSize(string fileName, bool isFileCheck = false)
    {
        long size;

        // If it's a zstd file
        if (fileName.EndsWith(".zs") && !fileName.StartsWith("Layout/"))
        {
            if (!isFileCheck) return -1;

            var decompressed = ZstdCompressionAlgorithm.Decompress(fileName);
            size = decompressed.Length;
        }
        else size = new FileInfo(fileName).Length;

        // Round up to the next number divisible by 32
        size = (size + 31) & -32;

        if (fileName.StartsWith("Layout/"))
            size += 8192;
        else if (fileName.EndsWith(".bars"))
            size += 712;
        else if (fileName.EndsWith(".bcsv") || fileName.EndsWith(".bfevfl") || fileName.EndsWith(".byml"))
            size += 416;
        else if (!isFileCheck) return -2; // unsupported file

        if (size > uint.MaxValue)
            throw new Exception($"{fileName} is too big!");

        return size;
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

                if (path.StartsWith("Model/UnitIcon") || path.StartsWith("Model/Layout_MessageCard_") || path.StartsWith("Swkbd/") || path.StartsWith("System/") || path.EndsWith("srsizetable") || path.EndsWith(".bdli.zs") || path.EndsWith(".bwav") || path.EndsWith(".ad1") || path.EndsWith(".a") || path.EndsWith(".byml") || path.EndsWith(".xml")) continue;

                if (LoadedFile.Dictionary.TryGetValue(path, out var result))
                {
                    var fileSize = GetFileSize(file, true);
                    if (fileSize != result.FileSize)
                    {
                        Console.WriteLine($"Opsss! {path} have a different size!\nOriginal size: {result.FileSize}\nOur size: {fileSize}");
                        success = false;
                        //break;
                    }
                }
                else
                {
                    Console.WriteLine($"{path} is not present on the file.");
                }
            }

            if (success) MessageBox.Show("Success validated all entries!");
        }
    }

    private async void UpdateFromModdedRomFs_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null || !LoadedFile.IsLoaded) return;

        var folderBrowserDialog = new FolderBrowserDialog()
        {
            Description = "Select your RomFs"
        };

        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            var moddedRomFsPath = folderBrowserDialog.SelectedPath;

            var allFiles = Directory.GetFiles(moddedRomFsPath, "*", SearchOption.AllDirectories);

            List<string> changedFiles = [];
            List<string> addedFiles = [];
            int skippedFiles = 0;
            int removedFiles = 0;

            TopMenu.Enabled = false;

            statusLabel.Text = $"";
            statusBar.Visible = true;
            statusProgressBar.Visible = true;
            statusProgressBar.Maximum = allFiles.Length;
            statusProgressBar.Value = 0;

            var progress = new Progress<(int Index, string FileName)>(value =>
            {
                statusLabel.Text = $"Getting file size... {value.FileName} ({value.Index}/{allFiles.Length})";
                statusProgressBar.Value = value.Index;
            });

            await Task.Run(() =>
            {
                int currentPosition = 1;

                foreach (var originalFile in allFiles)
                {
                    if (IsDisposed || Disposing) break;

                    var path = Path.GetRelativePath(moddedRomFsPath, originalFile).Replace('\\', '/');
                    if (path == "System/Resource/ResourceSizeTable.srsizetable" || path == "System/Resource/ResourceSizeTable.rsizetable")
                    {
                        skippedFiles++;
                        continue;
                    }

                    if (path.EndsWith(".byml") && path != "EventFlow/Info/EventFlowInfoProduct.byml")
                        continue;
                    

                    (progress as IProgress<(int, string)>).Report((currentPosition, path));

                    if (path.EndsWith(".zs"))
                        path = path[..^3];

                    var fileSize = GetFileSize(originalFile);

                    if (fileSize < 0)
                    {
                        // remove unsupported file from rstb
                        if (fileSize == -2)
                            Console.WriteLine("Unsupported: {0}", path);

                        LoadedFile.Dictionary.Remove(path);
                        removedFiles++;
                    }
                    else if (LoadedFile.Dictionary.TryGetValue(path, out var result) && fileSize >= 0 && fileSize != result.FileSize)
                    {
                        result.FileSize = (uint) fileSize;
                        changedFiles.Add(path);
                    }

                    else if (!LoadedFile.Dictionary.ContainsKey(path) && fileSize >= 0)
                    {
                        LoadedFile.AddEntry(new ResourceTable.ResourceTableEntry(path, (uint) fileSize, false));

                        addedFiles.Add(path);
                    }

                    currentPosition++;
                }

                if (IsDisposed || Disposing) return;

            });

            TopMenu.Enabled = true;
            statusBar.Visible = false;
            statusProgressBar.Visible = false;

            if (changedFiles.Count > 0 || addedFiles.Count > 0)
            {
                MessageBox.Show($"Successfully updated table values!" +
                                (changedFiles.Count > 0 ? $"\nUpdated {changedFiles.Count} entries." : "") +
                                (addedFiles.Count > 0 ? $"\nAdded {addedFiles.Count} entries." : "") +
                                (skippedFiles > 0 ? $"\nSkipped {skippedFiles} entries." : "") +
                                (removedFiles > 0 ? $"\nRemoved {removedFiles} entries." : "") +
                                "\n\nYou need to manually save your file in File > Save as...",
                    "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            PopulateGridView();

            foreach (DataGridViewRow row in mainDataGridView.Rows)
            {
                var name = row.Cells[0].Value.ToString();

                if (changedFiles.Contains(name))
                    row.DefaultCellStyle.BackColor = Color.Yellow;

                if (addedFiles.Contains(name))
                    row.DefaultCellStyle.BackColor = Color.Green;
            }
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

                if (text.EndsWith(".zs"))
                    text = text[..^3];

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

    private void associateSrsizetableToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exePath = Application.ExecutablePath;
        var arguments = $"--{(associateSrsizetableToolStripMenuItem.Checked ? "disassociate" : "associate")} srsizetable";


        var process = Process.Start(new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = arguments,
            Verb = "runas",
            UseShellExecute = true
        });

        process.WaitForExit();
        associateSrsizetableToolStripMenuItem.Checked = ProgramAssociation.GetAssociatedProgram(".srsizetable") == Application.ExecutablePath;
    }

    private void addNewEntriesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        addNewEntriesToolStripMenuItem.Checked = !addNewEntriesToolStripMenuItem.Checked;
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
    internal void ClearSearchCache()
    {
        if (searchCache != null && searchCache.Length > 0)
        {
            SearchClosing();

            lastSearchCell = null;
            searchCache = null;
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

    private void searchToolStripMenuItem_Click(object sender, EventArgs e)
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

    private async void compareDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var openFolderDialog = new FolderBrowserDialog()
        {
            ShowNewFolderButton = false,
            Description = "Select a RomFs directory",
            SelectedPath = Settings.Default.LastSelectedRomFsDirectory ?? ""
        };

        if (openFolderDialog.ShowDialog() == DialogResult.OK)
        {
            var selectedPath = openFolderDialog.SelectedPath;

            if (selectedPath == null || !Directory.Exists(selectedPath)) return;

            if (!mainDataGridView.Columns.Contains("Diff"))
            {
                mainDataGridView.Columns.Add("Diff", "Diff");
                mainDataGridView.Columns["Diff"].ValueType = typeof(long);
            }

            var progress = new Progress<(int Index, string FileName)>(value =>
            {
                statusLabel.Text = $"Loading: {value.FileName} ({value.Index}/{mainDataGridView.RowCount})";
                statusProgressBar.Value = value.Index;
            });

            statusBar.Visible = true;
            statusProgressBar.Visible = true;
            statusProgressBar.Maximum = mainDataGridView.RowCount;
            statusProgressBar.Value = 0;

            int zstdFiles = 0;
            foreach (DataGridViewRow row in mainDataGridView.Rows)
            {
                await Task.Run(() =>
                {
                    var fileName = row.Cells["fileName"].Value.ToString();
                    var actualPath = Path.Combine(selectedPath, fileName);
                    long zstdSize = -1;

                    (progress as IProgress<(int, string)>).Report((row.Index, fileName));

                    if (!File.Exists(actualPath) && File.Exists(actualPath + ".zs"))
                    {
                        actualPath += ".zs";
                        if (zstdFiles < 5000)
                        {
                            zstdSize = ZstdCompressionAlgorithm.Decompress(actualPath).Length;
                            zstdSize = (zstdSize + 31) & -32;
                            zstdFiles++;
                        }
                    }

                    row.Cells["Diff"].Value = (long)-1;

                    if (row.Cells["fileSize"].Value is uint fileSize)
                    {
                        if (zstdSize >= 0)
                            row.Cells["Diff"].Value = fileSize - zstdSize;
                        else if (File.Exists(actualPath) && !actualPath.EndsWith(".zs"))
                        {
                            var roundSize = (new FileInfo(actualPath).Length + 31) & -32;
                            row.Cells["Diff"].Value = fileSize - roundSize;
                        }
                    }
                });
               
            }

            statusBar.Visible = false;
            statusProgressBar.Visible = false;
        }
    }
}
