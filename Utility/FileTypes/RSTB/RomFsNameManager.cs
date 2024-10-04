using System.Collections.Generic;
using System.IO;

namespace HeavenTool.Utility.FileTypes.RSTB
{
    public static class RomFsNameManager
    {
        //private static Dictionary<string, uint> loadedNames;
        private static Dictionary<uint, string> uniqueHashes;
        private static List<string> nonUniqueHashes;

        public static Dictionary<uint, string> UniqueNames
        {
            get
            {
                if (!isInitialized)
                    Initialize();

                return uniqueHashes;
            }
        }

        public static List<string> NamesWithDuplicatedHash
        {
            get
            {
                if (!isInitialized)
                    Initialize();

                return nonUniqueHashes;
            }
        }

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
                
                uniqueHashes = new Dictionary<uint, string>();
                nonUniqueHashes = new List<string>();

                foreach(var line in lines)
                {
                    var hash = line.ToCRC32();

                    if (!uniqueHashes.ContainsKey(hash))
                        uniqueHashes.Add(hash, line);
                    else
                    {
                        if (uniqueHashes[hash] != "") {
                            nonUniqueHashes.Add(uniqueHashes[hash]);
                            uniqueHashes[hash] = "";
                        }

                        nonUniqueHashes.Add(line);
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

    }
}
