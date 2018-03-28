using FSPServerV2.MapData.MBTiles;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FSPServerV2.Maps.MBTiles
{
    public class MBTiles
    {
        public MBTiles()
        {

        }

        private SQLiteConnection _connection;
        private String _connectionString;

        public void CreateDatabaseFile(String filename)
        {
            SQLiteConnection.CreateFile(filename);
        }

        public void OpenDatabase(String filename)
        {
            _connectionString = String.Format("Data Source={0};Version=3;", filename);
            _connection.ConnectionString = _connectionString;
            _connection.Open();
        }

        public void CloseDatabase()
        {
            _connection.Close();
        }

        public void CreateEmptyMBTilesTables()
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
        }

    }
}
