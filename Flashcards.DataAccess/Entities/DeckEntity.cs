namespace Flashcards.DataAccess.Entities
{
    public class DeckEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool isPublic { get; set; }
    }
}