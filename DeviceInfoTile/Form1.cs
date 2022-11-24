using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace DeviceInfoTile
{
    public partial class Form1 : Form
    {
        public Form1()
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
            label1.Text = Environment.MachineName;
            ManagementClass mgmtclass1 = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection mgmtObjs1 = mgmtclass1.GetInstances();
            string OS = mgmtObjs1.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass1.Dispose();
            mgmtObjs1.Dispose();
            label2.Text = OS;

            // more stuff
            string build = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "BuildLabEx", "").ToString();
            label3.Text = "Build " + build;

            // ram
            double tc = 0;
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher(objectQuery);
            ManagementObjectCollection vals = searcher.Get();

            foreach (ManagementObject val in vals)
            {
                tc += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));
            }
            label4.Text = tc / 1073741824 + " GB (" + tc.ToString() + " bytes)";

            // cpu
            string CPU = "";
            ManagementClass mgmtclass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mgmtObjs = mgmtclass.GetInstances();
            CPU = mgmtObjs.OfType<ManagementObject>().FirstOrDefault().Properties["Name"].Value.ToString();
            mgmtclass.Dispose();
            mgmtObjs.Dispose();
            label5.Text = CPU;

            // local ip
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    try
                    {
                        String iphidefile = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/.dit-hideip";
                        if (File.Exists(iphidefile))
                        {
                            label6.Text = ip.ToString();
                        } else
                        {
                            var WebClient = new WebClient();
                            string pubip = WebClient.DownloadString("https://icanhazip.com");
                            WebClient.Dispose();
                            label6.Text = ip.ToString() + " / " + pubip;
                        }
                    } catch (WebException ex)
                    {
                        label6.Text = ip.ToString() + " / N/A";
                        Console.WriteLine(ex.ToString());
                    }
                } else
                {
                    label6.Text = "Error retrieving IP address";
                }
            }

            // monitor
            string mon = "";
            ManagementClass mgmtclass0 = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection mgmtObjs0 = mgmtclass0.GetInstances();
            mon = mgmtObjs0.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass0.Dispose();
            mgmtObjs0.Dispose();
            label8.Text = mon;

            // sound
            string sound = "";
            ManagementClass mgmtclass01 = new ManagementClass("Win32_SoundDevice");
            ManagementObjectCollection mgmtObjs01 = mgmtclass01.GetInstances();
            sound = mgmtObjs01.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass01.Dispose();
            mgmtObjs01.Dispose();
            label7.Text = sound;
            //label7.Text = Environment.UserName;

            // gpu (reusing)
            string GPU = "";
            ManagementClass mgmtclass2 = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection mgmtObjs2 = mgmtclass2.GetInstances();
            GPU = mgmtObjs2.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass2.Dispose();
            mgmtObjs2.Dispose();
            label8.Text = GPU;

            // monitor (reusing)
            string key = "";
            ManagementClass mgmtclass3 = new ManagementClass("Win32_Keyboard");
            ManagementObjectCollection mgmtObjs3 = mgmtclass3.GetInstances();
            key = mgmtObjs3.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass3.Dispose();
            mgmtObjs3.Dispose();
            label9.Text = key;

            // mouse (reusing)
            string mouse = "";
            ManagementClass mgmtclass4 = new ManagementClass("Win32_PointingDevice");
            ManagementObjectCollection mgmtObjs4 = mgmtclass4.GetInstances();
            mouse = mgmtObjs4.OfType<ManagementObject>().FirstOrDefault().Properties["Caption"].Value.ToString();
            mgmtclass4.Dispose();
            mgmtObjs4.Dispose();
            label10.Text = mouse;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            System.Diagnostics.Process.Start("SystemPropertiesAdvanced.exe");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
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

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            this.Hide();
            DITUser user = new DITUser();
            user.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            this.Hide();
            DITWindows win = new DITWindows();
            win.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            this.Hide();
            DITSettings set = new DITSettings();
            set.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            this.Hide();
            DITNetwork net = new DITNetwork();
            net.Show();
        }
    }
}
