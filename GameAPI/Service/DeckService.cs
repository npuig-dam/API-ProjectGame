using GameAPI.Model;
using GameAPI.Persistence;
using MySqlConnector;

namespace GameAPI.Service
{
    public class DeckService
    {
    
       
            public List<Deck> GetAllDecks()
            {
                List<Deck> result = new List<Deck>();

                using (var context = DbContext.GetInstance())
                {
                    var query = "SELECT * FROM Deck";

                    using (var command = new MySqlCommand(query, context))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Deck()
                                {
                                    DeckId = Convert.ToInt32(reader["deckId"]),
                                    CardsIds = reader["cardsIds"].ToString(),
                                    CreatorId = Convert.ToInt32(reader["creatorId"]),
                                    DeckName = reader["deckName"].ToString()
                                });
                            }
                        }
                    }
                }
                return result;
            }

    
            public Deck GetById(int deckId)
            {
                Deck result = null;

                using (var context = DbContext.GetInstance())
                {
                    var query = "SELECT * FROM Deck WHERE deckId = @DeckId";

                    using (var command = new MySqlCommand(query, context))
                    {
                        command.Parameters.Add(new MySqlParameter("DeckId", deckId));

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = new Deck()
                                {
                                    DeckId = Convert.ToInt32(reader["deckId"]),
                                    CardsIds = reader["cardsIds"].ToString(),
                                    CreatorId = Convert.ToInt32(reader["creatorId"]),
                                    DeckName = reader["deckName"].ToString()
                                };
                            }
                        }
                    }
                }
                return result;
            }


            public Deck GetByName(string deckName)
            {
                Deck result = null;

                using (var context = DbContext.GetInstance())
                {
                    var query = "SELECT * FROM Deck WHERE deckName = @DeckName";

                    using (var command = new MySqlCommand(query, context))
                    {
                        command.Parameters.Add(new MySqlParameter("DeckName", deckName));

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = new Deck()
                                {
                                    DeckId = Convert.ToInt32(reader["deckId"]),
                                    CardsIds = reader["cardsIds"].ToString(),
                                    CreatorId = Convert.ToInt32(reader["creatorId"]),
                                    DeckName = reader["deckName"].ToString()
                                };
                            }
                        }
                    }
                }
                return result;
            }

     
            public int Add(Deck deck)
            {
                int lastInsertedId = 0;

                using (var context = DbContext.GetInstance())
                {
             
                    var query = @"INSERT INTO Deck (cardsIds, creatorId, deckName) 
                              VALUES (@CardsIds, @CreatorId, @DeckName);
                              SELECT LAST_INSERT_ID();";

                    using (var command = new MySqlCommand(query, context))
                    {
                        command.Parameters.Add(new MySqlParameter("CardsIds", deck.CardsIds));
                        command.Parameters.Add(new MySqlParameter("CreatorId", deck.CreatorId));
                        command.Parameters.Add(new MySqlParameter("DeckName", deck.DeckName));

                        lastInsertedId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                return lastInsertedId;
            }

        public bool UpdateDeckCards(int deckId, string newCardsIds)
        {
            int rowsAffected = 0;

            using (var context = DbContext.GetInstance())
            {
                var query = "UPDATE Deck SET cardsIds = @CardsIds WHERE deckId = @DeckId";

                using (var command = new MySqlCommand(query, context))
                {
                    command.Parameters.Add(new MySqlParameter("CardsIds", newCardsIds));
                    command.Parameters.Add(new MySqlParameter("DeckId", deckId));

           
                    rowsAffected = command.ExecuteNonQuery();
                }
            }


            return rowsAffected > 0;
        }


        public bool UpdateDeckName(int deckId, string newDeckName)
        {
            int rowsAffected = 0;

            using (var context = DbContext.GetInstance())
            {
                var query = "UPDATE Deck SET deckName = @DeckName WHERE deckId = @DeckId";

                using (var command = new MySqlCommand(query, context))
                {
                    command.Parameters.Add(new MySqlParameter("DeckName", newDeckName));
                    command.Parameters.Add(new MySqlParameter("DeckId", deckId));

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected > 0;
        }
    }
}
