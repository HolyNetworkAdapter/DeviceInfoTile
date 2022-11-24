using DeviceInfoTile.Properties;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
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
    public partial class DITNetwork : Form
    {
        public DITNetwork()
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
            var WebClient = new WebClient();
            try
            {
                string ip = WebClient.DownloadString("http://ipv4.icanhazip.com/");
                string details = WebClient.DownloadString("http://ip.zxq.co/");
                WebClient.Dispose();
                string jdata = details;
                dynamic data = JObject.Parse(jdata);

                label1.Text = ip;
                label2.Text = "The server reports that this is a(n) " + data.country_full + " IP";
                label4.Text = data.city + ", " + data.region + ", " + data.country_full + ", " + data.continent_full;
                pictureBox5.Load("https://holynetworkadapter.fun/flags/" + data.country + ".png");
            } catch (WebException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Can't seem to contact the server, please check your internet connection.", "Connection failure", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                label1.Text = "No internet access";
                label2.Text = "Please make sure you are connected!";
                label4.Text = "N/A";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // nothing
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            var WebClient = new WebClient();
            try
            {
                label1.Text = "Reloading";
                label2.Text = "It may take a few seconds, depending on your internet connection!";
                label4.Text = "N/A";
                string ip = WebClient.DownloadString("http://ipv4.icanhazip.com/");
                string details = WebClient.DownloadString("http://ip.zxq.co/");
                WebClient.Dispose();
                string jdata = details;
                dynamic data = JObject.Parse(jdata);

                label1.Text = ip;
                label2.Text = "The server reports that this is a(n) " + data.country_full + " IP";
                label4.Text = data.city + ", " + data.region + ", " + data.country_full + ", " + data.continent_full;
                pictureBox5.Load("https://holynetworkadapter.fun/flags/" + data.country + ".png");
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                label1.Text = "No internet access";
                label2.Text = "Please make sure you are connected!";
                label4.Text = "N/A";
                pictureBox5.Image = null;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
            }
        }
    }
}
