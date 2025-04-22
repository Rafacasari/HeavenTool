using HeavenTool.IO.FileFormats.BCSV;
using System.Globalization;

namespace HeavenTool.IO;

public static class HashManager
{
    public static readonly Dictionary<string, DataType> KnownTypes = [];
    public static readonly Dictionary<uint, string> CRCHashes = [];
    public static readonly Dictionary<uint, string> MurmurHashes = [];
    public static readonly Dictionary<uint, List<BCSV_CRC32Value>> EnumHashes = [];

    private static bool isInitialized;
    public static void InitializeHashes()
    {
        if (isInitialized) return;

        Console.WriteLine("[BCSV] Initializing Hashes...");
        isInitialized = true;

        string crcHashes = Path.Combine("extra", "hashes");
        string murmurHashes = Path.Combine("extra", "murmur3-hashes");

        // Create directory if they don't exist
        Directory.CreateDirectory(crcHashes);
        Directory.CreateDirectory(murmurHashes);

        foreach (var file in Directory.GetFiles(crcHashes))
        {
            if (Path.GetExtension(file) != ".txt")
                continue;

            foreach (string hashStr in File.ReadAllLines(file))
                CRCHashes.TryAdd(hashStr.ToCRC32(), hashStr);

        }

        Console.WriteLine("[BCSV] Loaded {0} CRC32 Hashes", CRCHashes.Count);

        foreach (var file in Directory.GetFiles(murmurHashes))
        {
            if (Path.GetExtension(file) != ".txt")
                continue;

            foreach (string hashStr in File.ReadAllLines(file))
                MurmurHashes.TryAdd(hashStr.ToMurmur(), hashStr);

        }

        Console.WriteLine("[BCSV] Loaded {0} Murmur3 Hashes", MurmurHashes.Count);

        LoadEnumHashes();
        LoadKnownTypes();
    }

    private static void LoadKnownTypes()
    {
        string knownTypes = Path.Combine(AppContext.BaseDirectory, "extra", "known-types.txt");

        if (File.Exists(knownTypes))
        {
            var lines = File.ReadAllLines(knownTypes);

            foreach (var line in lines)
            {
                var split = line.Split('=');
                if (split.Length > 1 && split[0].Length > 0 && Enum.TryParse(split[1], out DataType type))
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
                if (!EnumHashes.ContainsKey(enumHash))
                    EnumHashes.Add(enumHash, []);

                var collection = EnumHashes[enumHash];
                foreach (string hashStr in File.ReadAllLines(file))
                {
                    uint hash = hashStr.ToCRC32();

                    if (!collection.Any(x => x.Value == hash))
                        collection.Add(new BCSV_CRC32Value(hash));

                    CRCHashes.TryAdd(hash, hashStr);
                }
            }
        }
    }
}

public class BCSV_CRC32Value(uint val)
{
    public uint Value { get; private set; } = val;

    public override string ToString()
    {
        return HashManager.CRCHashes.TryGetValue(Value, out string value) ? value : Value.ToString("x");
    }
}
