using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeavenTool.Utility.FileTypes.RSTB;

public static class RomFsNameManager
{
    private static Dictionary<uint, string> uniqueHashes;

    public static Dictionary<uint, string> UniqueNames
    {
        get
        {
            if (!isInitialized)
                Initialize();

            return uniqueHashes;
        }
    }


    private static bool isInitialized;

    public static void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        //loadedNames = new Dictionary<string, uint>();

        string extra = Path.Combine(AppContext.BaseDirectory, "extra");
        string fileLocation = Path.Combine(extra, "romfs-files.txt");

        // Create directory if don't exist]
        Directory.CreateDirectory(extra);

        if (File.Exists(fileLocation))
        {
            var lines = File.ReadAllLines(fileLocation);

            uniqueHashes = [];
            //nonUniqueHashes = [];

            foreach (var line in lines)
            {
                var hash = line.ToCRC32();

                if (!uniqueHashes.TryGetValue(hash, out string value))
                    uniqueHashes.Add(hash, line);
                else if (value != "")
                    uniqueHashes[hash] = "";
                
            }

        }
        else
        {
            // Create file if it doesn't exist
            File.WriteAllText(fileLocation, "");
        }
    }

    public static void Update(string[] names)
    {
        string extra = Path.Combine(AppContext.BaseDirectory, "extra");
        string fileLocation = Path.Combine(extra, "romfs-files.txt");

        Directory.CreateDirectory(extra);

        File.WriteAllLines(fileLocation, names);

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
        string extra = Path.Combine(AppContext.BaseDirectory, "extra");
        string fileLocation = Path.Combine(extra, "romfs-files.txt");

        var lines = File.ReadAllLines(fileLocation);
        if (!lines.Contains(fileName))
        {
            lines = [.. lines, fileName];
            File.WriteAllLines(fileLocation, lines);
        }
        
    }
}
