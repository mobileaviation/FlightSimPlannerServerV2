using FSPServerV2.Library;
using FSPServerV2.Maps.MapChruncher;
using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO;

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

            string path = ConfigurationManager.AppSettings.Get("MBTilesPath");

            if (path == null)
            {
                var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                path = Path.GetDirectoryName(conf.FilePath) + @"\maps";
                storePathInConfig(path);
            }

            localPathEdit.Text = ConfigurationManager.AppSettings.Get("MBTilesPath");

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

        private void storePathInConfig(String newpath)
        {
            var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = conf.AppSettings.Settings;
            if (ConfigurationManager.AppSettings.Get("MBTilesPath") != null)
                settings.Remove("MBTilesPath");
            settings.Add("MBTilesPath", newpath);
            conf.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(conf.AppSettings.SectionInformation.Name);
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

        private Layers layers;

        private void readMapChruncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenMapChruncherXMLDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                String filename = OpenMapChruncherXMLDialog.FileName;
                layers = new Layers();
                if (layers.LoadFromFile(filename))
                {
                    MessageBox.Show(String.Format("Loaded {0} layers from MapChruncherMetadata.xml.", layers.LayerList.Count())
                    , "Success!!");
                    exportToMBTilesFileToolStripMenuItem.Enabled = (layers.LayerList.Count() > 0);
                }
                else
                {
                    MessageBox.Show("Error loading MapChruncherMetadata.xml. Maybe version mismatch.", "Error!!");
                }
            }
        }

        private void exportToMBTilesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (layers != null)
            {
                ExportToMBTilesForm exportForm = new ExportToMBTilesForm();
                exportForm.basePath = ConfigurationManager.AppSettings.Get("MBTilesPath");
                exportForm.Layers = layers;
                exportForm.ShowDialog();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            close = true;
            Application.Exit();
        }

        private void setLocalPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            localFolderDialog.SelectedPath = ConfigurationManager.AppSettings.Get("MBTilesPath");
            if (localFolderDialog.ShowDialog()==DialogResult.OK)
            {
                String newPath = localFolderDialog.SelectedPath;
                storePathInConfig(newPath);
                localPathEdit.Text = newPath;
                MessageBox.Show("Restart the Server for the new Path to take effect!");
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            
        }
    }
}
