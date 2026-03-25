using GameAPI.Model;
using GameAPI.Persistence;
using MySqlConnector;
namespace GameAPI.Service
{
    public class PlayerService
    {
        public Player GetPlayer(int id)
        {
            Player result = null;

            using (var context = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Player WHERE IdPlayer = @IdPlayer";

                using (var command = new MySqlCommand(query, context))
                {
                    command.Parameters.Add(new MySqlParameter("IdPlayer", id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new Player()
                            {
                                IdPlayer = Convert.ToInt32(reader["IdPlayer"].ToString()),
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Deck = reader["Deck"].ToString(),

                            };
                        }
                    }
                }
            }
            return result;
        }

        public int Add(Player player)
        {
            int rows_affected = 0;
            using (var context = DbContext.GetInstance())
            {
                string query = "INSERT INTO Player (Id, Deck, StatsId) VALUES (@id, @deck, @statsId)";
                using (var command = new MySqlCommand(query, context))
                {
                    command.Parameters.Add(new MySqlParameter("id", player.Id));
                    command.Parameters.Add(new MySqlParameter("deck", player.Deck));


                    rows_affected = command.ExecuteNonQuery();

               
                }
            }

            return rows_affected;

        }

        public int Update(Player player)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Player SET Deck = @deck WHERE IdPlayer = @id";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("deck", player.Deck));
                    command.Parameters.Add(new MySqlParameter("id", player.Id));


                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }


        public int Delete(int id)
        {


            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM Player WHERE Id = @Id";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("Id", id));
                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

    }
}
