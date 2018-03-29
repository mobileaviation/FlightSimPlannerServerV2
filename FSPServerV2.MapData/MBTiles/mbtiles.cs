using FSPServerV2.MapData.MBTiles;
using FSPServerV2.Maps.MapChruncher;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace FSPServerV2.Maps.MBTiles
{
    public class MBTiles
    {
        public MBTiles()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        private Logger log;
        private SQLiteConnection _connection;
        private String _connectionString;

        public Boolean CreateDatabaseFile(String filename)
        {
            try
            {
                SQLiteConnection.CreateFile(filename);
                return File.Exists(filename);
            }
            catch (Exception ee)
            {
                log.Error("CreateDatabaseFile {0} Exception", filename);
                log.Error(ee);
                return false;
            }
        }

        public Boolean OpenDatabase(String filename)
        {
            try
            {
                _connectionString = String.Format("Data Source={0};Version=3;", filename);
                _connection = new SQLiteConnection(_connectionString);
                _connection.Open();
                return true;
            }
            catch(Exception ee)
            {
                log.Error("OpenDatabase {0} Exception", filename);
                log.Error(ee);
                return false;
            }
        }

        public Boolean CloseDatabase()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch(Exception ee)
            {
                log.Error("CloseDatabase Exception");
                log.Error(ee);
                return false;
            }
        }

        public Boolean CreateEmptyMBTilesTables()
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(CreateDatabaseQueries.CreateMetadata, _connection);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(CreateDatabaseQueries.CreateTiles, _connection);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(CreateDatabaseQueries.CreateTilesIndex, _connection);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(CreateDatabaseQueries.CreateGrids, _connection);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(CreateDatabaseQueries.CreateGridData, _connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ee)
            {
                log.Error("CreateEmptyMBTilesTables Exception");
                log.Error(ee);
                return false;
            }
        }

        public Boolean AddTiles(String basepath, Layer layer)
        {
            if (layer != null)
            {
                String path = basepath + @"\" + layer.TileNamingScheme.FilePrefix + @"\";
                String ext = layer.TileNamingScheme.FileSuffix;

                try
                {
                    foreach (RangeDescriptor descriptor in layer.RangeDescriptors)
                    {
                        String tileFilename = path + descriptor.QuadTreeLocation.ToString() + ext;
                        if (File.Exists(tileFilename))
                        {
                            byte[] imgByteArr = geImageArray(tileFilename);

                            insertTileInDatabase(descriptor, imgByteArr);
                        }
                    }

                    insertMetadata(layer);

                    return true;
                }
                catch (Exception ee)
                {
                    log.Error("AddTiles {0} Exception", layer.DisplayName);
                    log.Error(ee);
                    return false;
                }
            }
            else return false;
            
        }

        private byte[] geImageArray(String filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] imgByteArr = new byte[fs.Length];
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return imgByteArr;
        }

        private Boolean insertTileInDatabase(RangeDescriptor descriptor, byte[]imageArr)
        {
            try
            {
                string insertTileQuery = "INSERT or IGNORE INTO tiles (zoom_level, tile_column, tile_row, tile_data) Values(@Z,@X,@Y,@I);";
                SQLiteCommand insertCmd = new SQLiteCommand(insertTileQuery, _connection);
                insertCmd.Parameters.AddWithValue("@Z", descriptor.Zoom);
                insertCmd.Parameters.AddWithValue("@X", descriptor.TileX);
                insertCmd.Parameters.AddWithValue("@Y", descriptor.TileY);
                insertCmd.Parameters.AddWithValue("@I", imageArr);
                insertCmd.ExecuteNonQuery();
                descriptor.TilePresent = true;
                return true;
            }
            catch(Exception ee)
            {
                log.Error("InsertTile z{0}-x{1}-y{2} Exception", descriptor.Zoom, descriptor.TileX, descriptor.TileY);
                log.Error(ee);
                return false;
            }
        }

        private Boolean insertMetadata(Layer layer)
        {
            try
            {
                insertMetadataRecord("name", layer.DisplayName);
                insertMetadataRecord("format", layer.TileNamingScheme.FileSuffix.Replace(".", ""));
                insertMetadataRecord("bounds", layer.GetBounds());
                insertMetadataRecord("center", layer.GetCenter());
                insertMetadataRecord("minzoom", layer.GetMinZoom().ToString());
                insertMetadataRecord("maxzoom", layer.GetMaxZoom().ToString());
                insertMetadataRecord("attribution", layer.DisplayName);
                insertMetadataRecord("description", layer.DisplayName);
                insertMetadataRecord("type", "overlay");
                insertMetadataRecord("version", "1");
                return true;
            }
            catch(Exception ee)
            {
                log.Error("InsertMetadata {0} Exception", layer.DisplayName);
                log.Error(ee);
                return false;
            }
        }

        private Boolean insertMetadataRecord(String field, String value)
        {
            String query = "INSERT INTO metadata (name, value) Values (@F, @V);";
            SQLiteCommand cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@F", field);
            cmd.Parameters.AddWithValue("@V", value);
            int r = cmd.ExecuteNonQuery();
            return (r > 0);
        }


    }
}
