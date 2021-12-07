using System.Collections.Generic;
using Flashcards_backend.Core.Models;

namespace Flashcards.Domain.IRepositories
{
    public interface IDeckRepository
    {
        List<Deck> GetAllPublic(string search);
        List<Deck> GetByUserId(int userId, string search);
        Deck GetById(int deckId, string sortOrder);
        Deck Create(Deck deck);
        Deck Delete(int deckId);
        Deck Update(Deck deck);
        Deck CreateCopied(Deck newDeck);
    }
}