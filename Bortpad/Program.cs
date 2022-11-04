using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Bortpad
{
    internal static class Program
    {
        internal static readonly bool DEBUG = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            _ = Trace.Listeners.Add(new TextWriterTraceListener(
                $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.log",
                "ErrorLog"));
            Trace.Listeners["ErrorLog"].TraceOutputOptions |= TraceOptions.DateTime;
            Trace.AutoFlush = true;
            if (DEBUG)
            {
                string[] lang = { "en", "fr", "ja" };
                Trace.TraceInformation("Application start.");
                WilsonUtils.LanguageSetter.SetLanguage(lang[2]);
            }

            string[] args = Environment.GetCommandLineArgs();
            string filename = args.Length > 1 ? args[1] : null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Bortpad(filename));

            if (DEBUG)
            {
                Trace.TraceInformation("Application end.");
            }
            Trace.Close();
        }
    }
}