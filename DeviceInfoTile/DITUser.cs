using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class DITUser : Form
    {
        public DITUser()
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
            label1.Text = Environment.UserName;
            label2.Text = System.Environment.GetEnvironmentVariable("USERPROFILE");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var windir = Environment.GetEnvironmentVariable("windir");
            Process process = new Process();
            process.StartInfo.FileName = windir + @"\explorer.exe";
            process.StartInfo.Arguments = "shell:::{60632754-c523-4b62-b45c-4172da012619}";
            process.Start();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 main = new Form1();
            main.Show();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
