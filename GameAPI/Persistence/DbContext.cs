using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameAPI.Persistence
{
    public class DbContext
    {
        // 1. Change return type to SqliteConnection
        public static MySqlConnection GetInstance()
        {

            // For now, using your provided string directly:
            string connectionString = "Server=ellaboratori.cat;Port=3306;Database=nilp;Uid=nilp;Pwd=campa123;SslMode=none;AllowPublicKeyRetrieval=true;";

            // 3. Use SqliteConnection (lowercase l)
            var db = new MySqlConnection(connectionString);

            db.Open();

            return db;
        }
    }
}