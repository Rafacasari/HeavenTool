using System.Windows.Forms;

namespace HeavenTool.Utility.FileTypes.BCSV
{
    public class Field
    {
        public BcsvDataType DataType { get; set; }
        public uint Hash { get; set; }
        public uint Offset { get; set; }
        public uint Size { get; set; }

        public object GetFieldDefaultValue()
        {
            switch (DataType)
            {
                case BcsvDataType.MultipleU8:
                    return new byte[Size];

                case BcsvDataType.U8:
                    return (byte)0;

                case BcsvDataType.UInt16:
                    return (short)0;

                case BcsvDataType.Int32:
                    return 0;

                case BcsvDataType.Float32:
                    return (float)0;

                case BcsvDataType.String:
                    return "";

                case BcsvDataType.UInt32:
                case BcsvDataType.HashedCsc32:
                case BcsvDataType.Murmur3:
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
            return BCSVForm.CRCHashes.ContainsKey(Hash) == false;
        }

        public string GetTranslatedNameOrHash()
        {
            return BCSVForm.CRCHashes.ContainsKey(Hash) ? BCSVForm.CRCHashes[Hash] : HEX;
        }

        public string GetTranslatedNameOrNull()
        {
            return BCSVForm.CRCHashes.ContainsKey(Hash) ? BCSVForm.CRCHashes[Hash] : null;
        }
    }
}
