using NintendoTools.Compression.Zstd;
using System.IO;

namespace HeavenTool.Utility.IO.Compression;

public static class ZstdCompressionAlgorithm
{
    private static readonly ZstdDecompressor ZstdDecompressor = new();

    public static MemoryStream Decompress(string path)
    {
        using FileStream fileStream = File.OpenRead(path);
        MemoryStream memoryStream = new();

        ZstdDecompressor.Decompress(fileStream, memoryStream);
        memoryStream.Position = 0;

        return memoryStream;
    }
}
