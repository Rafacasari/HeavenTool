using HeavenTool.BCSV;
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

namespace HeavenTool.Forms
{
    public partial class DirectorySearch : Form
    {
        public DirectorySearch()
        {
            InitializeComponent();
        }

        private void selectDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
                directoryPath.Text = folderBrowserDialog.SelectedPath;

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(directoryPath.Text))
            {
                foreach (var file in Directory.GetFiles(directoryPath.Text))
                {
                    if (Path.GetExtension(file) != ".bcsv")
                        continue;

                    ReadBCSVFileAndSearch(file);
                }
            }
        }


        private void ReadBCSVFileAndSearch(string path)
        {
            if (!File.Exists(path))
                return;


            using (var reader = new BinaryFileReader(File.OpenRead(path)))
            {
                uint numEntries = reader.ReadUInt32();
                uint entrySize = reader.ReadUInt32();
                ushort numFields = reader.ReadUInt16();
                byte flag1 = reader.ReadByte();
                byte flag2 = reader.ReadByte();

                if (flag1 == 1)
                {
                    uint magic = reader.ReadUInt32();
                    uint unk = reader.ReadUInt32(); //Always 100000
                    uint unk2 = reader.ReadUInt32();//0
                    uint unk3 = reader.ReadUInt32();//0 
                }



                Field[] fields = new Field[numFields];
                for (int i = 0; i < numFields; i++)
                {
                    fields[i] = new Field()
                    {
                        Hash = reader.ReadUInt32(),
                        Offset = reader.ReadUInt32(),
                    };

                    var name = fields[i].GetTranslatedNameOrHash();
                        
                    if((containButton.Checked && name.ToLower().Contains(searchField.Text)) || name == searchField.Text)
                        hitsFound.Items.Add($"{Path.GetFileNameWithoutExtension(path)}: {name} (header)");
                }



                for (int i = 0; i < numEntries; i++)
                {
                    long pos = reader.Position;
                    for (int f = 0; f < fields.Length; f++)
                    {

                        uint size = entrySize - fields[f].Offset;

                        if (f < fields.Length - 1)
                            size = fields[f + 1].Offset - fields[f].Offset;

                        DataType type = DataType.String;
                        switch (size)
                        {
                            case 1:
                                type = DataType.U8;
                                break;
                            case 2:
                                type = DataType.UInt16;
                                break;
                            case 4:

                                type = DataType.UInt32;
                                var translatedName = fields[f].GetTranslatedNameOrNull();
                                if (translatedName != null)
                                {
                                    if (translatedName.EndsWith(".hshCstringRef"))
                                        type = DataType.HashedCsc32;

                                    else if (translatedName.EndsWith(" f32"))
                                        type = DataType.Float32;
                                }

                                break;

                        }


                        reader.SeekBegin(pos + fields[f].Offset);
                        object value = 0;

                        switch (type)
                        {
                            case DataType.U8:
                                value = reader.ReadByte();
                                break;
                            case DataType.Float32:
                                value = reader.ReadSingle();
                                break;
                            case DataType.UInt16:
                                value = reader.ReadInt16();
                                break;
                            case DataType.UInt32:
                                value = reader.ReadInt32();
                                break;

                            case DataType.HashedCsc32:
                                {
                                    var hash = reader.ReadUInt32();
                                    if (!MainFrm.LoadedHashes.ContainsKey(hash))
                                        value = hash.ToString("x");
                                    else value = MainFrm.LoadedHashes[hash];
                                    break;
                                }

                            case DataType.String:
                                value = reader.ReadString((int) size, Encoding.UTF8);
                                break;
                        }

                        if ((containButton.Checked && value.ToString().ToLower().Contains(searchField.Text)) || value.ToString() == searchField.Text)
                            hitsFound.Items.Add($"{Path.GetFileNameWithoutExtension(path)}: {value} (value)");


                    }


                    // Go to next entry in memory
                    reader.SeekBegin(pos + entrySize);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
