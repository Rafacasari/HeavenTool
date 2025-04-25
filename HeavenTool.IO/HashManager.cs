using HeavenTool.IO.FileFormats.BCSV;
using System.Collections;
using System.Globalization;

namespace HeavenTool.IO;

public static class HashManager
{
    private static readonly string KNOWN_TYPES_PATH = Path.Combine(AppContext.BaseDirectory, "extra", "known-types.txt");

    public static readonly Dictionary<string, DataType> KnownTypes = [];
    public static readonly Dictionary<uint, string> CRC32_Hashes = new()
    {
        { 0, "" }
    };
    public static readonly Dictionary<uint, string> MMH3_Hashes = [];

    public static readonly Dictionary<uint, List<CRC32_Entry>> EnumListCRC32 = [];
    public static readonly Dictionary<uint, List<MMH3_Entry>> EnumListMMH3 = [];

    private static bool isInitialized;
    public static void InitializeHashes()
    {
        if (isInitialized) return;

        Console.WriteLine("[BCSV] Initializing Hashes...");
        isInitialized = true;

        string crcHashes = Path.Combine(AppContext.BaseDirectory, "extra", "hashes");
        string murmurHashes = Path.Combine(AppContext.BaseDirectory, "extra", "murmur3-hashes");

        // Create directory if they don't exist
        Directory.CreateDirectory(crcHashes);
        Directory.CreateDirectory(murmurHashes);

        foreach (var file in Directory.GetFiles(crcHashes, "*.txt"))
        {
            if (Path.GetExtension(file) != ".txt") continue;

            foreach (string hashStr in File.ReadAllLines(file))
                CRC32_Hashes.TryAdd(hashStr.ToCRC32(), hashStr);

        }

        Console.WriteLine("[BCSV] Loaded {0} CRC32 Hashes", CRC32_Hashes.Count);

        foreach (var file in Directory.GetFiles(murmurHashes, "*.txt"))
        {
            if (Path.GetExtension(file) != ".txt")
                continue;

            foreach (string hashStr in File.ReadAllLines(file))
                MMH3_Hashes.TryAdd(hashStr.ToMurmur(), hashStr);

        }

        Console.WriteLine("[BCSV] Loaded {0} Murmur3 Hashes", MMH3_Hashes.Count);

        LoadEnumHashes();
        LoadKnownTypes();
        
    }

    private static void LoadKnownTypes()
    {
        if (File.Exists(KNOWN_TYPES_PATH))
        {
            var lines = File.ReadAllLines(KNOWN_TYPES_PATH);

            foreach (var line in lines)
            {
                var split = line.Split('=');

                if (split.Length == 2 && Enum.TryParse(split[1], out DataType type))
                    KnownTypes.TryAdd(split[0], type);
            }
        }

        Console.WriteLine("[BCSV] Loaded {0} types", KnownTypes.Count);
    }

    private static void LoadEnumHashes()
    {
        // TODO: Make a single file with all enums, maybe a json?
        // We gonna need these hashes separated as enums to properly display an EnumSelector in the BCSV Editor
        string enumFolder = Path.Combine(AppContext.BaseDirectory, "extra/hashes/enum");
        Directory.CreateDirectory(enumFolder);

        foreach (var file in Directory.GetFiles(enumFolder, "*.txt", SearchOption.AllDirectories))
        {
            if (Path.GetExtension(file) != ".txt")
                continue;

            var fileName = Path.GetFileNameWithoutExtension(file);
            if (fileName.StartsWith("0x"))
                fileName = fileName[2..];

            bool parsedSuccessfully = uint.TryParse(fileName, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint enumHash);
            if (parsedSuccessfully)
            {
                if (!EnumListCRC32.ContainsKey(enumHash))
                    EnumListCRC32.Add(enumHash, []);

                var collection = EnumListCRC32[enumHash];
                foreach (string hashStr in File.ReadAllLines(file))
                {
                    uint hash = hashStr.ToCRC32();

                    if (!collection.Any(x => x.Value == hash))
                        collection.Add(new CRC32_Entry(hash));

                    CRC32_Hashes.TryAdd(hash, hashStr);
                }
            }
        }
    }

    public static void AddOrEditForcedType(string key, DataType value)
    {
        if (!KnownTypes.TryAdd(key, value))
            KnownTypes[key] = value;

        File.WriteAllLines(KNOWN_TYPES_PATH, KnownTypes.Select(x => {
            return $"{x.Key}={x.Value}";
        }));
    }
}

public class CRC32_Entry(uint val)
{
    public uint Value { get; private set; } = val;

    public override string ToString()
    {
        return HashManager.CRC32_Hashes.TryGetValue(Value, out string value) ? value : Value.ToString("x");
    }
}

public class MMH3_Entry(uint val)
{
    public uint Value { get; private set; } = val;

    public override string ToString()
    {
        return HashManager.MMH3_Hashes.TryGetValue(Value, out string value) ? value : Value.ToString("x");
    }
}