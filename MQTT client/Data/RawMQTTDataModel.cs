using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.Entity;
using Microsoft.Data.Sqlite;

namespace MQTT_client.Data
{

    public class RawMQTTDataModel : DbContext
    {
        public DbSet<MQTTMessage> Messages { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "MQTTRawData.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            Debug.WriteLine($"Connecting to:{connection.ConnectionString}");

            optionsBuilder.UseSqlite(connection);
        }
    }
}
