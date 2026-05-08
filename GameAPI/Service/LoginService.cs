using System.Security.Cryptography;
using System.Text;
using GameAPI.Model;
using GameAPI.Persistence;
using MySqlConnector;

namespace GameAPI.Service
{
    public class LoginService
    {
        private static readonly string PrivateKey = "12345678901234567890123456789012";
        private static readonly string IV = "1234567890123456";

        public List<Login> GetAllLogins()
        {
            var result = new List<Login>();
            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Login";
                using (var command = new MySqlCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dbPass = reader["Passwd"].ToString();
                            result.Add(new Login
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                              
                                Passwd = Decrypt(dbPass)
                            });
                        }
                    }
                }
            }
            return result;
        }


        public Login GetById(int Id)
        {
            Login login = null;

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Login WHERE Id = @Id";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("Id", Id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            login = new Login()
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Passwd = reader["Passwd"].ToString(),
                               
                            };
                        }
                    }
                }
            }
            return login;
        }


        public int Add(Login login)
        {
            int rows_affected = 0;

            string encryptedPass = Encrypt(login.Passwd);

            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Login (Name, Passwd) VALUES (@name, @passwd)";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("name", login.Name));
                    command.Parameters.Add(new MySqlParameter("passwd", encryptedPass));

                    rows_affected = command.ExecuteNonQuery();


                }
            }

            return rows_affected;
        }

        public bool PasswdCorrect(int Id, string Passwd)
        {

           
            using (var ctx = DbContext.GetInstance())
            {
                string query = "SELECT COUNT(*) FROM Usuaris WHERE Id = @id AND Passwd = @passwd";

                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("id", Id));
                    command.Parameters.Add(new MySqlParameter("passwd", Passwd));

                    object result = command.ExecuteScalar();
                    int r = Convert.ToInt32(result);

                    if (r == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }


        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(PrivateKey);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(PrivateKey);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                            return sr.ReadToEnd();
                    }
                }
            }
        }
    }

}

