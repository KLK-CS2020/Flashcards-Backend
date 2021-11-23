using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Flashcards.Domain.IRepositories;

namespace Flashcards.Domain.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _repo;
        private readonly IUserRepository _userRepository;

        public DeckService(IDeckRepository repo, IUserRepository userRepository)
        {
            if (repo == null) throw new InvalidDataException("Repository cannot be null");
            if (userRepository == null) throw new InvalidDataException("Repository cannot be null");
            _repo = repo;
            _userRepository = userRepository;
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
            if (deck == null)
                throw new ArgumentNullException();
            if (deck.Id != 0)
                throw new InvalidDataException("Id cannot be specified");
            if(deck.Name==null)
                throw new InvalidDataException("Name must be specified");
            if(deck.Description==null)
                throw new InvalidDataException("Description must be specified");
            if (_userRepository.GetAll().FirstOrDefault(u => u.Id == deck.User.Id) == null)
                throw new InvalidDataException("specified user doesnt exist");
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