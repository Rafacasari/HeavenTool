using ScintillaNET;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HeavenTool.Forms
{
    public partial class EditorWindow : Form
    {
        public EditorWindow()
        {
            InitializeComponent();

            EnableFolding();

            InitSyntaxColoring();

            codeEditor.CaretLineBackColor = Color.DarkGray;
            codeEditor.CaretForeColor = Color.FromArgb(50, 50, 50);
            codeEditor.SelectionBackColor = Color.FromArgb(38, 79, 120);
            codeEditor.SelectionTextColor = Color.FromArgb(100, 100, 100);

            codeEditor.WhitespaceBackColor = Color.FromArgb(50, 50, 50);
            codeEditor.WrapMode = WrapMode.Word;
            //wordWrapToolStripMenuItem.Checked = true;

            codeEditor.Margins[0].Type = MarginType.Number;
            codeEditor.Margins[0].Width = 35;
        }

        private void InitSyntaxColoring()
        {
            codeEditor.StyleResetDefault();
            codeEditor.Styles[Style.Default].Font = "Consolas";
            codeEditor.Styles[Style.Default].Size = 10;
            codeEditor.Styles[Style.Default].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.Default].ForeColor = FORE_COLOR;
            codeEditor.StyleClearAll();

            codeEditor.Styles[Style.LineNumber].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.LineNumber].ForeColor = FORE_COLOR;
            codeEditor.Styles[Style.IndentGuide].ForeColor = FORE_COLOR;
            codeEditor.Styles[Style.IndentGuide].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.FoldDisplayText].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.Default].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.LineNumber].ForeColor = Color.DarkCyan;
            codeEditor.Styles[Style.BraceBad].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.BraceLight].BackColor = BACK_COLOR;
            codeEditor.Styles[Style.CallTip].BackColor = BACK_COLOR;


            ConfigureCustomLexer(codeEditor);
            //codeEditor.LexerName = "BCSV";
            // scintilla1.LexerLanguage = scintilla1.GetLexerIDFromLexer(Lexer.SCLEX_CPP);

            // Configure the CPP (C#) lexer styles
        }

        private static readonly Color BACK_COLOR = Color.FromArgb(30, 30, 30);
        private static readonly Color FORE_COLOR = Color.White;
        public void EnableFolding()
        {
            // Enable folding
            codeEditor.SetProperty("fold", "1");
            codeEditor.SetProperty("fold.compact", "1");

            codeEditor.Margins[0].Width = 20;

            // Use Margin 2 for fold markers
            codeEditor.Margins[2].Type = MarginType.Symbol;
            codeEditor.Margins[2].Mask = Marker.MaskFolders;
            codeEditor.Margins[2].Sensitive = true;
            codeEditor.Margins[2].Width = 20;

            // Reset folder markers
            for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
            {
                codeEditor.Markers[i].SetForeColor(BACK_COLOR);
                codeEditor.Markers[i].SetBackColor(Color.Gray);
            }

            codeEditor.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            codeEditor.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            codeEditor.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            codeEditor.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            codeEditor.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            codeEditor.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            codeEditor.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            codeEditor.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;

            codeEditor.SetFoldMarginColor(true, Color.FromArgb(100, 100, 100));
            codeEditor.SetFoldMarginHighlightColor(true, BACK_COLOR);

     
        }

        public void ConfigureCustomLexer(Scintilla scintilla)
        {
            // Definir o lexer como container para personalização

            // Configuração de estilo para o tipo customizado
            scintilla.Styles[0].ForeColor = Color.Black;    // Default
            scintilla.Styles[1].ForeColor = Color.Blue;     // Headers
            scintilla.Styles[2].ForeColor = Color.Brown;    // Values
            scintilla.Styles[3].ForeColor = Color.Red;      // Entry Name

            // Exemplo de coloração manual com base em regex
            scintilla.StyleNeeded += (sender, e) =>
            {
                var sci = sender as Scintilla;
                sci.StartStyling(0);
                int position = 0;

                var text = sci.Text;

                // Regex para identificar entries (sem indentação)
                var entryRegex = new Regex(@"^[^\s].*", RegexOptions.Multiline);

                // Regex para identificar headers (com uma indentação)
                var headerRegex = new Regex(@"^\s+\w+=", RegexOptions.Multiline);

                // Regex para identificar valores (qualquer coisa após o "=")
                var valueRegex = new Regex(@"=\s*""[^""]*""|\s*\d+", RegexOptions.Multiline);

                // Estilizando entries
                foreach (Match match in entryRegex.Matches(text))
                {
                    sci.SetStyling(match.Index - position, 0); // Ignorar até o próximo match
                    sci.SetStyling(match.Length, 3);           // Estilizar entry
                    position = match.Index + match.Length;
                }

                // Estilizando headers e valores
                foreach (Match match in headerRegex.Matches(text))
                {
                    if (match.Index - position <= 0) continue;

                    sci.SetStyling(match.Index - position, 0); // Ignorar até o próximo match
                    sci.SetStyling(match.Length, 1);           // Estilizar header

                    // Ajustar para estilizar o valor logo após o header
                    var valueMatch = valueRegex.Match(text, match.Index + match.Length);
                    if (valueMatch.Success && valueMatch.Index == match.Index + match.Length)
                    {
                        sci.SetStyling(valueMatch.Length, 2);  // Estilizar value
                        position = valueMatch.Index + valueMatch.Length;
                    }
                    else
                    {
                        position = match.Index + match.Length;
                    }
                }
            };
        }
    }
}
