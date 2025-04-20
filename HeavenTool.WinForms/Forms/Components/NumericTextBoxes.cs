namespace HeavenTool.Forms.Components;

public class Int32TextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !int.TryParse(e.NewText, out int i);
    }
}

public class UInt64TextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !ulong.TryParse(e.NewText, out ulong i);
    }
}

public class ByteTextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !byte.TryParse(e.NewText, out byte i);
    }
}

public class UInt16TextBox : ValidatingTextBox
{
    protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
    {
        e.Cancel = !ushort.TryParse(e.NewText, out ushort i);
    }
}
