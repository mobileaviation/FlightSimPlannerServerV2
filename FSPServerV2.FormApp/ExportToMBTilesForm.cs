using FSPServerV2.Maps.MapChruncher;
using FSPServerV2.Maps.MBTiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void ExportToMBTilesForm_Shown(object sender, EventArgs e)
        {
            MapsListBox.DataSource = Layers.LayerList;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (MapsListBox.SelectedItem is Layer)
            {
                Layer layer = (Layer)MapsListBox.SelectedItem;
                SaveMBTilesFileDialog.FileName = layer.ReferenceName + ".mbtiles";
                DialogResult result = SaveMBTilesFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    MBTiles mBTiles = new MBTiles();
                    mBTiles.CreateDatabaseFile(SaveMBTilesFileDialog.FileName);
                    mBTiles.OpenDatabase(SaveMBTilesFileDialog.FileName);
                    mBTiles.CreateEmptyMBTilesTables();

                    mBTiles.AddTiles(Layers.basePath, layer);

                    mBTiles.CloseDatabase();

                    this.Close();
                }
            }
        }
    }
}
