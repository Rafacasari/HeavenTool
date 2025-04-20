using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeavenTool.Forms.BCSV
{
    public partial class BCSVEnumEditor : Form
    {
        public string keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";

        public BCSVEnumEditor()
        {
            InitializeComponent();
        }

        public string[] Items = [];
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Items = richTextBox1.Text.Split(',');
            statusLabel.Text = $"Size: {richTextBox1.Text.Length} | Entries: {Items.Length}";
            UpdateTextBox();
        }

        private void UpdateTextBox()
        {
            var generator = GenerateStrings().GetEnumerator();
            generator.MoveNext();
            var result = new StringBuilder();
            result.Append("None,");

            int targetLength = 2407;
            while (result.Length < targetLength)
            {
                result.Append(generator.Current + ",");
                generator.MoveNext();
            }
            richTextBox2.Text = result.ToString();
            var entries = richTextBox2.Text.Split(",");
            newStatusLabel.Text = $"Size: {richTextBox2.Text.Length} | Entries: {entries.Length}";
        }
        static IEnumerable<string> GenerateStrings()
        {
            //var chars = new List<char>();

            //// Add printable characters from ISO-8859-1
            //for (int i = 0x21; i <= 0x7E; i++) chars.Add((char)i);
         
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*()[]{};:.~-_/|".ToCharArray();
            int length = 1;

            while (true)
            {
                var current = new int[length];
                while (true)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < length; i++)
                        sb.Append(chars[current[i]]);

                    yield return sb.ToString();

                    int index = length - 1;
                    while (index >= 0 && current[index] == chars.Length - 1)
                    {
                        current[index] = 0;
                        index--;
                    }

                    if (index < 0)
                    {
                        length++;
                        break;
                    }

                    current[index]++;
                }
            }
        }
    }
}
