using FSPServerV2.Maps.MapChruncher;
using FSPServerV2.Maps.MBTiles;
using System;
using System.Windows.Forms;

namespace FSPServerV2.FormApp
{
    public partial class ExportToMBTilesForm : Form
    {
        public ExportToMBTilesForm()
        {
            InitializeComponent();
        }

        public Layers Layers { get; set; }
        public String basePath { get; set; }

        private void ExportToMBTilesForm_Shown(object sender, EventArgs e)
        {
            MapsListBox.DataSource = Layers.LayerList;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (MapsListBox.SelectedItem is Layer)
            {
                Layer layer = (Layer)MapsListBox.SelectedItem;
                SaveMBTilesFileDialog.FileName = basePath + @"\" + layer.ReferenceName + ".mbtiles";
                DialogResult result = SaveMBTilesFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    exportProgressBar.Visible = true;
                    SaveBtn.Enabled = false;
                    CancelBtn.Enabled = false;
                    MapsListBox.Enabled = false;

                    MBTiles mBTiles = new MBTiles();

                    mBTiles.progressEvent += MBTiles_progressEvent;

                    if (!mBTiles.CreateDatabaseFile(SaveMBTilesFileDialog.FileName))
                    {
                        MessageBox.Show("Error creating MBTiles file!", "Error");
                        this.Close();
                        return;
                    }
                    if (!mBTiles.OpenDatabase(SaveMBTilesFileDialog.FileName))
                    {
                        MessageBox.Show("Error opening MBTiles file!", "Error");
                        this.Close();
                        return;
                    }
                    if (!mBTiles.CreateEmptyMBTilesTables())
                    {
                        MessageBox.Show("Error creating database tables in MBTiles file!", "Error");
                        this.Close();
                        return;
                    }
                    if (!mBTiles.AddTiles(Layers.basePath, layer))
                    {
                        MessageBox.Show("Error adding tiles to MBTiles file!", "Error");
                        this.Close();
                        return;
                    }
                    if (!mBTiles.CloseDatabase())
                    {
                        MessageBox.Show("Error closing MBTiles file!", "Error");
                        this.Close();
                        return;
                    }

                    this.Close();
                }
            }
        }

        private void MBTiles_progressEvent(object sender, int tileCount, int tileNumber)
        {
            exportProgressBar.Maximum = tileCount;
            exportProgressBar.Value = tileNumber;
        }
    }
}
