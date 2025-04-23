//using HeavenTool.Utility.FileTypes.BCSV;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace HeavenTool
//{
//    // TODO: Move to BCSVHashing
//    public static class KnownHashValueManager
//    {
//        public static Dictionary<string, BCSVDataType> KnownTypes = new Dictionary<string, BCSVDataType>();
//        private static string FILE_PATH = Path.Combine(AppContext.BaseDirectory, "extra/known-types.txt");

//        public static void Load()
//        {
//            if (File.Exists(FILE_PATH))
//            {
//                var lines = File.ReadAllLines(FILE_PATH);

//                foreach (var line in lines)
//                {
//                    var split = line.Split('=');
//                    KnownTypes.TryAdd(split[0], (BCSVDataType) Enum.Parse(typeof(BCSVDataType), split[1]));
//                }
//            }
//        }

//        public static void Save() { 
//            File.WriteAllLines(FILE_PATH, KnownTypes.Select(x => {
//                return $"{x.Key}={x.Value}";
//            }));
//        }

//        public static void AddOrEdit(string key, BCSVDataType value) { 
//            if (KnownTypes.ContainsKey(key))
//                KnownTypes[key] = value;
//            else 
//                KnownTypes.Add(key, value);

//            Save();
//        }
//    }
//}
