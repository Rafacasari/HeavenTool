using System.Windows.Forms;
using static HeavenTool.Utility.FileTypes.BCSV.BCSVHashing;

namespace HeavenTool.Utility.FileTypes.BCSV;

public class Field
{
    public BCSVDataType DataType { get; set; }
    public uint Hash { get; set; }
    public uint Offset { get; set; }
    public uint Size { get; set; }

    public bool TrustedType { get; set; } = false;

    public object GetFieldDefaultValue()
    {
        switch (DataType)
        {
            case BCSVDataType.MultipleU8:
                return new byte[Size];

            case BCSVDataType.U8:
                return (byte)0;

            case BCSVDataType.UInt16:
                return (short)0;

            case BCSVDataType.Int32:
                return 0;

            case BCSVDataType.Float32:
                return (float)0;

            case BCSVDataType.String:
                return "";

            case BCSVDataType.UInt32:
            case BCSVDataType.HashedCsc32:
            case BCSVDataType.Murmur3:
                {
                    return (uint)0;
                }

            default:
                MessageBox.Show($"{GetTranslatedNameOrHash()} {DataType} is't defined, returned null default value thay may have unexpected behaviour!\nReport this to the developer.");
                return null;
        }
    }

    public string HEX { get { return Hash.ToString("x"); } }

    public bool IsMissingHash()
    {
        return CRCHashes.ContainsKey(Hash) == false;
    }

    public string GetTranslatedNameOrHash()
    {
        return GetTranslatedNameOrNull() ?? HEX;
    }

    public string GetTranslatedNameOrNull()
    {
        return CRCHashes.TryGetValue(Hash, out string value) ? value : null;
    }
}
