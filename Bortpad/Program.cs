using System;
using System.Diagnostics;
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
            _ = Trace.Listeners.Add(new TextWriterTraceListener(
                $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.log",
                "ErrorLog"));
            Trace.Listeners["ErrorLog"].TraceOutputOptions |= TraceOptions.DateTime;
            Trace.AutoFlush = true;
            // Trace.TraceInformation("Application start.");
            // LanguageSetter.SetLanguage("ja");

            string[] args = Environment.GetCommandLineArgs();
            string filename = args.Length > 1 ? args[1] : null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Bortpad(filename));

            // Trace.TraceInformation("Application end.");
            Trace.Close();
        }
    }
}