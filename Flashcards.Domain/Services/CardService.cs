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
            throw new System.NotImplementedException();
        }

        public Card Create(Card newCard)
        {
            if (newCard == null)
                throw new ArgumentNullException();
            if (newCard.Id != 0)
                throw new InvalidDataException("I dont need id haha");
            if(newCard.Question==null)
                throw new InvalidDataException("Pls add a Question");
            if(newCard.Answer==null)
                throw new InvalidDataException("Pls add a Answer");
            return _repo.Create(newCard);
        }

        public Card Delete(int cardId)
        {
            return _repo.Delete(cardId);
        }

        public Card Update(Card card)
        {
            return _repo.Update(card);
        }
    }
}