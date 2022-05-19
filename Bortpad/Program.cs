using System;
using System.Windows.Forms;

namespace Bortpad
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            string filename = args.Length > 1 ? args[1] : null;

            Application.EnableVisualStyles();
            // Application.SetHighDpiMode(HighDpiMode.SystemAware); // .NET Core only
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new BortForm(filename));
        }
    }
}