using System;
using System.Collections.Generic;
using System.IO;
using Flashcards.Domain.IRepositories;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;

namespace Flashcards.Domain.Services
{
    public class CardService: ICardService
    {
        private readonly ICardRepository _repo;

        public CardService(ICardRepository repository)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Card repo cant be null");
            }
            _repo = repository;
        }
        
        public List<Card> GetAllCardsByDeckId(int deckId)
        {
           return _repo.ReadAllCardsByDeckId(deckId);
        }

        public Card Create(Card newCard)
        {
            if(string.IsNullOrEmpty(newCard.Question))
                throw new InvalidDataException("Question cannot be empty");
            if(string.IsNullOrEmpty(newCard.Answer))
                throw new InvalidDataException("Answer cannot be empty");
            if(newCard.Deck.Id<0)
                throw new InvalidDataException("Deck id cannot be less than 0");
            return _repo.Create(newCard);
        }

        public Card Delete(int cardId)
        {
            return _repo.Delete(cardId);
        }

        public Card Update(Card card)
        {
            if(card.Id<0)
                throw new InvalidDataException("Id cannot be less than 0");
            if(string.IsNullOrEmpty(card.Question))
                throw new InvalidDataException("Question cannot be empty");
            if(string.IsNullOrEmpty(card.Answer))
                throw new InvalidDataException("Answer cannot be empty");
            if(card.Correctness<0)
                throw new InvalidDataException("Correctness cannot be less than 0");
            return _repo.Update(card);
        }
    }
}