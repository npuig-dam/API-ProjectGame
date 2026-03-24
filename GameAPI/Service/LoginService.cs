using GameAPI.Persistence;
using MySqlConnector;
using GameAPI.Model;

namespace GameAPI.Service
{
    public class LoginService
    {
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
                            result.Add(new Login
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Passwd = reader["Passwd"].ToString(),
                           
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
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Login (Name, Passwd) VALUES (@name, @passwd)";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("name", login.Name));
                    command.Parameters.Add(new MySqlParameter("passwd", login.Passwd));


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

    }
}
