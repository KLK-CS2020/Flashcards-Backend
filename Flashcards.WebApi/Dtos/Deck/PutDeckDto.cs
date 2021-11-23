namespace Flashcards.WebApi.Dtos.Deck
{
    public class PutDeckDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isPublic { get; set; }
    }
}