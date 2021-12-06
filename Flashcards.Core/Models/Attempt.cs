using System;

namespace Flashcards_backend.Core.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CardId { get; set; }
        public bool WasCorrect { get; set; }
        public DateTime Date { get; set; }
    }
}