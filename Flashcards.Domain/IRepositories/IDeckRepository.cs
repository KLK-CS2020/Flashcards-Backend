using System.Collections.Generic;
using Flashcards_backend.Core.Models;

namespace Flashcards.Domain.IRepositories
{
    public interface IDeckRepository
    {
        List<Deck> GetAllPublic();
        List<Deck> GetByUserId(int userId);
        Deck GetById(int deckId);
        Deck Create(Deck deck);
        Deck Delete(int deckId);
        Deck Update(Deck deck);
    }
}