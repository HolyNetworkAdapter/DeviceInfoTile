using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceInfoTile
{
    public partial class DITSettings : Form
    {
        public DITSettings()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(663, 512);
            String iphidefile = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/.dit-hideip";
            if (File.Exists(iphidefile)) {
                checkBox1.Checked = true;
            } else
            {
                checkBox1.Checked = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DITSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Please restart DeviceTileInfo to fully adjust to the new settings.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            String iphidefile = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/.dit-hideip";
            if (File.Exists(iphidefile))
            {
                checkBox1.Enabled = false;
                File.Delete(iphidefile);
                checkBox1.Enabled = true;
            } else
            {
                using (FileStream fs = File.Create(iphidefile))
                {
                    checkBox1.Enabled = false;
                    Byte[] title = new UTF8Encoding(true).GetBytes("1");
                    fs.Write(title, 0, title.Length);
                    checkBox1.Enabled = true;
                }
            }
        }
    }
}
