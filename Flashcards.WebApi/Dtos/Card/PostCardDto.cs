namespace Flashcards.WebApi.Dtos.Card
{
    public class PostCardDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}