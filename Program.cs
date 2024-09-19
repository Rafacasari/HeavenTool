using HeavenTool.Utility.IO;
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
            var mainFrm = new BCSVForm();

            if (args.Length > 0)
                switch(args[0])
                {
                    case "--associate":
                        {
                            if (args.Length > 1)
                            {
                                var fileTypeToAssociate = args[1];
                                switch(fileTypeToAssociate)
                                {
                                    case "bcsv":
                                        ProgramAssociation.AssociateProgram(".bcsv", "BCSV", "BCSV File");
                                        //TODO: Move associate to here, so user don't need to open program as admin
                                        break;

                                }
                            }
                            return;
                        }

                    case "--disassociate":
                        {
                            if (args.Length > 1)
                            {
                                var fileTypeToAssociate = args[1];
                                switch (fileTypeToAssociate)
                                {
                                    case "bcsv":
                                        ProgramAssociation.DisassociateProgram(".bcsv", "BCSV");
                                        break;

                                }
                            }
                            return;
                        }

                    default:
                        {
                            mainFrm.HandleInput(args[0]);
                        }
                        break;
                }
            

            Application.Run(mainFrm);
        }

        /// <summary>
        /// Handle path as an input; e.g. User double-clicked a .bcsv file or opened it with the editor.
        /// </summary>
        /// <param name="path">File path</param>
        public static void HandleInput(string[] originalArguments)
        {
            var path = originalArguments[0];
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
