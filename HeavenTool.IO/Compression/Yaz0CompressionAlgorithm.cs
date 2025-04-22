using NintendoTools.Compression.Yaz0;

namespace HeavenTool.IO.Compression;

public static class Yaz0CompressionAlgorithm
{
    private static readonly Yaz0Decompressor Decompressor = new();

    public static Stream Decompress(Stream stream)
    {
        if (!Decompressor.CanDecompress(stream))
            return null;

        var decompressedStream = new MemoryStream();
        Decompressor.Decompress(stream, decompressedStream);
        
        return decompressedStream;
    }

    public static byte[] Decompress(byte[] data)
    {
        var decompressedStream = Decompress(new MemoryStream(data));

        return decompressedStream?.ToArray();
    }
}
