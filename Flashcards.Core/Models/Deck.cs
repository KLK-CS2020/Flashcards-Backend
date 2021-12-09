using System.Collections.Generic;

namespace Flashcards_backend.Core.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public List<Card> Cards { get; set; }
        public bool isPublic { get; set; }
        public bool WasCopied { get; set; }

    }
}