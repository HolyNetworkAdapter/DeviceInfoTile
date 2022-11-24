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
using DeviceInfoTile.Properties;

namespace DeviceInfoTile
{
    public partial class DITWindows : Form
    {
        public DITWindows()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.Opacity = 50;
            pictureBox2.BackColor = Color.Transparent;

            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += (s, e) =>
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementClass mgmtclass1 = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection mgmtObjs1 = mgmtclass1.GetInstances();
            string OS = mgmtObjs1.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass1.Dispose();
            mgmtObjs1.Dispose();
            label1.Text = OS;
            label2.Text = System.Environment.GetEnvironmentVariable("WINDIR");
            label3.Text = "This is an " + System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + " installation of " + OS + ".";

            // comspec
            string owner = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "RegisteredOwner", "").ToString();
            label4.Text = owner;


            // build
            string build = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "BuildLabEx", "").ToString();
            label5.Text = build;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("SystemPropertiesAdvanced.exe");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 main = new Form1();
            main.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTCLIENT = 0x01;

            if (message.Msg == WM_NCHITTEST)
            {
                message.Result = new IntPtr(HTCLIENT);
                return;
            }

            base.WndProc(ref message);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutBox about = new AboutBox();
            about.Show();
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            System.Diagnostics.Process.Start("https://holynetworkadapter.fun");
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LicenseBox l = new LicenseBox();
            l.Show();
        }
    }
}
