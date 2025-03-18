// Big thanks to https://github.com/McSpazzy/PBC
using HeavenTool.Utility.IO;
using System;
using System.IO;
using System.Linq;

namespace HeavenTool.Utility.FileTypes.PBC;

/// <summary>
/// A class to read PBC files
/// </summary>
public partial class PBCFileReader
{
    public class Tile
    {
        public float[][,] Layers;
        public TileType[,] Type;

        public Tile(BinaryReader reader)
        {
            // Read Height Map(s)
            Layers = new float[3][,];

            for (var i = 0; i < 3; i++)
            {
                Layers[i] = new float[2, 2];

                Layers[i][1, 1] = reader.ReadSingle();
                Layers[i][0, 0] = reader.ReadSingle();
                Layers[i][0, 1] = reader.ReadSingle();
                Layers[i][1, 0] = reader.ReadSingle();
            }

            // Read Collision Map
            Type = new TileType[2, 2];
            Type[1, 1] = (TileType) reader.ReadByte();
            Type[1, 0] = (TileType) reader.ReadByte();
            Type[0, 0] = (TileType) reader.ReadByte();
            Type[0, 1] = (TileType) reader.ReadByte();
        }

        public float[,] GetHeightMap(int index)
        {
            if (index >= Layers.Length || index < 0)
                return null;

            return Layers[index];
        }

        public void Write(BinaryWriter writer)
        {
            for (var i = 0; i < 3; i++)
                for (var y = 0; y < 2; y++)
                    for (var x = 0; x < 2; x++)
                         writer.Write(Layers[i][x, y]);
            

            writer.Write((byte) Type[0, 0]);
            writer.Write((byte) Type[0, 1]);
            writer.Write((byte) Type[1, 1]);
            writer.Write((byte) Type[1, 0]);
        }
    }

    /// <summary>
    /// Check for file magic, return true if <paramref name="magic"/> is <b>pbc\0</b>
    /// </summary>
    /// <param name="magic">The first 4 bytes of the file.</param>
    /// <returns></returns>
    public static bool MagicMatches(byte[] magic)
    {
        return "pbc\0"u8.SequenceEqual(magic);
    }

    /// <summary>
    /// Image Width
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Image Height
    /// </summary>
    public int Height { get; }

    public int OffsetX { get; }
    public int OffsetY { get; }

    public Tile[,] Tiles { get; set; }

    public Tile this[int height, int width]
    {
        get => Tiles[height, width];
        set => Tiles[height, width] = value;
    }


    public PBCFileReader(byte[] buffer)
    {
        using var stream = new MemoryStream(buffer);
        using var reader = new BinaryFileReader(stream);
        
        if (!MagicMatches(reader.ReadBytes(4))) 
            throw new Exception("This is not a PBC file!");

        Width = reader.ReadInt32();
        Height = reader.ReadInt32();

        OffsetX = reader.ReadInt32();
        OffsetY = reader.ReadInt32();

        Tiles = new Tile[Height, Width];

        for (var h = 0; h < Height; h++)
            for (var w = 0; w < Width; w++)
                Tiles[h, w] = new Tile(reader);
    }   

    public byte[] SaveAsBytes()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);

        writer.Write(Width);
        writer.Write(Height);
        writer.Write(OffsetX);
        writer.Write(OffsetY);

        for (var h = 0; h < Height; h++)
            for (var w = 0; w < Width; w++)
                Tiles[h, w].Write(writer);

        return stream.ToArray();
    }
}