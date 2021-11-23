namespace Flashcards.WebApi.Dtos.Deck
{
    public class GetDeckDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int NumberOfCards { get; set; }
        public bool isPublic { get; set; }
    }
}