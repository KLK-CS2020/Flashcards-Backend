using Flashcards_backend.Core.Models;

namespace Flashcards.DataAccess.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public double Correctness { get; set; }
        public int DeckId { get; set; }
        public DeckEntity Deck { get; set; }
    }
}