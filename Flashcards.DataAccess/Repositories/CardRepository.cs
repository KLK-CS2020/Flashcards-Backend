using System;
using System.Collections.Generic;
using System.Linq;
using Flashcards.DataAccess.Entities;
using Flashcards.Domain.IRepositories;
using Flashcards_backend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.DataAccess.Repositories
{
    public class CardRepository: ICardRepository
    {
        private readonly MainDbContext _ctx;

        public CardRepository(MainDbContext ctx)
        {
            if (ctx == null)
            {
                throw new InvalidOperationException("CardRepository must have dbContext");
            }
            _ctx = ctx;
        }


        public List<Card> ReadAllCardsByDeckId(int deckId)
        {
            
            return _ctx.Cards.Where(c =>c.Deck.Id == deckId).Select(ca => new Card
                    {
                        Id = ca.Id,
                        Question = ca.Question,
                        Answer = ca.Answer,
                        Correctness = ca.Correctness
                    }).ToList();
        }

        public Card Create(Card newCard)
        {
            var newEntity = new CardEntity
            {
                Question = newCard.Question,
                Answer = newCard.Answer,
                Correctness = newCard.Correctness,
                DeckId = newCard.Deck.Id,
                Deck = new DeckEntity{Id = newCard.Deck.Id}
            };
            _ctx.Cards.Attach(newEntity).State = EntityState.Added;
            _ctx.SaveChanges();
            return newCard;
        }

        public Card Delete(int cardId)
        {
            var cardToDelete =_ctx.Cards.Select(ca => new Card
            {
                Id = ca.Id,
                Question = ca.Question,
                Answer = ca.Answer,
                Correctness = ca.Correctness
            }).FirstOrDefault(c =>c.Id == cardId);
            _ctx.Cards.Remove(new CardEntity(){Id = cardId});
            _ctx.SaveChanges();
            return cardToDelete;
        }

        public Card Update(Card card)
        {
            CardEntity ce = new CardEntity()
            {
                Id = card.Id,
                Question = card.Question,
                Answer = card.Answer,
                Correctness = card.Correctness
            };
            
            var updatedE= _ctx.Update(ce).Entity;

            _ctx.SaveChanges();

            return new Card()
            {
                Id = updatedE.Id,
                Question = updatedE.Question,
                Answer = updatedE.Answer,
                Correctness = updatedE.Correctness
            };
        }
    }
}