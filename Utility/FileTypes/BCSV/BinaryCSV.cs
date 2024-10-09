using HeavenTool.Utility.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeavenTool.Utility.FileTypes.BCSV;

// Thanks to https://nintendo-formats.com/games/acnh/bcsv.html
// The information about HasExtendedHeader and padding helped a lot =D

public class BinaryCSV : IDisposable
{
    /// <summary>
    /// The size of each entry
    /// </summary>
    internal uint EntrySize { get; set; }

    /// <summary>
    /// Table of <seealso cref="Field"/> (columns)
    /// </summary>
    public Field[] Fields { get; set; }

    /// <summary>
    /// List of <see cref="BCSVEntry"/>
    /// </summary>
    public List<BCSVEntry> Entries { get; set; }

    /// <summary>
    /// Cache to get a <see cref="Field"/> using a hash/hex, use <see cref="GetFieldByHashedName(string)"/>
    /// </summary>
    public Dictionary<string, Field> Cache { get; set; }

    /// <summary>
    /// Get a <see cref="Field"/> using a hash/hex (<seealso cref="Field.HEX"/>)
    /// </summary>
    /// <param name="hashedName"></param>
    /// <returns></returns>
    public Field GetFieldByHashedName(string hashedName)
    {
        Cache ??= [];

        if (Fields == null)
            return null;

        // Try to get a field from the cache (prevent long time)
        if (Cache.TryGetValue(hashedName, out Field value)) return value;

        // If the value is not found, 
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

    internal byte HasExtendedHeader {  get; private set; }
    internal byte UnknownField { get; private set; }

    internal ushort HeaderVersion { get; private set; }
    internal string HeaderMagic { get; private set; }


    public BinaryCSV(uint entrySize, Field[] fields, BCSVEntry[] entries, byte hasExtendedHeader, byte unknownField, string magic, ushort version)
    {
        EntrySize = entrySize;
        Fields = fields;
        Entries = [.. entries];

        HasExtendedHeader = hasExtendedHeader;
        UnknownField = unknownField;

        HeaderMagic = magic;
        HeaderVersion = version;
    }

    public static BinaryCSV CopyFileWithoutEntries(BinaryCSV fileToCopy) => new(fileToCopy.EntrySize, fileToCopy.Fields, [], fileToCopy.HasExtendedHeader, fileToCopy.UnknownField, fileToCopy.HeaderMagic, fileToCopy.HeaderVersion);


    public BinaryCSV(string filePath)
    {
        using var fileStream = File.OpenRead(filePath);
        using var reader = new BinaryFileReader(fileStream);

        var entryCount = reader.ReadUInt32();
        EntrySize = reader.ReadUInt32();
        var fieldCount = reader.ReadUInt16();

        HasExtendedHeader = reader.ReadByte();
        UnknownField = reader.ReadByte();

        if (HasExtendedHeader == 1)
        {
            HeaderMagic = reader.ReadString(4, Encoding.ASCII); // BCSV but inverted "VSCB"
            HeaderVersion = reader.ReadUInt16();

            // 10 byte padding
            reader.Position += 10;
        }

        // Read Fields
        Fields = new Field[fieldCount];
        for (int i = 0; i < fieldCount; i++)
        {
            Fields[i] = new Field()
            {
                Hash = reader.ReadUInt32(),
                Offset = reader.ReadUInt32(),
            };
        }

        for (int i = 0; i < Fields.Length; i++)
        {
            var currentField = Fields[i];

            // If it's not the last one is the next offset to calculate the current size, otherwise use EntrySize
            currentField.Size = (i < Fields.Length - 1 ? Fields[i + 1].Offset : EntrySize) - currentField.Offset;

            BCSVDataType type = BCSVDataType.String;
            switch (currentField.Size)
            {
                case 1:
                    {
                        type = BCSVDataType.U8;
                        var translatedName = Fields[i].GetTranslatedNameOrNull();

                        if (translatedName != null && translatedName.EndsWith(" s8"))
                            type = BCSVDataType.S8;
                    }
                    break;

                case 2:
                    {
                        var translatedName = Fields[i].GetTranslatedNameOrNull();
                        if (translatedName != null && translatedName.EndsWith(" s16"))
                            type = BCSVDataType.Int16;
                        else 
                            type = BCSVDataType.UInt16;
                    }
                    break;

                case 4:
                    {
                        type = BCSVDataType.UInt32;
                        var translatedName = Fields[i].GetTranslatedNameOrNull();
                        if (translatedName != null)
                        {
                            if (translatedName.EndsWith(".hshCstringRef"))
                                type = BCSVDataType.HashedCsc32;

                            else if (translatedName.EndsWith(" s32"))
                                type = BCSVDataType.Int32;

                            else if (translatedName.EndsWith(" f32"))
                                type = BCSVDataType.Float32;

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
                    type = BCSVDataType.MultipleU8;
            }
            else
            {
                if (KnownHashValueManager.KnownTypes.TryGetValue(currentField.HEX, out BCSVDataType value))
                    type = value;
                else if (type == BCSVDataType.String && currentField.Size <= 6)
                    type = BCSVDataType.MultipleU8;
            }

            currentField.DataType = type;
        }

        //// Get Type for each field
        Entries = [];
        for (int i = 0; i < entryCount; i++)
        {
            BCSVEntry entry = [];
            Entries.Add(entry);

            //long pos = reader.Position;
            var entryPosition = reader.ReadUInt32();

            for (int fieldId = 0; fieldId < Fields.Length; fieldId++)
            {
                var currentField = Fields[fieldId];

                // Seems after the "MultipleU8" implementation this isn't really needed cause fields should always have the correct size, but it's better to keep just to make sure
                reader.SeekBegin(entryPosition + currentField.Offset);

                object value = 0;
                switch (currentField.DataType)
                {
                    case BCSVDataType.MultipleU8:
                        value = reader.ReadBytes((int)currentField.Size);
                        break;

                    case BCSVDataType.S8:
                        value = reader.ReadSByte();
                        break;

                    case BCSVDataType.U8:
                        value = reader.ReadByte();
                        break;

                    case BCSVDataType.Float32:
                        value = reader.ReadSingle();
                        break;

                    case BCSVDataType.Int16:
                        value = reader.ReadInt16(); 
                        break;

                    case BCSVDataType.UInt16:
                        value = reader.ReadUInt16();
                        break;

                    case BCSVDataType.Int32:
                        value = reader.ReadInt32();
                        break;

                    case BCSVDataType.UInt32:
                    case BCSVDataType.HashedCsc32:
                    case BCSVDataType.Murmur3:
                        value = reader.ReadUInt32();
                        break;

                    case BCSVDataType.String:
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

    public void SaveAs(string filePath)
    {
        var fileStream = File.OpenWrite(filePath);

        using (var writer = new BinaryWriter(fileStream))
        {

            writer.Write(Entries.Count);
            writer.Write(EntrySize);
            writer.Write((ushort)Fields.Length);

            writer.Write(HasExtendedHeader);
            writer.Write(UnknownField);

            if (HasExtendedHeader == 1)
            {
                writer.Write(Encoding.UTF8.GetBytes(HeaderMagic));
                writer.Write(HeaderVersion);

                // Padding
                writer.Seek(10, SeekOrigin.Current);
            }


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
                        case BCSVDataType.MultipleU8:
                            writer.Write((byte[])entryValue);
                            break;

                        case BCSVDataType.S8:
                            writer.Write((sbyte)entryValue);
                            break;

                        case BCSVDataType.U8:
                            writer.Write((byte)entryValue);
                            break;

                        case BCSVDataType.Float32:
                            writer.Write((float)entryValue);
                            break;

                        case BCSVDataType.Int16:
                            writer.Write((short)entryValue);
                            break;

                        case BCSVDataType.UInt16:
                            writer.Write((ushort)entryValue);
                            break;

                        case BCSVDataType.Int32:
                            writer.Write((int)entryValue);
                            break;

                        case BCSVDataType.UInt32:
                        case BCSVDataType.HashedCsc32:
                        case BCSVDataType.Murmur3:
                            writer.Write((uint)entryValue);
                            break;


                        case BCSVDataType.String:
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
                                    MessageBox.Show($"Failed to save string\n{ex}");
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