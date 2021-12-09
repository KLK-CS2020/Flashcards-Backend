using System.Collections.Generic;

namespace Flashcards.DataAccess.Entities
{
    public class DeckEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public UserEntity UserEntity { get; set; }
        public bool isPublic { get; set; }
        public bool WasCopied { get; set; }
        public List<CardEntity> Cards { get; set; }
    }
}