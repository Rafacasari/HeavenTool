using HeavenTool.Utility.FileTypes.PBC;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HeavenTool.Forms.PBC;

public partial class PBCEditor : Form
{
    //public int CurrentZoom = 5;
    public PBCFileReader CurrentPBC;
    //public ViewType CurrentView = ViewType.Collision;
    //public bool GridView = true;
    public bool ShowType = true;

    private Action<byte[]> SaveFunction = null;

    public PBCEditor(byte[] fileContent, string fileName, Action<byte[]> saveFunction)
    {
        InitializeComponent();
        SaveFunction = saveFunction;

        pbcPreview.ZoomChanged += ZoomChanged;
        pbcPreview.MouseMove += MouseMoved;
        //pbcPreview.SizeMode = PictureBoxSizeMode.AutoSize;

        Text = $"PBC Editor: {fileName}";
        CurrentPBC = new PBCFileReader(fileContent);
        UpdateStatusLabel();

        gridToolStripMenuItem.Checked = pbcPreview.DisplayGrid;
        viewIDToolStripMenuItem.Checked = ShowType;

        ReloadPBCImage();
    }

    private void UpdateStatusLabel()
    {
        statusLabel.Text = $"Width: {CurrentPBC.Width * 2} | Height: {CurrentPBC.Height * 2} | Offset: (X {CurrentPBC.OffsetX}, Y {CurrentPBC.OffsetY}) ";
    }

    private void MouseMoved(object sender, MouseEventArgs e)
    {
        var currentHeight = pbcPreview.HighlightedHeight != null ? $"| Highlithed Height: {pbcPreview.HighlightedHeight}" : "";
        UpdateStatusLabel();
        statusLabel.Text += currentHeight;
    }

    private void ZoomChanged(int zoom)
    {
        currentZoomMenu.Text = $"Zoom: {zoom}x";
    }

    public void ReloadPBCImage()
    {
        pbcPreview.PBCFile = CurrentPBC;
        pbcPreview.Invalidate();

        heightMap1ToolStripMenuItem.Checked = pbcPreview.CurrentView == ViewType.HeightMap1;
        heightMap2ToolStripMenuItem.Checked = pbcPreview.CurrentView == ViewType.HeightMap2;
        heightMap3ToolStripMenuItem.Checked = pbcPreview.CurrentView == ViewType.HeightMap3;
        collisionMapToolStripMenuItem.Checked = pbcPreview.CurrentView == ViewType.Collision;
        ////var image = CurrentPBC.GenerateImage(CurrentView, tileEditor1.Zoom, GridView, ShowType);
        //currentZoomMenu.Text = $"Zoom: {tileEditor1.Zoom}x";

        //pbcPreview.Image = image;
        //pbcPreview.SizeMode = PictureBoxSizeMode.Normal;
        //pbcPreview.Width = image.Width;
        //pbcPreview.Height = image.Height;
        //pbcPreview.Invalidate();


        propertyGrid1.SelectedObject = CurrentPBC;
    }

    private void zoomPlusButton_Click(object sender, EventArgs e)
    {
        pbcPreview.Zoom++;

        ZoomChanged(pbcPreview.Zoom);
        ReloadPBCImage();
    }

    private void zoomMinusButton_Click(object sender, EventArgs e)
    {
        if (pbcPreview.Zoom > 1)
            pbcPreview.Zoom--;

        ZoomChanged(pbcPreview.Zoom);
        ReloadPBCImage();
    }

    private void viewIDToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowType = !ShowType;
        viewIDToolStripMenuItem.Checked = ShowType;

        ReloadPBCImage();
    }

    private void gridToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //GridView = !GridView;
        
        pbcPreview.DisplayGrid = !pbcPreview.DisplayGrid;
        gridToolStripMenuItem.Checked = pbcPreview.DisplayGrid;

        ReloadPBCImage();
    }

    private void collisionMapToolStripMenuItem_Click(object sender, EventArgs e)
    {
        pbcPreview.CurrentView = ViewType.Collision;
        ReloadPBCImage();
    }

    private void heightMap1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        pbcPreview.CurrentView = ViewType.HeightMap1;
        ReloadPBCImage();
    }

    private void heightMap2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        pbcPreview.CurrentView = ViewType.HeightMap2;
        ReloadPBCImage();
    }

    private void heightMap3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        pbcPreview.CurrentView = ViewType.HeightMap3;
        ReloadPBCImage();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CurrentPBC != null)
            SaveFunction?.Invoke(CurrentPBC.SaveAsBytes());
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        if (CurrentPBC != null)
            SaveFunction?.Invoke(CurrentPBC.SaveAsBytes());
    }
}
