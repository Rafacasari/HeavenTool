//using HeavenTool.IO;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;

//namespace HeavenTool.Utility.FileTypes.BCSV;

//public static class BCSVHashing
//{
//    public static readonly Dictionary<uint, string> CRCHashes = [];
//    public static readonly Dictionary<uint, string> MurmurHashes = [];
//    public static readonly Dictionary<uint, List<BCSV_CRC32Value>> EnumHashes = [];

//    private static bool isInitialized;
//    public static void InitializeHashes()
//    {
//        if (isInitialized) return;

//        isInitialized = true;

//        string crcHashes = Path.Combine(AppContext.BaseDirectory, "extra", "hashes");
//        string murmurHashes = Path.Combine(AppContext.BaseDirectory, "extra", "murmur3-hashes");

//        // Create directory if they don't exist
//        Directory.CreateDirectory(crcHashes);
//        Directory.CreateDirectory(murmurHashes);

//        foreach (var file in Directory.GetFiles(crcHashes))
//        {
//            if (Path.GetExtension(file) != ".txt")
//                continue;

//            foreach (string hashStr in File.ReadAllLines(file))
//                CRCHashes.TryAdd(hashStr.ToCRC32(), hashStr);

//        }

//        foreach (var file in Directory.GetFiles(murmurHashes))
//        {
//            if (Path.GetExtension(file) != ".txt")
//                continue;

//            foreach (string hashStr in File.ReadAllLines(file))
//                MurmurHashes.TryAdd(hashStr.ToMurmur(), hashStr);

//        }

//        LoadEnumHashes();
//    }

//    private static void LoadEnumHashes()
//    {
//        string enumFolder = Path.Combine(AppContext.BaseDirectory, "extra/hashes/enum");
//        Directory.CreateDirectory(enumFolder);

//        foreach (var file in Directory.GetFiles(enumFolder, "*.txt", SearchOption.AllDirectories))
//        {
//            if (Path.GetExtension(file) != ".txt")
//                continue;

//            var fileName = Path.GetFileNameWithoutExtension(file);
//            if (fileName.StartsWith("0x"))
//                fileName = fileName[2..];

//            bool parsedSuccessfully = uint.TryParse(fileName, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint enumHash);
//            if (parsedSuccessfully)
//            {
//                if (!EnumHashes.ContainsKey(enumHash))
//                    EnumHashes.Add(enumHash, []);

//                var collection = EnumHashes[enumHash];
//                foreach (string hashStr in File.ReadAllLines(file))
//                {
//                    uint hash = hashStr.ToCRC32();

//                    if (!collection.Any(x => x.Value == hash))
//                        collection.Add(new BCSV_CRC32Value(hash));

//                    CRCHashes.TryAdd(hash, hashStr);
//                }
//            }
//        }
//    }
//}

//public class BCSV_CRC32Value(uint val)
//{
//    public uint Value { get; private set; } = val;

//    public override string ToString()
//    {
//        return BCSVHashing.CRCHashes.TryGetValue(Value, out string value) ? value : Value.ToString("x");
//    }
//}
