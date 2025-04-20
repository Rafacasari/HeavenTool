using static HeavenTool.IO.HashManager;

namespace HeavenTool.IO.FileFormats.BCSV;

public class Field
{
    public DataType DataType { get; set; }
    public uint Hash { get; set; }
    public int Offset { get; set; }
    public int Size { get; set; }

    public bool TrustedType { get; set; } = false;

    public object? GetFieldDefaultValue()
    {
        switch (DataType)
        {
            case DataType.U8Array:
                return new byte[Size];

            case DataType.U8:
                return (byte)0;

            case DataType.UInt16:
                return (short)0;

            case DataType.Int32:
                return 0;

            case DataType.Float32:
                return (float)0;

            case DataType.String:
                return "";

            case DataType.UInt32:
            case DataType.CRC32:
            case DataType.MMH3:
                return (uint)0;


            default:
                return null;
        }
    }

    public string HEX { get { return Hash.ToString("x"); } }

    public bool IsMissingHash
    {
        get => CRCHashes.ContainsKey(Hash) == false;
    }

    private string? _displayName;
    public string DisplayName { 
        get
        {
            if (_displayName == null)
            {
                // get translated name
                var translatedName = GetTranslatedNameOrNull();
                if (translatedName != null)
                {
                    // format to display name
                    if (translatedName.Contains('.'))
                        translatedName = translatedName.Split('.')[0];
                    else if (translatedName.Contains(' '))
                        translatedName = translatedName.Split(' ')[0];
                }

                _displayName = translatedName ?? HEX;
            }

            return _displayName;
        }
    }


    public string GetTranslatedNameOrHash()
    {
        return GetTranslatedNameOrNull() ?? HEX;
    }

    public string? GetTranslatedNameOrNull()
    {
        return CRCHashes.TryGetValue(Hash, out string? value) ? value : null;
    }

    public override bool Equals(object obj)
    {
        if (obj is Field field)
            return Hash == field.Hash;
        
        return base.Equals(obj);
    }
}
