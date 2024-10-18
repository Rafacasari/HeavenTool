using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeavenTool.Forms.PBC;
using HeavenTool.Utility.IO;
using NintendoTools.Compression.Zstd;
using NintendoTools.FileFormats;
using NintendoTools.FileFormats.Sarc;

namespace HeavenTool.Forms.SARC;

public partial class SarcEditor : Form
{
    public SarcEditor()
    {
        InitializeComponent();
    }
    private static readonly ZstdCompressor Compressor = new();
    private static readonly ZstdDecompressor Decompressor = new();
    private static readonly SarcFileParser SarcFileParser = new();
    private static readonly SarcFileCompiler SarcCompiler = new();

    private string LoadedFileName {  get; set; }
    private SarcFile LoadedFile;

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        filesTreeView.Nodes.Clear();

        var openFileDialog = new OpenFileDialog()
        {
            Title = "Open a SARC file",
            CheckFileExists = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            var path = openFileDialog.FileName;
            
            Stream file = File.OpenRead(path);
            MemoryStream fileStream = new();

            // Check for compressor
            if (Decompressor.CanDecompress(file))
                Decompressor.Decompress(file, fileStream);
            else
                // No compressor found, copy file to memoryStream
                file.CopyTo(fileStream);


            if (fileStream.Length == 0) throw new Exception("Failed to open SARC file!");

            LoadedFileName = Path.GetFileName(path);
            LoadedFile = SarcFileParser.Parse(fileStream);

            foreach (var sarcContent in LoadedFile.Files)
            {

                var treeNode = filesTreeView.Nodes.Add(sarcContent.Name);
                var context = new ContextMenuStrip();


                if (sarcContent.Name.EndsWith(".pbc"))
                {
                    var item = context.Items.Add("Open PBC Editor", null, (_, _) =>
                    {
                        var editor = new PBCEditor(sarcContent.Data);
                        editor.Show();
                    });
                }

                context.Items.Add("Export Data...", null, (_, _) =>
                {
                    var saveFileDialog = new SaveFileDialog()
                    {
                        FileName = Path.GetFileName(sarcContent.Name),
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, sarcContent.Data);
                    }
                });

                context.Items.Add("Replace Data...", null, (_, _) =>
                {
                    var openFileDialog = new OpenFileDialog()
                    {
                        Title = "Select a file to replace"
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var stream = File.OpenRead(openFileDialog.FileName);
                        sarcContent.Data = stream.ToArray();
                    }
                });

                treeNode.ContextMenuStrip = context;


            }

            filesTreeView.Invalidate();
        }


    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (LoadedFile == null) return;

        var memoryStream = new MemoryStream();

        SarcCompiler.Compile(LoadedFile, memoryStream);

        var msg = MessageBox.Show("Do you want to compress with Zstd?", "ZSTD Compression", MessageBoxButtons.YesNo);
        bool isCompressed = false;

        if (msg == DialogResult.Yes)
        {
            var compressedStream = new MemoryStream();
            Compressor.Compress(memoryStream, compressedStream);

            // Make sure we are at start
            memoryStream.Position = 0;

            compressedStream.Position = 0;
            compressedStream.CopyTo(memoryStream);

            isCompressed = true;
        }

        var saveFileDialog = new SaveFileDialog()
        {
            Title = "Select where you want to save",
            FileName = isCompressed ? $"{LoadedFileName}.Nin_NX_NVN.zs" : LoadedFileName
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            var path = saveFileDialog.FileName;
            File.WriteAllBytes(path, memoryStream.ToArray());
        }
    }
}
