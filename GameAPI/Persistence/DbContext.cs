using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameAPI.Persistence
{
    public class DbContext
    {
        private static IConfiguration _configuration;

     
        static DbContext()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static MySqlConnection GetInstance()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "ellaboratori.cat",
                Port = 3306,
                Database = "nilp",
                UserID = "nilp",
                Password = "campa123",
       
                SslMode = MySqlSslMode.None,
                AllowPublicKeyRetrieval = true
            };

            var db = new MySqlConnection(builder.ConnectionString);

            db.Open();

            return db;
        }
    }
}