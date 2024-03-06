namespace gameList.Models
{
    public class Game
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public string isDone { get; set; }
        public DateTime createdAt { get; set; }

    }
}
