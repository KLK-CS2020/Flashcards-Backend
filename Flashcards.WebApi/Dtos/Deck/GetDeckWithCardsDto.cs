using System.Collections.Generic;
using Flashcards.WebApi.Dtos.Card;

namespace Flashcards.WebApi.Dtos.Deck
{
    public class GetDeckWithCardsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool IsPublic { get; set; }
        public bool WasCopied { get; set; }
        public List<CardInDeckDto> Cards { get; set; }
        
    }
}