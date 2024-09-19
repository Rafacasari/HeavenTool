using System.Drawing;
using System.Windows.Forms;

namespace HeavenTool.Forms.Components
{
    class DarkMenuStrip : MenuStrip
    {
        public DarkMenuStrip()
        {
            Renderer = new ToolStripProfessionalRenderer(new DarkColorTable());
        }
    }

    class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => Color.Empty;
        public override Color MenuItemSelected => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color MenuItemPressedGradientMiddle => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);

        public override Color ToolStripDropDownBackground => Color.FromArgb(0xff, 0x33, 0x33, 0x33);

        public override Color ImageMarginGradientBegin => Color.Empty;
        public override Color ImageMarginGradientMiddle => Color.Empty;
        public override Color ImageMarginGradientEnd => Color.Empty;

        public override Color ButtonSelectedBorder => Color.Empty;
        public override Color ButtonPressedBorder => Color.Empty;
        public override Color ButtonSelectedGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonSelectedGradientMiddle => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonSelectedGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonPressedGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonPressedGradientMiddle => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonPressedGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonCheckedGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonCheckedGradientMiddle => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonCheckedGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonSelectedHighlightBorder => Color.Empty;
        public override Color ButtonPressedHighlightBorder => Color.Empty;
        public override Color ButtonCheckedHighlightBorder => Color.Empty;
        public override Color ButtonSelectedHighlight => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonPressedHighlight => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ButtonCheckedHighlight => Color.FromArgb(0xff, 0x47, 0x47, 0x47);

        public override Color ToolStripPanelGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ToolStripPanelGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);

        public override Color ToolStripContentPanelGradientBegin => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
        public override Color ToolStripContentPanelGradientEnd => Color.FromArgb(0xff, 0x47, 0x47, 0x47);

        public override Color SeparatorLight => Color.Empty;
        public override Color SeparatorDark => Color.FromArgb(0xff, 0x47, 0x47, 0x47);
    }
    
}
