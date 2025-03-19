﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HeavenTool.Utility.FileTypes.PBC;

namespace HeavenTool.Forms.PBC;

public class TileEditor : Control
{
    private PBCFileReader _pbcFile;
    public PBCFileReader PBCFile
    {
        get { return _pbcFile; }
        set
        {
            if (_pbcFile != value)
            {
                Zoom = 10;
                offset = default;
                lastMousePos = null;
            }

            _pbcFile = value;
        }
    }

    public int Zoom { get; set; } = 10;
    public bool DisplayGrid { get; set; } = true;
    public bool ShowType { get; set; } = true;

    public TileType? TileBrush = TileType.Custom1;
    public ViewType CurrentView
    {
        get
        {
            return _heightId switch
            {
                0 => ViewType.HeightMap1,
                1 => ViewType.HeightMap2,
                2 => ViewType.HeightMap3,
                _ => ViewType.Collision
            };

        }
        set
        {
            HeightId = value switch
            {
                ViewType.HeightMap1 => 0,
                ViewType.HeightMap2 => 1,
                ViewType.HeightMap3 => 2,
                _ => -1,
            };
        }
    }

    public int _heightId = -1;
    public int HeightId {
        get { return _heightId; } set
        {
            if ( _heightId != value )
            {
                _heightId = value;
                UpdateHeight();
            }
        }
    }

    public float? MinHeight { get; private set; }
    public float? MaxHeight { get; private set; }

    private void UpdateHeight()
    {
        for (var h = 0; h < PBCFile.Height; h++)
            for (var w = 0; w < PBCFile.Width; w++)
            {
                var tileHeight = PBCFile.Tiles[h, w].HeightMap;
                if (tileHeight != null)
                {
                    foreach (var heightTile in tileHeight.Quadrants)
                    {
                        // Ignore void height
                        if (heightTile.Val2 == -10000000) continue;

                        if (MinHeight == null || heightTile.Val2 < MinHeight)
                            MinHeight = heightTile.Val2;

                        if (MaxHeight == null || heightTile.Val2 > MaxHeight)
                            MaxHeight = heightTile.Val2;
                    }
                }
            }

    }

    private Point offset;
    private Point? lastMousePos;

    public delegate void ZoomEventHandler(int zoom);
    public event ZoomEventHandler ZoomChanged;

    public TileEditor()
    {
        DoubleBuffered = true;

        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);

        BackColor = Color.White;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.Clear(BackColor);
        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

        if (PBCFile == null) return;

        using var pen = new Pen(Color.FromArgb(50, 255, 255, 255), 1);
        using var bigGridPen = new Pen(Color.FromArgb(30, 255, 255, 255), 2);

        var sf = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        var f = new Font(FontFamily.GenericMonospace, Zoom / 2, FontStyle.Regular, GraphicsUnit.Pixel);

        for (int h = 0; h < PBCFile.Height; h++)
        {
            for (int w = 0; w < PBCFile.Width; w++)
            {
                var tile = PBCFile.Tiles[h, w];
                var tileHeight = tile.HeightMap;
                for (int subY = 0; subY < 2; subY++)   
                {
                    int globalY = h * 2 + subY;
                    for (int subX = 0; subX < 2; subX++)
                    {
                        int globalX = w * 2 + subX;

                        // Render HeightMap
                        if (HeightId > -1 && MinHeight.HasValue && MaxHeight.HasValue)
                        {
                            //var heightInfo = tileHeight[subY, subX];
                            var heightInfo = tileHeight.Quadrants[subY, subX].Val2;
                            var c = PBCImageUtilities.GetHeightColor(heightInfo, MinHeight.Value, MaxHeight.Value);
                            using var brush = new SolidBrush(c);
                            e.Graphics.FillRectangle(brush, globalX * Zoom + offset.X, globalY * Zoom + offset.Y, Zoom, Zoom);
                            
                            // Render text if ShowType is true and it's not void
                            if (heightInfo != -10000000 && ShowType)
                                e.Graphics.DrawString(heightInfo.ToString(), f, Brushes.White, new Rectangle(globalX * Zoom + offset.X, globalY * Zoom + offset.Y, Zoom, Zoom));
                        }
                        // Render Tile Color
                        else
                        {
                            using var brush = new SolidBrush(PBCImageUtilities.GetColor(tile.Type[subY, subX]));
                            e.Graphics.FillRectangle(brush, globalX * Zoom + offset.X, globalY * Zoom + offset.Y, Zoom, Zoom);

                            if (ShowType)
                                e.Graphics.DrawString(((int) tile.Type[subY, subX]).ToString(), f, Brushes.White, new Rectangle(globalX * Zoom + offset.X, globalY * Zoom + offset.Y, Zoom, Zoom));
                        }

                        if (DisplayGrid)
                            e.Graphics.DrawRectangle(pen, globalX * Zoom + offset.X, globalY * Zoom + offset.Y, Zoom, Zoom);
                    }
                }

                if (DisplayGrid)
                    e.Graphics.DrawRectangle(bigGridPen, w * 2 * Zoom + offset.X, h * 2 * Zoom + offset.Y, 2 * Zoom, 2 * Zoom);

            }
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        lastMousePos = e.Location;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (!ModifierKeys.HasFlag(Keys.Control) && TileBrush.HasValue && CurrentView == ViewType.Collision)
        {
            int clickedX = (e.X - offset.X) / Zoom;
            int clickedY = (e.Y - offset.Y) / Zoom;
            int tileX = clickedX / 2;
            int tileY = clickedY / 2;
            int subX = clickedX % 2;
            int subY = clickedY % 2;

            if (PBCFile != null && tileX >= 0 && tileX < PBCFile.Width && tileY >= 0 && tileY < PBCFile.Height && subX >= 0 && subY >= 0) 
            {
                var tile = PBCFile[tileY, tileX];
                tile.Type[subY, subX] = TileBrush.Value;
                Invalidate();
            }
        }
    }

    public float? HighlightedHeight { get; set; }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control) && lastMousePos.HasValue)
        {
            int dx = e.X - lastMousePos.Value.X;
            int dy = e.Y - lastMousePos.Value.Y;

            offset.X += dx;
            offset.Y += dy;
            lastMousePos = e.Location;
            Invalidate();
        }
        else if (CurrentView != ViewType.Collision)
        {
            int clickedX = (e.X - offset.X) / Zoom;
            int clickedY = (e.Y - offset.Y) / Zoom;
            int tileX = clickedX / 2;
            int tileY = clickedY / 2;
            int subX = clickedX % 2;
            int subY = clickedY % 2;

            if (PBCFile != null 
                && tileX >= 0 
                && tileX < PBCFile.Width 
                && tileY >= 0 
                && tileY < PBCFile.Height && subX >= 0 && subY >= 0)
            {

                var tile = PBCFile[tileY, tileX];
                //HighlightedHeight = tile.GetHeightMap(HeightId)[subY, subX];
                Invalidate();
            }

        }
        else
        {
            HighlightedHeight = null;
        }

        base.OnMouseMove(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        Zoom = Math.Max(1, Zoom + (e.Delta > 0 ? 1 : -1));
        ZoomChanged?.Invoke(Zoom);
        Invalidate();
    }
}
