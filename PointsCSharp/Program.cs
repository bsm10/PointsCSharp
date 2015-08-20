using System;
using System.Windows.Forms;

namespace DotsGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static class DebugData
        {
            public static string Value { get; set; }
        }
    }
}
