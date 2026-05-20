namespace GameAPI.Model
{
    public class Player
    {
        public int IdPlayer { get; set; }
        public int Id { get; set; }

        public string Deck { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public bool Playing { get; set; }
    }
}
