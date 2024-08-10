using HeavenTool.BCSV;
using Force.Crc32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTool.Forms
{
    public partial class BruteForce : Form
    {
        public BruteForce()
        {
            InitializeComponent();
        }

        public class MissingHash
        {
            public uint Hash;
            public string Type;
            public string File;

            public MissingHash( uint hash, string type, string file)
            {
                Hash = hash;
                Type = type;
                File = file;
            }
        }

        private static List<MissingHash> missingHashes = new List<MissingHash>();

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
                    
                    ReadBCSVFile(file);   
                }
            }

            foreach(var hash in UnknownHashes)
            {
                listBox1.Items.Add("0x" + hash.ToString("x"));
            }
        }

        private static List<uint> UnknownHashes = new List<uint>();
        private List<uint> UnknownStringHashes = new List<uint>();
        private const bool INCLUDE_STRING_HASHES = false;
        private const string WEBHOOK = "";

        private void ReadBCSVFile(string path)
        {
            if (!File.Exists(path))
                return;

            var fileName = Path.GetFileNameWithoutExtension(path);

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



                    if (fields[i].GetTranslatedNameOrNull() == null)
                    {
                        UnknownHashes.Add(fields[i].Hash);
                    }

                }

                for(int f = 0; f < fields.Length; f++)
                {
                    if (!UnknownHashes.Contains(fields[f].Hash))
                        continue;

                    uint size = entrySize - fields[f].Offset;
                    if (f < fields.Length - 1)
                        size = fields[f + 1].Offset - fields[f].Offset;

                    var typeString = "string";
                    switch (size)
                    {
                        default:
                            typeString = $"string{size}?";
                            break;
                        case 1:
                            typeString = "byte?";
                            break;
                        case 2:
                            typeString = "number (u16/s16)";
                            break;
                        case 4:
                            typeString = "hshCstringRef/f32/u32";
                            break;

                    }    

                    missingHashes.Add(new MissingHash(fields[f].Hash, typeString, fileName));
                }


#pragma warning disable CS0162 // Código inacessível detectado
                if (INCLUDE_STRING_HASHES)
                {
                    for (int i = 0; i < numEntries; i++)
                    {
                        long pos = reader.Position;


                        for (int f = 0; f < fields.Length; f++)
                        {

                            uint size = entrySize - fields[f].Offset;
                            // If current field isn't the last one
                            if (f < fields.Length - 1)
                                // Then get the size based on the next field offset
                                size = fields[f + 1].Offset - fields[f].Offset;

                            // TODO: Move type to Field so we can actually save the document later on
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

                                        // Otherwise will still be an int
                                    }

                                    break;

                            }


                            reader.SeekBegin(pos + fields[f].Offset);
                            string name = fields[f].GetTranslatedNameOrHash();
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
                                            UnknownStringHashes.Add(hash);
                                        break;
                                    }

                                //case DataType.String:
                                //    value = reader.ReadZeroTerminatedString(Encoding.UTF8);
                                //    break;
                            }


                        }


                        // Go to next entry in memory
                        reader.SeekBegin(pos + entrySize);
                    }
                }
                #pragma warning restore CS0162 // Código inacessível detectado
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        static void SendMs(string message)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            string payload = "{\"content\": \"" + message + "\"}";
            client.UploadData(WEBHOOK, Encoding.UTF8.GetBytes(payload));
        }

        private static List<string> QueueMessages = new List<string>();
        static void SendMsQueue(string message)
        {
            if (QueueMessages.Count == 19)
            {
                QueueMessages.Add(message);
                var queueMsg = String.Join(" | ", QueueMessages.ToArray());
                QueueMessages.Clear();

                SendMs(queueMsg);
            } else { QueueMessages.Add(message); }
        }

        private void startBruteForceButton_Click(object sender, EventArgs e)
        {
            SendMs($"Started brute-force to find {UnknownHashes.Count} hashes");
            BruteForceCrc32(15);
        }

        private static string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static string[] suffixes = {
            " u8", " u16", " u32", " f32", " string4", " string8", " string20", " string25",
            " string30", " string32", " string33", " string50", " string60", " string64",
            " string65", " string66", " string128", " s8", " s16", " s32", ".hshCstringRef"
        };
        //private static string[] suffixes = {
        //    " u32", " f32", " string32", " s32", ".hshCstringRef"
        //};

        static void BruteForceCrc32(int maxLength)
        {
            ConcurrentBag<string> results = new ConcurrentBag<string>();
            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            for (int length = 1; length <= maxLength; length++)
            {
                Parallel.ForEach(suffixes, parallelOptions, suffix =>
                {
                    BruteForceCrc32Recursive(charset.ToCharArray(), new char[length], 0, suffix, results);
                });
            }

            if (results.IsEmpty)
            {
                SendMs("Scan complete: No matches found");

                if (QueueMessages.Count > 0)
                {
                    var queueMsg = String.Join(" | ", QueueMessages.ToArray());
                    QueueMessages.Clear();

                    SendMs(queueMsg);
                }
            }
        }

        static void BruteForceCrc32Recursive(char[] chars, char[] result, int position, string suffix, ConcurrentBag<string> results)
        {
            if (position == result.Length)
            {
                string candidate = new string(result) + suffix;
                byte[] bytes = Encoding.UTF8.GetBytes(candidate);
                uint crc32 = Crc32Algorithm.Compute(bytes);

                if (UnknownHashes.Contains(crc32))
                {
                    results.Add(candidate);
                    SendMsQueue($"`{candidate}`");
                }
                return;
            }

            foreach (char c in chars)
            {
                result[position] = c;
                BruteForceCrc32Recursive(chars, result, position + 1, suffix, results);
            }
        }

        private void exportMissingHashesButton_Click(object sender, EventArgs e)
        {
            if (missingHashes.Count == 0)
                return;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog.SelectedPath;

                var groups = missingHashes.GroupBy(hash => hash.File);

                foreach(var group in groups)
                {
                    var path = Path.Combine(selectedPath, group.Key + ".txt");
                    var content = group.Select(hash => $"{hash.Hash:x} - {hash.Type}").ToArray();

                    File.WriteAllLines(path, content);
                }
            }
        }
    }
}
