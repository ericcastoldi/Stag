using System;
using System.Windows.Forms;

namespace Stag
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            string workspace = null;
            if (args != null && args.Length > 0)
            {
                workspace = args[0];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(workspace));
        }
    }
}