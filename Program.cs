using System;
using System.IO;
using System.Windows.Forms;

namespace HeavenTool
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainFrm = new MainFrm();

            if (args.Length == 1)
                mainFrm.HandleInput(args[0]);
            

            Application.Run(mainFrm);
        }

        /// <summary>
        /// Handle path as an input; e.g. User double-clicked a .bcsv file or opened it with the editor.
        /// </summary>
        /// <param name="path">File path</param>
        public static void HandleInput(string path)
        {
            var extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".bcsv":
                    HeavenMain.bcsvEditor.Show();
                    HeavenMain.bcsvEditor.LoadBCSVFile(path);
                    break;
            }
        }
    }
}
