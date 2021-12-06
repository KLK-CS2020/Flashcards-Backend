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
            var entity = new CardEntity
            {
                Id = card.Id,
                Question = card.Question,
                Answer = card.Answer,
                Correctness = card.Correctness
            };
            _ctx.Attach(entity);
            _ctx.Entry(entity).Property(e => e.Question).IsModified = true;
            _ctx.Entry(entity).Property(e => e.Answer).IsModified = true;
            _ctx.Entry(entity).Property(e => e.Correctness).IsModified = true;

            _ctx.SaveChanges();
            
            return card;
        }

        public void UpdateCorrectness(int cardId, double correctness)
        {
            var card = _ctx.Cards
                .FirstOrDefault(c => c.Id == cardId);
            card.Correctness = correctness;
            _ctx.Attach(card);
            _ctx.Entry(card).Property(c => c.Correctness).IsModified = true;

            _ctx.SaveChanges();
            
        }
    }
}