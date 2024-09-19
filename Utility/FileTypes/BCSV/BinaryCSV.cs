using HeavenTool.Utility.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeavenTool.Utility.FileTypes.BCSV
{
    public class BinaryCSV : IDisposable
    {
        internal uint EntrySize { get; set; }

        // TODO: remove this and use Fields.Lenght instead
        private ushort FieldCount { get; set; }

        public Field[] Fields { get; set; }

        public Dictionary<string, Field> Cache { get; set; }

        public Field GetFieldByHashedName(string hashedName)
        {
            if (Cache == null) Cache = new Dictionary<string, Field>();
            if (Fields == null) return null;

            if (Cache.ContainsKey(hashedName)) return Cache[hashedName];

            Field foundField = null;

            try
            {
                foundField = Fields.First(x => x.HEX == hashedName);
            }
            catch { 
                // Not found
            }

            Cache.Add(hashedName, foundField);
            return foundField;
        }

        public List<BCSVEntry> Entries { get; set; }

        internal ushort version;
        internal string fileType;
        internal uint unk1;
        internal uint unk2;
        internal uint unk3;

        public BinaryCSV(uint entrySize, Field[] fields, BCSVEntry[] entries, ushort version, string fileType, uint unk1, uint unk2, uint unk3)
        {
            EntrySize = entrySize;
            FieldCount = Convert.ToUInt16(fields.Length);
            Fields = fields;
            Entries = entries.ToList();

            this.version = version;
            this.fileType = fileType;
            this.unk1 = unk1;
            this.unk2 = unk2;
            this.unk3 = unk3;
        }

        public static BinaryCSV CopyFileWithoutEntries(BinaryCSV fileToCopy) => new BinaryCSV(fileToCopy.EntrySize, fileToCopy.Fields, new BCSVEntry[0], fileToCopy.version, fileToCopy.fileType, fileToCopy.unk1, fileToCopy.unk2, fileToCopy.unk3);


        public BinaryCSV(string filePath)
        {
            //var fileStream = File.OpenRead(filePath);
            using (var fileStream = File.OpenRead(filePath))
            using (var reader = new BinaryFileReader(fileStream))
            {
                // entryCount is a local variable cause entries can be edited by the user, so this value isn't really important when saving
                // we use entries.Count at saving instead of this one
                var entryCount = reader.ReadUInt32();
                EntrySize = reader.ReadUInt32();
                FieldCount = reader.ReadUInt16();

                // version
                version = reader.ReadUInt16(); // 257

                // BCSV but inverted "VSCB"
                fileType = reader.ReadString(4, Encoding.ASCII);

                // What I think is the "file revision/version"
                unk1 = reader.ReadUInt32(); // 20100
                unk2 = reader.ReadUInt32(); // 0
                unk3 = reader.ReadUInt32(); // 0
                

                // Read Fields
                Fields = new Field[FieldCount];
                for (int i = 0; i < FieldCount; i++)
                {
                    Fields[i] = new Field()
                    {
                        Hash = reader.ReadUInt32(),
                        Offset = reader.ReadUInt32(),
                    };
                }

                // Determine types and size for each field
                for (int fieldId = 0; fieldId < Fields.Length; fieldId++)
                {
                    var currentField = Fields[fieldId];
                    //MessageBox.Show($"Field: {currentField.GetTranslatedNameOrHash()} | Offset: {currentField.Offset}");
                    currentField.Size = EntrySize - currentField.Offset;

                    if (fieldId < Fields.Length - 1)
                        currentField.Size = Fields[fieldId + 1].Offset - currentField.Offset;

                    BcsvDataType type = BcsvDataType.String;
                    switch (currentField.Size)
                    {
                        case 1:
                            {
                                type = BcsvDataType.U8;
                                var translatedName = Fields[fieldId].GetTranslatedNameOrNull();

                                if (translatedName != null && translatedName.EndsWith(" s8"))
                                    type = BcsvDataType.S8;
                            }
                            break;

                        case 2:
                            type = BcsvDataType.UInt16;
                            break;

                        case 4:
                            {
                                type = BcsvDataType.UInt32;
                                var translatedName = Fields[fieldId].GetTranslatedNameOrNull();
                                if (translatedName != null)
                                {
                                    if (translatedName.EndsWith(".hshCstringRef"))
                                        type = BcsvDataType.HashedCsc32;

                                    else if (translatedName.EndsWith(" s32"))
                                        type = BcsvDataType.Int32;

                                    else if (translatedName.EndsWith(" f32"))
                                        type = BcsvDataType.Float32;

                                    // Otherwise will still be an int
                                    // Note: Theoretically it can also be a normal string, but I never saw a string4 yet so I assume it doesn't exist
                                }
                            }
                            break;
                    }

                    var translation = currentField.GetTranslatedNameOrNull();
                    if (translation != null)
                    {
                        if (translation.EndsWith(" u8") && currentField.Size > 1)
                            type = BcsvDataType.MultipleU8;
                    } else
                    {
                        if (KnownHashValueManager.KnownTypes.ContainsKey(currentField.HEX))
                            type = KnownHashValueManager.KnownTypes[currentField.HEX];
                        else if (type == BcsvDataType.String && currentField.Size <= 6)
                            type = BcsvDataType.MultipleU8;
                    }

                    currentField.DataType = type;
                }

                //// Get Type for each field
                Entries = new List<BCSVEntry>();
                for (int i = 0; i < entryCount; i++)
                {
                    BCSVEntry entry = new BCSVEntry();
                    Entries.Add(entry);

                    //long pos = reader.Position;
                    var entryPosition = reader.ReadUInt32();

                    for (int fieldId = 0; fieldId < Fields.Length; fieldId++)
                    {
                        var currentField = Fields[fieldId];

                        // Why is this needed? Are we missing something?
                        // Edit: Theorically this is useless? Seems to always go to the exact same position... but who knows...
                        // Seems after the "MultipleU8" implementation this isn't really needed cause fields should always have the correct size, but it's better to keep just to make sure
                        reader.SeekBegin(entryPosition + currentField.Offset);

                        object value = 0;
                        switch (currentField.DataType)
                        {
                            case BcsvDataType.MultipleU8:
                                value = reader.ReadBytes((int)currentField.Size);
                                break;

                            case BcsvDataType.S8:
                                value = reader.ReadSByte();
                                break;

                            case BcsvDataType.U8:
                                value = reader.ReadByte();
                                break;

                            case BcsvDataType.Float32:
                                value = reader.ReadSingle();
                                break;

                            case BcsvDataType.UInt16:
                                value = reader.ReadInt16();
                                break;

                            case BcsvDataType.Int32:
                                value = reader.ReadInt32();
                                break;

                            case BcsvDataType.UInt32:
                            case BcsvDataType.HashedCsc32:
                            case BcsvDataType.Murmur3:
                                value = reader.ReadUInt32();
                                break;

                            case BcsvDataType.String:
                                value = reader.ReadString((int)currentField.Size, Encoding.UTF8);
                                break;
                        }

                        entry.Add(currentField.HEX, value);
                    }

                    // Go to next entry
                    // Same from above applies here, seems we don't need to seek every time but it's better to prevent issues
                    reader.SeekBegin(entryPosition + EntrySize);
                }

                fileStream.Close();
            }

            
        }

        public void SaveAs(string filePath)
        {
            var fileStream = File.OpenWrite(filePath);

            using (var writer = new BinaryWriter(fileStream))
            {

                writer.Write(Entries.Count);
                writer.Write(EntrySize);
                writer.Write((ushort)Fields.Length);

                writer.Write(version);
                writer.Write(Encoding.UTF8.GetBytes(fileType));
                writer.Write(unk1);
                writer.Write(unk2);
                writer.Write(unk3);

                foreach (var field in Fields)
                {
                    writer.Write(field.Hash);
                    writer.Write(field.Offset);
                }

                foreach (var entry in Entries)
                {

                    var pos = (int)writer.BaseStream.Position;
                    writer.Write(pos);

                    foreach (var field in Fields)
                    {
                        writer.Seek(pos + (int)field.Offset, SeekOrigin.Begin);

                        var entryValue = entry[field.HEX];
                        switch (field.DataType)
                        {
                            case BcsvDataType.MultipleU8:
                                writer.Write((byte[])entryValue);
                                break;

                            case BcsvDataType.S8:
                                writer.Write((sbyte)entryValue);
                                break;

                            case BcsvDataType.U8:
                                writer.Write((byte)entryValue);
                                break;

                            case BcsvDataType.Float32:
                                writer.Write((float)entryValue);
                                break;

                            case BcsvDataType.UInt16:
                                writer.Write((short)entryValue);
                                break;

                            case BcsvDataType.Int32:
                                writer.Write((int)entryValue);
                                break;

                            case BcsvDataType.UInt32:
                            case BcsvDataType.HashedCsc32:
                            case BcsvDataType.Murmur3:
                                writer.Write((uint)entryValue);
                                break;


                            case BcsvDataType.String:
                                {
                                    try
                                    {
                                        string stringValue = entryValue.ToString().Trim();

                                        var bytes = Encoding.UTF8.GetBytes(stringValue);
                                        Array.Resize(ref bytes, (int)field.Size);
                                        writer.Write(bytes);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Failed to save string\n" + ex.ToString());
                                    }
                                }
                                break;
                        }

                    }

                    writer.Seek(pos + (int)EntrySize, SeekOrigin.Begin);
                }
            }

            fileStream.Close();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Fields = null;
                    Entries = null;
                }

                // Indicate that the instance has been disposed.
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}