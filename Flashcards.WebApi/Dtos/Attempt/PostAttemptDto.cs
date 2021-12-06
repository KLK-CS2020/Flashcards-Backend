using System;

namespace Flashcards.WebApi.Dtos.Attempt
{
    public class PostAttemptDto
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
        public bool WasCorrect { get; set; }
        public DateTime Date { get; set; }
    }
}