using FSPServerV2.Library;
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

namespace FSPServerV2.FormApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            String strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            var add = from a in addr
                      where a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                      select a;

            IPAddressTextBox.Text = (add.Count() > 0) ? add.ElementAt(0).ToString() : "Unknown!!";

            CheckConnection();
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
            Application.Exit();
        }
    }
}
