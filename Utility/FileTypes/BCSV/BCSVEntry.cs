using System.Collections.Generic;

namespace HeavenTool.Utility.FileTypes.BCSV;

public class BCSVEntry : Dictionary<string, object>
{
    public BCSVEntry() { }

    public BCSVEntry(IDictionary<string, object> source) : base(source)
    {

    }

}
