using System.Collections.Generic;

namespace HeavenTool.Export
{
    public class ExportedLine
    {
        public int LineId { get; set; }
        public ImportMode Mode { get; set; }
        public Dictionary<string, object> Values { get; set; }

        public ExportedLine(int lineId, ImportMode defaultMode, Dictionary<string, object> values) { 
            LineId = lineId;
            Mode = defaultMode;
            Values = values;
        }
    }
}
