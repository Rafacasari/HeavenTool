using System.IO;

namespace HeavenTool.Utility.IO.Compression;

public static class Yaz0CompressionAlgorithm
{
    public const uint Alignment = 0;
    public const int CompressionLevel = 3;

    public static MemoryStream Compress(Stream stream)
    {
        return new MemoryStream(EveryFileExplorer.YAZ0.Compress(stream.ToArray(), CompressionLevel, Alignment));
    }

    public static MemoryStream Compress(byte[] bytes)
    {
        return new MemoryStream(EveryFileExplorer.YAZ0.Compress(bytes, CompressionLevel, Alignment));
    }

    public static Stream Decompress(Stream stream)
    {
        var comp = stream.ToArray();
        var decompressedSize = (uint) (comp[4] << 24 | comp[5] << 16 | comp[6] << 8 | comp[7]);
        var data = Decompress(comp);

        return new MemoryStream(data);
    }

    private static byte[] Decompress(byte[] data)
    {
        return EveryFileExplorer.YAZ0.Decompress(data);
    }
}
