using HeavenTool.IO;
using HeavenTool.IO.FileFormats.BCSV;

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

            string[] uniqueFields = [
                "UniqueID u16", // Most files use this

                // Specific files
                "Label string8", // NmlNpcRaceParam.bcsv
                "ItemUniqueID u16", // WherearenItemKind.bcsv
                "StateName string48", // PlayerStateParam.bcsv
                "NumberingId u16", // AmiiboData.bcsv
                "ResourceName string33", // TVProgram.bcsv
                "StageName string32", // FieldCreateParam.bcsv
                "ItemFrom string32", // ShopItemRouteFlags.bcsv
                "TVProgramName.hshCstringRef", // TVProgramMonday.bcsv
                "GroundAttributeUniqueID u16", // FieldLandMakingRoadKindParam.bcsv
                "NpcRoleSetID u16", // WherearenRollSet.bcsv
                // Don't have anything we can do, we gonna need to match entire row (which can cause issues when editing/removing rows; addition rows should work fine)
                // FishAppearRiverParam.bcsv,
                // NpcLife.bcsv,
                // SeafoodAppearParam.bcsv,
                // FishAppearSeaParam.bcsv,
                // FgFlowerHeredity.bcsv,
                // NpcCastLabelData.bcsv,
                // NpcInterest.bcsv,
                // NpcCastScheduleData.bcsv,
                // SeasonCalendar.bcsv,
                // FieldLandMakingActionParam.bcsv,
                // NpcMoveRoomTemplate.bcsv
            ];

            uint[] uniqueHashes = [
                0x37571146, // MessageCardSelectDesign.bcsv, MessageCardSelectDesignSp.bcsv, MessageCardSelectPresent.bcsv and MessageCardSelectPresentSp.bcsv
            ];

            List<uint> hashes = new(uniqueFields.Select(x => x.ToCRC32()));
            hashes.AddRange(uniqueHashes);

            UniqueHeader = hashes.FirstOrNullStruct(x => LoadedFile.Fields.Any(field => field.Hash == x));
            UniqueHeaderIndex = UniqueHeader.HasValue ? Array.FindIndex(LoadedFile.Fields, x => x.Hash == UniqueHeader) : -1;

            if (UniqueHeaderIndex == -1) 
                return;

            for (int entryIndex = 0; entryIndex < LoadedFile.Length; entryIndex++)
            {
                object uniqueValue = null;
                var values = new object[LoadedFile.Fields.Length];
                for (int fieldIndex = 0; fieldIndex < LoadedFile.Fields.Length; fieldIndex++)
                {

                    var entry = LoadedFile[entryIndex, fieldIndex];

                    if (fieldIndex == UniqueHeaderIndex && entry is object uniqueVal)
                        uniqueValue = uniqueVal;
                    values[fieldIndex] = entry;
                }

                if (uniqueValue != null)
                    Entries.TryAdd(uniqueValue, values);
            }
        }
    }

    public Dictionary<object, object[]> Entries = [];

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


        foreach (var (uniqueId, _) in Entries)
            if (!otherBCSV.Entries.ContainsKey(uniqueId) && !Removes.Contains(uniqueId))
                Removes.Add(uniqueId);
        

        foreach (var (uniqueId, values) in otherBCSV.Entries) 
        {
            if (Entries.TryGetValue(uniqueId, out var currentValues) && !values.SequenceEqual(currentValues))
            {
                if (Changes.ContainsKey(uniqueId))
                    ConsoleUtilities.WriteLine($"[BCSV] Conflict in file {Name} (UniqueHeader: {UniqueHeader:x} | Value {uniqueId})", ConsoleColor.Red);
                Changes[uniqueId] = currentValues;
            }
            else if (!Entries.ContainsKey(uniqueId))
            {
                if (Additions.ContainsKey(uniqueId))
                    ConsoleUtilities.WriteLine($"[BCSV] Conflict! Two mods tried to add a entry with same UniqueId in {Name} (UniqueHeader: {UniqueHeader:x} | Value {uniqueId})", ConsoleColor.Red);
                Additions[uniqueId] = values;
            }
        }
    }

    public override byte[] SaveFile()
    {
        if (LoadedFile == null) 
            return null;

        BakeFile();

        return LoadedFile.Save();
    }

    private void BakeFile() 
    {

        Removes.RemoveAll(Changes.ContainsKey);

        if (UniqueHeaderIndex != -1 && Changes.Count > 0)
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

        // TODO: remake how the BCSV file works, cause using multidimension array [,] is a pain to make a "delete" 
    }
}
