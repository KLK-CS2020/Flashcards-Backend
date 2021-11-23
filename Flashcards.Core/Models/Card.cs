namespace Flashcards_backend.Core.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public double Correctness { get; set; }
        public Deck Deck { get; set; }
    }
}