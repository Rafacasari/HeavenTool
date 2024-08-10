using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HeavenTool.BCSV
{
    public class BCSVFile: IDisposable
    {
        //private uint EntriesCount { get; set; }
        uint EntrySize { get; set; }
        ushort FieldCount { get; set; }

        public Field[] Fields { get; set; }
        public List<DataEntry> Entries { get; set; }

        private ushort version;
        private string fileType;
        private uint unk1;
        private uint unk2;
        private uint unk3;

        public BCSVFile(string filePath)
        {
            var fileStream = File.OpenRead(filePath);

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

                    DataType type = DataType.String;
                    switch (currentField.Size)
                    {
                        case 1:
                            {
                                type = DataType.U8;
                                var translatedName = Fields[fieldId].GetTranslatedNameOrNull();

                                if (translatedName != null && translatedName.EndsWith(" s8"))
                                    type = DataType.S8;
                            }
                            break;

                        case 2:
                            type = DataType.UInt16;
                            break;

                        case 4:
                            {
                                type = DataType.UInt32;
                                var translatedName = Fields[fieldId].GetTranslatedNameOrNull();
                                if (translatedName != null)
                                {
                                    if (translatedName.EndsWith(".hshCstringRef"))
                                        type = DataType.HashedCsc32;

                                    else if (translatedName.EndsWith(" s32"))
                                        type = DataType.Int32;

                                    else if (translatedName.EndsWith(" f32"))
                                        type = DataType.Float32;

                                    // Otherwise will still be an int
                                    // Note: Theoretically it can also be a normal string, but I never saw a string4 yet so I assume it doesn't exist
                                }
                                else if (KnownHashValueManager.KnownTypes.ContainsKey(currentField.HashedName))
                                    type = KnownHashValueManager.KnownTypes[currentField.HashedName];
                            }
                            break;
                    }

                    var translation = currentField.GetTranslatedNameOrNull();
                    if (translation != null)
                    {
                        if (translation.EndsWith(" u8") && currentField.Size > 1)
                            type = DataType.MultipleU8;
                    }

                    currentField.DataType = type;
                }

               
                //// Get Type for each field
                Entries = new List<DataEntry>();
                for (int i = 0; i < entryCount; i++)
                {
                    DataEntry entry = new DataEntry();
                    Entries.Add(entry);

                    //long pos = reader.Position;
                    var entryPosition = reader.ReadUInt32();


                    for (int fieldId = 0; fieldId < Fields.Length; fieldId++)
                    {
                        var currentField = Fields[fieldId];

                        // TODO: Why is this needed? Are we missing something?
                        reader.SeekBegin(entryPosition + currentField.Offset);

                        object value = 0;

                        // Read values based on Header type
                        switch (currentField.DataType)
                        {
                            case DataType.MultipleU8:
                                {
                                   value = reader.ReadBytes((int) currentField.Size);
                                }
                                break;

                            case DataType.S8:
                                value = reader.ReadSByte();
                                break;

                            case DataType.U8:
                                value = reader.ReadByte();
                                break;

                            case DataType.Float32:
                                value = reader.ReadSingle();
                                break;

                            case DataType.UInt16:
                                value = reader.ReadInt16();
                                break;

                            case DataType.Int32:
                                value = reader.ReadInt32();
                                break;

                            case DataType.UInt32:
                                value = reader.ReadUInt32();
                                break;

                            case DataType.HashedCsc32:
                                value = reader.ReadUInt32();
                                break;


                            case DataType.String:
                                value = reader.ReadString((int) currentField.Size, Encoding.UTF8);
                                break;
                        }
                        entry.Fields.Add(currentField.HashedName, value);
                    }

                    // Go to next entry
                    reader.SeekBegin(entryPosition + EntrySize);
                }
            }

            fileStream.Close();
        }

        public void SaveAs(string filePath)
        {
            var fileStream = File.OpenWrite(filePath);

            using (var writer = new BinaryWriter(fileStream)) {

                writer.Write(Entries.Count); 
                writer.Write(EntrySize);    
                writer.Write((ushort) Fields.Length); 

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

                foreach (var entry in Entries) {

                    var pos = (int) writer.BaseStream.Position;
                    writer.Write(pos);

                    foreach (var field in Fields) 
                    {
                        writer.Seek(pos + (int) field.Offset, SeekOrigin.Begin);

                        var entryValue = entry.Fields[field.HashedName];
                        switch (field.DataType)
                        {
                            case DataType.MultipleU8:
                                writer.Write((byte[]) entryValue);
                                break;

                            case DataType.S8:
                                writer.Write((sbyte)entryValue);
                                break;

                            case DataType.U8:
                                writer.Write((byte) entryValue);
                                break;

                            case DataType.Float32:
                                writer.Write((float) entryValue);
                                break;

                            case DataType.UInt16:
                                writer.Write((short)entryValue);
                                break;

                            case DataType.Int32:
                                writer.Write((int) entryValue);
                                break;

                            case DataType.UInt32:
                            case DataType.HashedCsc32:
                                writer.Write((uint) entryValue);
                                break;


                            case DataType.String:
                                {
                                    string stringValue = entryValue.ToString().Trim();

                                    var bytes = Encoding.UTF8.GetBytes(stringValue);
                                    Array.Resize(ref bytes, (int) field.Size);
                                    writer.Write(bytes);
                                }
                                break;
                        }

                    }

                    writer.Seek(pos + (int) EntrySize, SeekOrigin.Begin);
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