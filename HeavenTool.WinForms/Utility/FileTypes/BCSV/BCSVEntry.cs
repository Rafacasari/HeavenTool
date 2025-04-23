//using System.Collections.Generic;

//namespace HeavenTool.Utility.FileTypes.BCSV;

//public class BCSVEntry : Dictionary<string, object>
//{
//    public BCSVEntry() { }

//    public BCSVEntry(IDictionary<string, object> source) : base(source)
//    {

//    }

//    internal string GetFormattedValue(Field field)
//    {
//        if (field == null || string.IsNullOrEmpty(field.HEX) || !ContainsKey(field.HEX))
//            return "Invalid";

//        var val = this[field.HEX];

//        switch(field.DataType)
//        {
//            case BCSVDataType.HashedCsc32:
//            {
//                if (val is not uint hashValue) return "Invalid";

//                var containsKey = BCSVHashing.CRCHashes.ContainsKey(hashValue);
//                return containsKey ? BCSVHashing.CRCHashes[hashValue] : hashValue.ToString("x");
//            }

//            case BCSVDataType.Murmur3:
//            {
//                if (val is not uint hashValue) return "Invalid";

//                var containsKey = BCSVHashing.MurmurHashes.ContainsKey(hashValue);
//                return containsKey ? BCSVHashing.MurmurHashes[hashValue] : hashValue.ToString("x");
//            }

//            default:
//                return val.ToString();
//        }
//    }
//}
