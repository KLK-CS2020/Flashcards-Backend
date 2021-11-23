using System.Collections.Generic;
using Flashcards_backend.Core.Models;

namespace Flashcards_backend.Core.IServices
{
    public interface ICardService
    {
        List<Card> GetAllCardsByDeckId(int deckId);
        Card Create(Card newCard);
        Card Delete(int cardId);
        Card Update(Card card);
    }
}