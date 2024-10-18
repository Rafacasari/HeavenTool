using HeavenTool.Utility.FileTypes.PBC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavenTool.Forms.PBC;

public partial class PBCEditor : Form
{

    public PBCEditor(byte[] fileContent)
    {
        InitializeComponent();

        var fileReader = new PBCFileReader(fileContent);

        statusLabel.Text = $"Width: {fileReader.Width * 2} | Height: {fileReader.Height * 2} | Offset: (X {fileReader.OffsetX}, Y {fileReader.OffsetY}) ";
        pictureBox1.Image = fileReader.Image;
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.Invalidate();
    }
}
