namespace Flashcards.WebApi.Dtos.Card
{
    public class PostCardDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int deckId { get; set; }
        
    }
}