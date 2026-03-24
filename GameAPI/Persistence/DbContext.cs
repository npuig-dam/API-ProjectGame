using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameAPI.Persistence
{
    public class DbContext
    {
        private static IConfiguration _configuration;

        // This "Static Constructor" runs automatically the first time the class is used
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
                // This is the specific property for the MySqlConnector library
                SslMode = MySqlSslMode.None,
                AllowPublicKeyRetrieval = true
            };

            var db = new MySqlConnection(builder.ConnectionString);

            // We open here to catch any "Access Denied" or firewall issues
            db.Open();

            return db;
        }
    }
}