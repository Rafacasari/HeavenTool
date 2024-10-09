using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeavenTool.Utility.FileTypes.RSTB;

public static class RomFsNameManager
{
    //private static Dictionary<string, uint> loadedNames;
    private static Dictionary<uint, string> uniqueHashes;
    //private static List<string> nonUniqueHashes;

    public static Dictionary<uint, string> UniqueNames
    {
        get
        {
            if (!isInitialized)
                Initialize();

            return uniqueHashes;
        }
    }

    //// TODO: This may be useless?
    //public static List<string> NamesWithDuplicatedHash
    //{
    //    get
    //    {
    //        if (!isInitialized)
    //            Initialize();

    //        return nonUniqueHashes;
    //    }
    //}

    private const string FILE_LOCATION = "extra/romfs-files.txt";

    private static bool isInitialized;

    public static void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        //loadedNames = new Dictionary<string, uint>();

        // Create directory if don't exist
        Directory.CreateDirectory("extra");

        if (File.Exists(FILE_LOCATION))
        {
            var lines = File.ReadAllLines(FILE_LOCATION);

            uniqueHashes = [];
            //nonUniqueHashes = [];

            foreach (var line in lines)
            {
                var hash = line.ToCRC32();

                if (!uniqueHashes.TryGetValue(hash, out string value))
                    uniqueHashes.Add(hash, line);
                else
                {
                    if (value != "")
                    {
                        //nonUniqueHashes.Add(value);
                        uniqueHashes[hash] = "";
                    }

                    //nonUniqueHashes.Add(line);
                }
            }

        }
        else
        {
            // Create file if it doesn't exist
            File.WriteAllText(FILE_LOCATION, "");
        }
    }

    public static void Update(string[] names)
    {
        Directory.CreateDirectory("extra");

        File.WriteAllLines(FILE_LOCATION, names);

        // Reiniatialize so variables can be updated
        isInitialized = false;
        Initialize();
    }

    public static string GetValue(uint hash)
    {
        if (!isInitialized) Initialize(); 

        var myString = UniqueNames.TryGetValue(hash, out string value) ? value : null;

        if (string.IsNullOrEmpty(myString)) myString = null;

        return myString ?? $"0x{hash:x}";   
    }

    internal static void Add(string fileName)
    {
        //var hash = fileName.ToCRC32();

        //if (!uniqueHashes.TryGetValue(hash, out string value))
        //    uniqueHashes.Add(hash, fileName);
        //else if (value != "")
        //    uniqueHashes[hash] = "";

        //saveNeeded = true;

        var lines = File.ReadAllLines(FILE_LOCATION);
        if (!lines.Contains(fileName))
        {
            lines = [.. lines, fileName];
            File.WriteAllLines(FILE_LOCATION, lines);
        }
        
    }
}
