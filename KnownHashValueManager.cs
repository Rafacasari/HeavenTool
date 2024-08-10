using HeavenTool.BCSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeavenTool
{
    public static class KnownHashValueManager
    {
        public static Dictionary<string, DataType> KnownTypes = new Dictionary<string, DataType>();
        private static string FILE_PATH = Path.Combine(AppContext.BaseDirectory, "known-types.txt");

        public static void Load()
        {
            if (File.Exists(FILE_PATH))
            {
                var lines = File.ReadAllLines(FILE_PATH);

                foreach (var line in lines)
                {
                    var split = line.Split('=');
                    KnownTypes.Add(split[0], (DataType) Enum.Parse(typeof(DataType), split[1]));
                }
            }
        }

        public static void Save() { 
            File.WriteAllLines(FILE_PATH, KnownTypes.Select(x => {
                return $"{x.Key}={x.Value}";
            }));
        }

        public static void AddOrEdit(string key, DataType value) { 
            if (KnownTypes.ContainsKey(key))
                KnownTypes[key] = value;
            else 
                KnownTypes.Add(key, value);

            Save();
        }
    }
}
