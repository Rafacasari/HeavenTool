using HeavenTool.Forms.Pack;
using HeavenTool.Forms.RSTB;
using HeavenTool.Forms.SARC;
using HeavenTool.IO;
using HeavenTool.IO.Compression;
using HeavenTool.Utility;
using HeavenTool.Utility.FileTypes.BCSV;
using HeavenTool.Utility.FileTypes.BCSV.Exporting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeavenTool;

public partial class HeavenMain : Form
{
    public HeavenMain()
    {
        InitializeComponent();

        Text = $"Heaven Tool {Program.VERSION}";
    }

    // Forms
    public static BCSVForm bcsvEditor = new();
    public static BCSVRework bcsvRework = new();
    public static RSTBEditor rstbEditor = new();
    public static SarcEditor sarcEditor = new();
    public static ItemIDHelper itemIDHelper = new();

    private void bcsvEditorButton_Click(object sender, EventArgs e)
    {
        if (bcsvEditor.IsDisposed) bcsvEditor = new();

        bcsvEditor.Show();
        bcsvEditor.BringToFront();
    }

    private void rstbEditorButton_Click(object sender, EventArgs e)
    {
        if (rstbEditor.IsDisposed) rstbEditor = new();

        rstbEditor.Show();
        rstbEditor.BringToFront();
    }

    private void sarcEditorButton_Click(object sender, EventArgs e)
    {
        if (sarcEditor.IsDisposed) sarcEditor = new();

        sarcEditor.Show();
        sarcEditor.BringToFront();
    }

    private void itemParamHelperButton_Click(object sender, EventArgs e)
    {
        if (itemIDHelper.IsDisposed) itemIDHelper = new();

        itemIDHelper.Show();
        itemIDHelper.BringToFront();
    }

    private void yaz0DecompressToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var openFileDialog = new OpenFileDialog()
        {
            Multiselect = false
        };

        var selectedFile = openFileDialog.ShowDialog();

        if (selectedFile == DialogResult.OK
            && !string.IsNullOrEmpty(openFileDialog.FileName)
            && File.Exists(openFileDialog.FileName))
        {
            using var fileStream = File.OpenRead(openFileDialog.FileName);
            //MemoryStream memoryStream = new();

            byte[] decompressedBytes = Yaz0CompressionAlgorithm.Decompress(fileStream)?.ToArray();


            if (decompressedBytes == null) return;

            var saveFileDialog = new SaveFileDialog()
            {
                FileName = openFileDialog.FileName,
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var savePath = saveFileDialog.FileName;
                File.WriteAllBytes(savePath, decompressedBytes);
            }
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (bcsvRework.IsDisposed) bcsvRework = new();

        bcsvRework.Show();
        bcsvRework.BringToFront();
    }

    private void ExportLabelsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var folderBrowserDialog = new FolderBrowserDialog();

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

    private void ExportEnumsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var folderBrowserDialog = new FolderBrowserDialog();

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
                    var hashedFields = bcsvFile.Fields.Where(x => x.DataType == BCSVDataType.HashedCsc32).ToList();
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
                                if (entry[field.HEX] is uint fieldHash && !parsedList.Contains(fieldHash))
                                {
                                    if (BCSVHashing.CRCHashes.TryGetValue(fieldHash, out string value))
                                        registers.Add(value);


                                    parsedList.Add(fieldHash);
                                }
                            }

                            File.WriteAllLines(outputFile, registers);
                        }
                    }
                }

                Process.Start(outputDirectory);
            }
        }
    }

    private void exportCFGToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var openFolderDialog = new FolderBrowserDialog()
        {
            ShowNewFolderButton = false,
            Description = "Select BCSV folder",
            UseDescriptionForTitle = true
        };

        if (openFolderDialog.ShowDialog() == DialogResult.OK)
        {
            var selectedPath = openFolderDialog.SelectedPath;
            if (selectedPath == null || !Directory.Exists(selectedPath)) return;


            var dir = new Dictionary<string, BCSVExporter.BcsvHeader>();
            var crcList = new List<string>();
            var murmurList = new List<string>();

            var bcsvFiles = Directory.GetFiles(selectedPath, "*.bcsv");
            foreach (var bcsvFile in bcsvFiles)
            {
                var bcsv = new BinaryCSV(bcsvFile);

                if (bcsv == null) continue;

                foreach (var field in bcsv.Fields)
                {
                    // Find Enums
                    if (field.DataType == BCSVDataType.HashedCsc32 || field.DataType == BCSVDataType.Murmur3)
                    {
                        foreach (var entry in bcsv.Entries)
                        {
                            var entryValue = entry[field.HEX];
                            if (entryValue == null) continue;

                            if (entryValue is uint hash)
                            {
                                if (field.DataType == BCSVDataType.HashedCsc32 && BCSVHashing.CRCHashes.TryGetValue(hash, out string crcValue))
                                    crcList.AddIfNotExist(crcValue);
                                else if (field.DataType == BCSVDataType.Murmur3 && BCSVHashing.MurmurHashes.TryGetValue(hash, out string murmurValue))
                                    murmurList.AddIfNotExist(murmurValue);
                            }
                        }
                    }

                    // Get Header Info
                    if (dir.ContainsKey(field.HEX))
                        continue;

                    var headerInfo = new BCSVExporter.BcsvHeader()
                    {
                        Hash = field.GetTranslatedNameOrNull() != null ? string.Empty : $"0x{field.HEX}",
                        Name = field.GetTranslatedNameOrNull() ?? string.Empty
                    };

                    if (field.TrustedType)
                        headerInfo.DataType = field.DataType.GetName();

                    if (!string.IsNullOrEmpty(headerInfo.DataType) || !string.IsNullOrEmpty(headerInfo.Name))
                        dir.Add(field.HEX, headerInfo);
                }
            }

            var gameConfig = new BCSVExporter.GameConfig()
            {
                Bcsv = new BCSVExporter.BcsvConfig()
                {
                    Headers = [.. dir.Values],
                    FieldHashes = new BCSVExporter.FieldHashes()
                    {
                        MurmurHashes = murmurList,
                        CRCHashes = crcList
                    }
                }
            };

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CFG (*.cfg)|*.cfg",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                gameConfig.ExportConfig(saveFileDialog.FileName);

        }
    }

    private void exportUsedHashesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var folderBrowserDialog = new FolderBrowserDialog();

        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            string dir = "Exported Hashes";
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
                    if (BCSVHashing.CRCHashes.TryGetValue(item.Hash, out string hashName) && !usedHashesHeaders.Contains(hashName))
                        usedHashesHeaders.Add(hashName);


                foreach (var entry in bcsvFile.Entries)
                    foreach (var entryField in entry)
                        if (entryField.Value is uint hashValue && (hashValue > 100000))
                        {
                            if (BCSVHashing.CRCHashes.TryGetValue(hashValue, out string hashName) && !usedHashesValues.Contains(hashName))
                                usedHashesValues.Add(hashName);

                        }


            }

            File.WriteAllLines(Path.Combine(dir, "headers.txt"), usedHashesHeaders);
            File.WriteAllLines(Path.Combine(dir, "values.txt"), usedHashesValues);

            // Open directory
            Process.Start(dir);
        }
    }
}
