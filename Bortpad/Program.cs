using System;
using System.Windows.Forms;

namespace Bortpad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            string filename = args.Length > 1 ? args[1] : null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new BortForm(filename));
        }
    }
}
