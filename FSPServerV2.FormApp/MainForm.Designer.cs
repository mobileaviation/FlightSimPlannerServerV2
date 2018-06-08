namespace FSPServerV2.FormApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.FSUIPCStatusCheckTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.serverStatusLbl = new System.Windows.Forms.Label();
            this.tcpPortTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMapChruncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToMBTilesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMapChruncherXMLDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.setLocalPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Flight Sim Planner FSUIPC Server V2";
            this.notifyIcon1.BalloonTipTitle = "FSP Server V2";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Flight Sim Planner FSUIPC Server V2";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 48);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.statusToolStripMenuItem.Text = "&Status";
            this.statusToolStripMenuItem.Click += new System.EventHandler(this.statusToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local IPAddress:";
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.Enabled = false;
            this.IPAddressTextBox.Location = new System.Drawing.Point(104, 68);
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(167, 20);
            this.IPAddressTextBox.TabIndex = 2;
            // 
            // FSUIPCStatusCheckTextBox
            // 
            this.FSUIPCStatusCheckTextBox.Enabled = false;
            this.FSUIPCStatusCheckTextBox.Location = new System.Drawing.Point(12, 205);
            this.FSUIPCStatusCheckTextBox.Multiline = true;
            this.FSUIPCStatusCheckTextBox.Name = "FSUIPCStatusCheckTextBox";
            this.FSUIPCStatusCheckTextBox.Size = new System.Drawing.Size(259, 48);
            this.FSUIPCStatusCheckTextBox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(259, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Check FSUIPC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // serverStatusLbl
            // 
            this.serverStatusLbl.Location = new System.Drawing.Point(6, 2);
            this.serverStatusLbl.Name = "serverStatusLbl";
            this.serverStatusLbl.Size = new System.Drawing.Size(249, 37);
            this.serverStatusLbl.TabIndex = 5;
            this.serverStatusLbl.Text = "Local IPAddress:";
            // 
            // tcpPortTextBox
            // 
            this.tcpPortTextBox.Enabled = false;
            this.tcpPortTextBox.Location = new System.Drawing.Point(105, 94);
            this.tcpPortTextBox.Name = "tcpPortTextBox";
            this.tcpPortTextBox.Size = new System.Drawing.Size(43, 20);
            this.tcpPortTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tcp-IP Port:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.serverStatusLbl);
            this.panel1.Location = new System.Drawing.Point(12, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 42);
            this.panel1.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(289, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readMapChruncherToolStripMenuItem,
            this.exportToMBTilesFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.setLocalPathToolStripMenuItem,
            this.toolStripMenuItem2,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // readMapChruncherToolStripMenuItem
            // 
            this.readMapChruncherToolStripMenuItem.Name = "readMapChruncherToolStripMenuItem";
            this.readMapChruncherToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.readMapChruncherToolStripMenuItem.Text = "Read MapChruncher Metadata File";
            this.readMapChruncherToolStripMenuItem.Click += new System.EventHandler(this.readMapChruncherToolStripMenuItem_Click);
            // 
            // exportToMBTilesFileToolStripMenuItem
            // 
            this.exportToMBTilesFileToolStripMenuItem.Enabled = false;
            this.exportToMBTilesFileToolStripMenuItem.Name = "exportToMBTilesFileToolStripMenuItem";
            this.exportToMBTilesFileToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.exportToMBTilesFileToolStripMenuItem.Text = "Export to MBTiles file";
            this.exportToMBTilesFileToolStripMenuItem.Click += new System.EventHandler(this.exportToMBTilesFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(254, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // OpenMapChruncherXMLDialog
            // 
            this.OpenMapChruncherXMLDialog.DefaultExt = "xml";
            this.OpenMapChruncherXMLDialog.Filter = "XML FIles|*.xml";
            this.OpenMapChruncherXMLDialog.Title = "Open MapChruncherMetadata.xml";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Local Path:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(104, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(167, 20);
            this.textBox1.TabIndex = 11;
            // 
            // setLocalPathToolStripMenuItem
            // 
            this.setLocalPathToolStripMenuItem.Name = "setLocalPathToolStripMenuItem";
            this.setLocalPathToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.setLocalPathToolStripMenuItem.Text = "Set Local Path";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(254, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 272);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tcpPortTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FSUIPCStatusCheckTextBox);
            this.Controls.Add(this.IPAddressTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "FSP Server Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IPAddressTextBox;
        private System.Windows.Forms.TextBox FSUIPCStatusCheckTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label serverStatusLbl;
        private System.Windows.Forms.TextBox tcpPortTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readMapChruncherToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenMapChruncherXMLDialog;
        private System.Windows.Forms.ToolStripMenuItem exportToMBTilesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setLocalPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

