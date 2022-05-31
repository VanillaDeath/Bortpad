using System;
using System.Windows.Forms;

// using WilsonUtils;

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
            // DEBUG
            // LanguageSetter.SetLanguage("ja");

            string[] args = Environment.GetCommandLineArgs();
            string filename = args.Length > 1 ? args[1] : null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Bortpad(filename));
        }
    }
}