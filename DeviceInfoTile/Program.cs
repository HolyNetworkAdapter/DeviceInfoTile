using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceInfoTile
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists("Newtonsoft.Json.dll")) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            } else
            {
                MessageBox.Show("A required library (Newtonsoft.Json) was not found in the directory where DeviceInfoTile is located in. Please fix this issue, and try opening the program again.\nAborting initialization. Click \"OK\" to exit.", "Initialization", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Environment.Exit(1);
            }
        }
    }
}
