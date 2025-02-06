using HeavenTool.Forms.RSTB;
using HeavenTool.Utility.IO;
using System;
using System.IO;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace HeavenTool
{
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        public const string VERSION = "v1.3.2";
        public static Form TargetForm = null;

        [STAThread]
        static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length > 0)
                    switch (args[0])
                    {
                        case "--associate":
                            {
                                if (args.Length > 1)
                                {
                                    var fileTypeToAssociate = args[1];
                                    switch (fileTypeToAssociate)
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
                                TargetForm = HandleInput(args);
                            }
                            break;
                    }


                // TargetForm is defined by the input provided by the system (user opened a file)
                // If no input is provided (user opened the .exe alone), will open the Main Window
                TargetForm ??= new HeavenMain();

                Application.Run(TargetForm);
            } else
            {
                Console.WriteLine("This is a Window Application, please run it on Windows OS.");
            }
        }

        /// <summary>
        /// Handle path as an input; e.g. User double-clicked a .bcsv file or opened it with the editor.
        /// </summary>
        /// <param name="path">File path</param>
        public static Form HandleInput(string[] originalArguments)
        {
            var path = originalArguments[0];
            var extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".bcsv":
                    var bcsvEditor = new BCSVForm();
                    bcsvEditor.LoadBCSVFile(path);
                    return bcsvEditor;

                case ".resourcesize":
                    var rstbEditor = new RSTBEditor();
                    rstbEditor.LoadFile(path);
                    return rstbEditor;

                default: 
                    return null;
            }
        }
    }
}
