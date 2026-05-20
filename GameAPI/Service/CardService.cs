using GameAPI.Model;
using GameAPI.Persistence;
using MySqlConnector;

namespace GameAPI.Service
{
    public class CardService
    {
        public Card GetCard(int cardId)
        {
            Card result = null;

            using (var context = DbContext.GetInstance())
            {
         
                var query = "SELECT * FROM Card WHERE cardId = @CardId";

                using (var command = new MySqlCommand(query, context))
                {
                    command.Parameters.Add(new MySqlParameter("CardId", cardId));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            result = new Card()
                            {
                                
                                CardId = Convert.ToInt32(reader["cardId"]),
                                CardType = reader["cardType"].ToString(),
                                CardName = reader["cardName"].ToString(),
                                CardCost = Convert.ToInt32(reader["cardCost"]),
                                CardTier = Convert.ToInt32(reader["cardTier"]),
                                CardInfo = reader["cardInfo"].ToString(),
                                Popularity = Convert.ToInt32(reader["Popularity"])
                            };
                        }
                    }
                }
            }
            return result;
        }


        public Card GetMostPopularCard()
        {
            Card result = null;

            using (var context = DbContext.GetInstance())
            {
           
                var query = "SELECT * FROM Card ORDER BY Popularity DESC LIMIT 1";

                using (var command = new MySqlCommand(query, context))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Card()
                            {
                                CardId = Convert.ToInt32(reader["cardId"]),
                                CardType = reader["cardType"].ToString(),
                                CardName = reader["cardName"].ToString(),
                                CardCost = Convert.ToInt32(reader["cardCost"]),
                                CardTier = Convert.ToInt32(reader["cardTier"]),
                                CardInfo = reader["cardInfo"].ToString(),
                                Popularity = Convert.ToInt32(reader["Popularity"])
                            };
                        }
                    }
                }
            }
            return result;
        }



    }
}
