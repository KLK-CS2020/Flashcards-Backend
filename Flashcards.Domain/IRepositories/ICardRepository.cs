using System.Collections.Generic;
using Flashcards_backend.Core.Models;

namespace Flashcards.Domain.IRepositories
{
    public interface ICardRepository
    {
        List<Card> ReadAllCardsByDeckId(int deckId);
        Card Create(Card newCard);
        Card Delete(int cardId);
        Card Update(Card card);
        void UpdateCorrectness(int attemptCardId, double correctness);
    }
}