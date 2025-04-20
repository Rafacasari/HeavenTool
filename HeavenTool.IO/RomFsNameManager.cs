namespace HeavenTool.IO;

public static class RomFsNameManager
{
    private static Dictionary<uint, string> Hashes { get; set; }

    private static bool _isInitialized;

    private static void Initialize()
    {
        if (_isInitialized) return;
        _isInitialized = true;

        string fileLocation = "romfs-files.txt";

        if (File.Exists(fileLocation))
        {
            var lines = File.ReadAllLines(fileLocation);

            Hashes = [];

            foreach (var line in lines)
            {
                var hash = line.ToCRC32();

                if (!Hashes.TryGetValue(hash, out string value))
                    Hashes.Add(hash, line);
                else if (value != "")
                    Hashes[hash] = "";

            }

        }
        else
        {
            // Create file if it doesn't exist
            File.WriteAllText(fileLocation, "");
        }
    }

    public static string GetValue(uint hash)
    {
        if (!_isInitialized) Initialize();

        var myString = Hashes.TryGetValue(hash, out string value) ? value : null;

        if (string.IsNullOrEmpty(myString)) myString = null;

        return myString ?? $"0x{hash:x}";
    }
}
