using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flashcards.Domain.IRepositories;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;

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
            if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
            return _repo.GetByUserId(userId);
        }

        public Deck GetById(int deckId)
        {
            if (deckId < 0) throw new InvalidDataException("deckId cannot be less than 0");
            return _repo.GetById(deckId);
        }

        public Deck Create(Deck deck)
        {
            if (deck == null)
                throw new ArgumentNullException();
            if (deck.Id != 0)
                throw new InvalidDataException("Id cannot be specified");
            if (deck.Name is null or "")
                throw new InvalidDataException("Name must be specified");
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == deck.User.Id);
            if (user == null)
                throw new InvalidDataException("specified user doesnt exist");
            deck.User = user;
            return _repo.Create(deck);
        }

        public Deck Delete(int deckId)
        {
            if (deckId < 0) throw new InvalidDataException("deckId cannot be less than 0");
            return _repo.Delete(deckId);
        }

        public Deck Update(Deck deck)
        {
            if (deck.Id < 0) throw new InvalidDataException("deckId cannot be less than 0");
            if (deck.Name.Length == 0) throw new InvalidDataException("name cannot be empty");
            if (deck.Description.Length > 250)
                throw new InvalidDataException("description cannot be longer than 250 characters");
            return _repo.Update(deck);
        }
    }
}