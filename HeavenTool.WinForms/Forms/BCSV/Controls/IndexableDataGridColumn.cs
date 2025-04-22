using System.Windows.Forms;

namespace HeavenTool.Forms.BCSV.Controls;

public class IndexableDataGridColumn(int index) : DataGridViewColumn
{
    public int HeaderIndex { get; set; } = index;
}
