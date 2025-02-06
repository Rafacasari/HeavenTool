using HeavenTool.Utility.IO;
using HeavenTool.Utility.IO.Compression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTool.Utility.FileTypes.PBC;

/// <summary>
/// A class to read PBC files
/// </summary>
public class PBCFileReader
{
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

    public Image Image { get; }

    public PBCFileReader(byte[] buffer)
    {
        using var stream = new MemoryStream(buffer);
        using var reader = new BinaryFileReader(stream);

        if (!"pbc\0"u8.SequenceEqual(reader.ReadBytes(4))) throw new Exception("This is not a PBC file!");

        Width = reader.ReadInt32();
        Height = reader.ReadInt32();

        OffsetX = reader.ReadInt32();
        OffsetY = reader.ReadInt32();

        //var tilesBuffer = new byte[Width * Height * 4];

        using var bmp = new Bitmap(Width * 2, Height * 2);

        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
            { 
                // Padding?
                reader.ReadBytes(0x30);

                var a = reader.ReadByte();
                var b = reader.ReadByte();
                var c = reader.ReadByte();
                var d = reader.ReadByte();

                int baseX = x * 2;
                int baseY = y * 2;


                try
                {
                    bmp.SetPixel(baseX, baseY, Color.FromArgb(255, a, a, a));
                    bmp.SetPixel(baseX, baseY + 1, Color.FromArgb(255, b, b, b));
                    bmp.SetPixel(baseX + 1, baseY + 1, Color.FromArgb(255, c, c, c));
                    bmp.SetPixel(baseX + 1, baseY, Color.FromArgb(255, d, d, d));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Width: {Width * 2} | Height: {Height * 2} | x{baseX} y{baseY}\n{ex}");
                }
            }
        

        Image = bmp.Clone() as Image;
        bmp.Save("output.png");
    }
}


public class TileData
{
    public static TileData ReadTileData(BinaryFileReader reader)
    {

        return new TileData(); 
    }
}