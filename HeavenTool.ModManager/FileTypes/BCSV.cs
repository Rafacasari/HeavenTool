using HeavenTool.IO;
using HeavenTool.IO.FileFormats.BCSV;
using System.Xml;

namespace HeavenTool.ModManager.FileTypes;

public sealed class BCSV : ModFile
{
    public BinaryCSV LoadedFile { get; set; }
    public uint? UniqueHeader {  get; set; } = null;
    public int UniqueHeaderIndex { get; set; }

    public BCSV(Stream stream, string name) : base(stream, name)
    {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentException.ThrowIfNullOrEmpty(name);

        if (stream is MemoryStream memoryStream)
        {
            var bytes = memoryStream.ToArray();

            LoadedFile = new BinaryCSV(bytes);
            if (LoadedFile.Length == 0 || LoadedFile.Fields.Length == 0) return;

            UniqueHeader = BinaryCSV.UniqueHashes.FirstOrNullStruct(x => LoadedFile.Fields.Any(field => field.Hash == x));
            UniqueHeaderIndex = UniqueHeader.HasValue ? Array.FindIndex(LoadedFile.Fields, x => x.Hash == UniqueHeader) : -1;

            //if (UniqueHeaderIndex == -1) return;

            //for (int entryIndex = 0; entryIndex < LoadedFile.Length; entryIndex++)
            //{
            //    object uniqueValue = null;
            //    var values = new object[LoadedFile.Fields.Length];
            //    // store all entry values into a dictionary with the unique value as key

            //    for (int fieldIndex = 0; fieldIndex < LoadedFile.Fields.Length; fieldIndex++)
            //    {
            //        var entry = LoadedFile[entryIndex, fieldIndex];

            //        if (fieldIndex == UniqueHeaderIndex && entry is object uniqueVal)
            //            uniqueValue = uniqueVal;
            //        values[fieldIndex] = entry;
            //    }

            //    if (uniqueValue != null)
            //    {
            //        if (Entries.ContainsKey(uniqueValue))
            //            ConsoleUtilities.WriteLine($"[BCSV] File {name} have entries with the same unique header, this can result in broken mods!", ConsoleColor.Red);

            //        Entries.TryAdd(uniqueValue, values);
            //    }
            //}
        }
    }

    //public Dictionary<object, object[]> Entries = [];

    internal Dictionary<object, object[]> Changes = [];
    internal Dictionary<object, object[]> Additions = [];
    internal List<object> Removes = [];

    public override void DoDiff(ModFile otherFile)
    {
        if (otherFile == null) return;

        if (otherFile is not BCSV otherBCSV)
            throw new ArgumentException("File type mismatch", nameof(otherFile));

        if (LoadedFile == null ||  otherBCSV.LoadedFile == null || 
            Name != otherBCSV.Name ||
            LoadedFile.Fields.SequenceEqual(otherBCSV.LoadedFile.Fields)) return;

        if (UniqueHeaderIndex == -1)
        {
            ConsoleUtilities.WriteLine($"[BCSV] File {Name} does not support merging. This file doesn't contain a Unique Header.", ConsoleColor.Red);
            return;
        }

        try
        {
            var fileEntries = LoadedFile.Entries.ToDictionary(x => x[UniqueHeaderIndex], y => y);
            var otherFileEntries = otherBCSV.LoadedFile.Entries.ToDictionary(x => x[UniqueHeaderIndex], y => y);

            foreach (var (uniqueId, _) in fileEntries)
                if (!otherFileEntries.ContainsKey(uniqueId) && !Removes.Contains(uniqueId))
                    Removes.Add(uniqueId);

            foreach (var (uniqueId, values) in otherFileEntries)
            {
                if (!fileEntries.ContainsKey(uniqueId))
                {
                    if (Additions.ContainsKey(uniqueId))
                        ConsoleUtilities.WriteLine($"[BCSV] Conflict! Two mods tried to add a entry with same Unique Key in {Name} (UniqueHeader: {UniqueHeader:x} | Value {uniqueId})", ConsoleColor.Red);
                    Additions[uniqueId] = values;
                }
                else if (fileEntries.TryGetValue(uniqueId, out var currentValues) && !values.SequenceEqual(currentValues))
                {
                    if (Changes.ContainsKey(uniqueId))
                        ConsoleUtilities.WriteLine($"[BCSV] Conflict in file {Name} (UniqueHeader: {UniqueHeader:x} | Value {uniqueId})", ConsoleColor.Yellow);
                    Changes[uniqueId] = currentValues;
                }
            }
        }
        catch (Exception ex) 
        {
            ConsoleUtilities.WriteLine($"[BCSV] Failed to merge {Name} (probably there is one or more entries using the same unique-key)\n{ex}\n", ConsoleColor.Red);
        }
    }

    public override byte[] SaveFile()
    {
        if (LoadedFile == null)  return null;

        BakeFile();

        return LoadedFile.Save();
    }

    private void BakeFile() 
    {
        if (UniqueHeaderIndex == -1) return;
        

        Removes.RemoveAll(Changes.ContainsKey);

        if (Changes.Count > 0)
        {
            for (int entryIndex = 0; entryIndex < LoadedFile.Length; entryIndex++)
            {
                if (LoadedFile[entryIndex, UniqueHeaderIndex] is object value && Changes.TryGetValue(value, out var values))
                {
                    if (values.Length != LoadedFile.Fields.Length) continue;

                    for (int i = 0; i < values.Length; i++)
                        LoadedFile[entryIndex, i] = values[i];
                    
                }
            }
        }

        foreach (var (uniqueId, values) in Additions)
        {
            var entryId = LoadedFile.Length + 1;

            for (var i = 0; i < values.Length; i++)
                LoadedFile[entryId, i] = values[i];
        }

        LoadedFile.Entries.RemoveAll(entryValues =>
        {
            var uniqueValue = entryValues[UniqueHeaderIndex];

            return Removes.Contains(uniqueValue);
        });
    }
}
