using FSPServerV2.Library;
using FSPServerV2.Maps.MapChruncher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FSPServerV2.FormApp
{
    public partial class MainForm : Form
    {
        Boolean close;

        public MainForm()
        {
            InitializeComponent();

            close = false;

            String strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            var add = from a in addr
                      where a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                      select a;

            IPAddressTextBox.Text = (add.Count() > 0) ? add.ElementAt(0).ToString() : "Unknown!!";
            tcpPortTextBox.Text = Properties.Settings.Default.Port.ToString();

            CheckConnection();
        }

        public Boolean serverRunning;
        public String startupMessage;

        public void SetServerStatus()
        {
            serverStatusLbl.Text = startupMessage;
            if (!serverRunning)
            {
                serverStatusLbl.ForeColor = Color.Red;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                Int32 port = Properties.Settings.Default.Port;
                string baseAddress = "http://localhost:" + port.ToString() + "/";
                ServerTest serverTest = new ServerTest(baseAddress);
                String testOut = serverTest.Test();
                if (testOut== "Error")
                {
                    serverStatusLbl.ForeColor = Color.Red;
                    this.WindowState = FormWindowState.Normal;
                    serverStatusLbl.Text = "Server responsed Error: " + testOut;
                }
                else
                {
                    serverStatusLbl.Text = "Server responsed OK: " + testOut;
                }
                
            }
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void CheckConnection()
        {
            ConnectionTest connectionTest = new ConnectionTest();
            connectionTest.Connect();

            FSUIPCStatusCheckTextBox.Text = connectionTest.status;

            connectionTest.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            close = true;
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState==FormWindowState.Minimized)
            {
                ShowIcon = true;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !close;
            WindowState = FormWindowState.Minimized;

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void readMapChruncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenMapChruncherXMLDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                String filename = OpenMapChruncherXMLDialog.FileName;
                Layers layers = new Layers();
                if (layers.LoadFromFile(filename))
                {
                    ExportToMBTilesForm exportForm = new ExportToMBTilesForm();
                    exportForm.Layers = layers;
                    exportForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error loading MapChruncherMetadata.xml. Maybe version mismatch.", "Error!!");
                }
            }
        }
    }
}
