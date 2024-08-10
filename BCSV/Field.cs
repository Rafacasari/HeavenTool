using Force.Crc32;
using System.Text;
using System.Windows.Forms;

namespace HeavenTool.BCSV
{
    public class Field
    {
        public DataType DataType { get; set; }
        public uint Hash { get; set; }
        public uint Offset { get; set; }
        public uint Size { get; set; } 

        public object GetFieldDefaultValue()
        {
            switch(DataType)
            {
                case DataType.U8:
                    return (byte) 0;

                case DataType.UInt16:
                    return (short) 0;

                case DataType.UInt32:
                    return (int) 0;

                case DataType.Float32:
                    return (float) 0;

                case DataType.String:
                    return "";

                case DataType.HashedCsc32:
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes("None");
                        uint hash = Crc32Algorithm.Compute(bytes);

                        return hash;
                    }

                default:
                    MessageBox.Show($"{GetTranslatedNameOrHash()} {DataType} is't defined, returned null default value thay may have unexpected behaviour!\nReport this to the developer.");
                    return null;
            }
        }

        public string HashedName { get { return Hash.ToString("x"); } }

        public bool IsMissingHash()
        {
            return MainFrm.LoadedHashes.ContainsKey(Hash) == false;
        }

        public string GetTranslatedNameOrHash()
        {
            return MainFrm.LoadedHashes.ContainsKey(Hash) ? MainFrm.LoadedHashes[Hash] : Hash.ToString("x");
        }

        public string GetTranslatedNameOrNull()
        {
            return MainFrm.LoadedHashes.ContainsKey(Hash) ? MainFrm.LoadedHashes[Hash] : null;
        }
    }  
}
