using SharpConfig;
using System;
using System.IO;
using System.Windows.Forms;

namespace Bortpad
{
    internal static class Program
    {
        internal const string _CONFIG_FILE = "Bortpad.cfg";
        internal const string _DEFAULT_FONT = "Consolas, 11.25pt";
        internal const int _UTF8_CODEPAGE = 65001;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Configuration config;
            if (File.Exists(_CONFIG_FILE))
            {
                config = Configuration.LoadFromFile(_CONFIG_FILE);
            }
            else
            {
                config = new Configuration();
                Section c1 = config["General"];
                c1["DefaultEncoding"].IntValue = _UTF8_CODEPAGE;
                c1["Font"].StringValue = _DEFAULT_FONT;
                c1["StatusBar"].BoolValue = true;
                c1["WordWrap"].BoolValue = false;
                c1["DarkMode"].BoolValue = false;
                Section c2 = config["Find"];
                c2["Search"].StringValue = "";
                c2["Replace"].StringValue = "";
                c2["Up"].BoolValue = false;
                c2["MatchCase"].BoolValue = false;
                c2["WrapAround"].BoolValue = false;
                config.SaveToFile(_CONFIG_FILE);
            }

            string[] args = Environment.GetCommandLineArgs();
            string filename = args.Length > 1 ? args[1] : null;

            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new BortForm(config, filename));
        }
    }
}