using System;
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
    }
}
