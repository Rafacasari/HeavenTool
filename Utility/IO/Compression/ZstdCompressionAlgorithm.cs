using NintendoTools.Compression.Zstd;
using System.IO;
namespace HeavenTool.Utility.IO.Compression;

public static class ZstdCompressionAlgorithm
{
    #region private members
    private static readonly ZstdDecompressor ZstdDecompressor = new();
    #endregion

    public static MemoryStream Decompress(string path)
    {
        using FileStream fileStream = new(path, FileMode.Open, FileAccess.Read);
        MemoryStream memoryStream = new();

        ZstdDecompressor.Decompress(fileStream, memoryStream);
        memoryStream.Position = 0;

        return memoryStream;
    }
}
