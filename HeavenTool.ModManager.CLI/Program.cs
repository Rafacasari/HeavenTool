using HeavenTool.IO;
using System.Diagnostics;

namespace HeavenTool.ModManager.CLI
{
    internal class Program
    {
        public const string DEFAULT_OUTPUT_PATH = "output";
        public const string DEFAULT_MODS_FOLDER = "mods";

        static void Main(string[] args)
        {
            var outputPath = DEFAULT_OUTPUT_PATH;
            var modsFolder = DEFAULT_MODS_FOLDER;

            if (Directory.Exists(outputPath) && Directory.GetFiles(outputPath, "*", SearchOption.AllDirectories).Length > 0)
            {
                ConsoleUtilities.WriteLine("Output path is not empty, do you want to delete it? [y/n]", ConsoleColor.Gray);

                if (ConsoleUtilities.YesOrNo())
                {
                    ConsoleUtilities.WriteLine("Deleting output folder...", ConsoleColor.Magenta);
                    Directory.Delete(outputPath, true);
                }
                else
                    ConsoleUtilities.WriteLine("Proceding without deleting output folder. This can cause issues!", ConsoleColor.Red);
            }

            var modMerger = new FileMerger(modsFolder);

            Console.WriteLine("Loading mods...");
            modMerger.SearchModsContentPaths();
            
            ConsoleUtilities.WriteLine("Do you want to merge these mods? [y/n]", ConsoleColor.Gray);
               
            if (!ConsoleUtilities.YesOrNo()) return;
  

            Console.WriteLine("Saving to output folder");
            var outputDirectory = Directory.CreateDirectory(outputPath);

            modMerger.PatchAndExport(outputPath);
            modMerger.CreateResourceSizeTable(outputPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Merge complete!");
            Console.WriteLine($"Saved at: {outputDirectory.FullName}");

            // TODO: Detect if its in Windows
            Process.Start("explorer.exe", outputDirectory.FullName);

            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
