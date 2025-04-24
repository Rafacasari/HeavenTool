using System.ComponentModel;

namespace HeavenTool.Forms.Components;

[ToolboxItem(true)]
[DisplayName("Int32 TextBox")]
public class Int32TextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !int.TryParse(e.NewText, out int i);
    }
}

[ToolboxItem(true)]
[DisplayName("UInt32 TextBox")]
public class UInt64TextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !ulong.TryParse(e.NewText, out ulong i);
    }
}

[ToolboxItem(true)]
[DisplayName("Byte TextBox")]
public class ByteTextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !byte.TryParse(e.NewText, out byte i);
    }
}

[ToolboxItem(true)]
[DisplayName("UShort TextBox")]
public class UInt16TextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !ushort.TryParse(e.NewText, out ushort i);
    }
}
