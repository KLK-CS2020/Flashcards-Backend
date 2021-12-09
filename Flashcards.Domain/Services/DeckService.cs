using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flashcards_backend.Core.Filtering;
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
        public List<Deck> GetAllPublic(string search, Filter filter)
        {
            if (filter.CurrentPage < 1) throw new InvalidDataException("current page must be at least 1");
            if (filter.ItemsPrPage < 1) throw new InvalidDataException("there must be at least 1 item per page");
            return _repo.GetAllPublic(search, filter);
        }

        public List<Deck> GetByUserId(int userId, string search)
        {
            if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
            return _repo.GetByUserId(userId, search);
        }

        public Deck GetById(int deckId, string sortOrder)
        {
            if (deckId < 0) throw new InvalidDataException("deckId cannot be less than 0");
            return _repo.GetById(deckId, sortOrder);
        }

        public Deck Create(Deck deck)
        {
            if (deck == null)
                throw new ArgumentNullException();
            if (deck.Id != 0)
                throw new InvalidDataException("Id cannot be specified");
            if(deck.Name is null or "" )
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
            if (deck.Name.Length==0) throw new InvalidDataException("name cannot be empty");
            if (deck.Description.Length > 250) throw new InvalidDataException("description cannot be longer than 250 characters");
            return _repo.Update(deck);
        }

        public Deck CreateCopied(int deckId, int userId)
        {
            if (deckId < 0) throw new InvalidDataException("deckId cannot be less than 0");
            if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
            
            var original = _repo.GetById(deckId, "");

            if (original.User.Id == userId)
                throw new InvalidDataException("you cannot copy public deck for the owner of the deck");
            
            var newDeck = new Deck
            {
                Name = original.Name,
                Description = original.Description,
                isPublic = false,
                WasCopied = true,
                User = new User {Id = userId},
                Cards = original.Cards
            };
            
            return _repo.CreateCopied(newDeck);
        }
    }
}