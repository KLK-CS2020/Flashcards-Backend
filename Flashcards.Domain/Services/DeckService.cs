using System.Collections.Generic;
using System.IO;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Flashcards.Domain.IRepositories;

namespace Flashcards.Domain.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _repo;

        public DeckService(IDeckRepository repo)
        {
            if (repo == null) throw new InvalidDataException("Repository cannot be null");
            _repo = repo;
        }
        public List<Deck> GetAllPublic()
        {
            return _repo.GetAllPublic();
        }

        public List<Deck> GetByUserId(int userId)
        {
            return _repo.GetByUserId(userId);
        }

        public Deck GetById(int deckId)
        {
            return _repo.GetById(deckId);
        }

        public Deck Create(Deck deck)
        {
            return _repo.Create(deck);
        }

        public Deck Delete(int deckId)
        {
            return _repo.Delete(deckId);
        }

        public Deck Update(Deck deck)
        {
            return _repo.Update(deck);
        }
    }
}