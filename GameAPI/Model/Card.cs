namespace GameAPI.Model
{
    public class Card
    {
        public int CardId { get; set; }

        public string CardType { get; set; }

        public string CardName { get; set; }

        public int CardCost { get; set; }

        public int CardTier { get; set; }

        public string CardInfo { get; set; }

        public int Popularity { get; set; }
    }
}
