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
    public class Quadrant
    {
        public float Val1;
        public float Val2;
        public float Val3;

        public Quadrant(BinaryReader reader)
        {
            Val1 = reader.ReadSingle();
            Val2 = reader.ReadSingle();
            Val3 = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Val1);
            writer.Write(Val2);
            writer.Write(Val3);
        }
    }

    public class HeightMap
    {
        public Quadrant[,] Quadrants;

        public HeightMap(BinaryReader reader) {

            Quadrants = new Quadrant[2, 2];

            for (int subY = 0; subY < 2; subY++)
                for (int subX = 0; subX < 2; subX++)
                    Quadrants[subY, subX] = new Quadrant(reader);

        }

        public void Write(BinaryWriter writer)
        {
            for (int subY = 0; subY < 2; subY++)
                for (int subX = 0; subX < 2; subX++)
                    Quadrants[subY, subX].Write(writer);
        }
    }


    public class Tile
    {
        //public float[][,] Layers;
        public HeightMap HeightMap;
        public TileType[,] Type;

        public Tile(BinaryReader reader)
        {
            // Read Height Map
            HeightMap = new HeightMap(reader);

            // Read Collision Map
            Type = new TileType[2, 2];

            for (int subY = 0; subY < 2; subY++)
                for (int subX = 0; subX < 2; subX++)
                    Type[subY, subX] = (TileType) reader.ReadByte();      
        }


        public void Write(BinaryWriter writer)
        {
            HeightMap.Write(writer);

            for (int subY = 0; subY < 2; subY++)
                for (int subX = 0; subX < 2; subX++)
                    writer.Write((byte) Type[subY, subX]);
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

        writer.Write("pbc\0"u8);

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