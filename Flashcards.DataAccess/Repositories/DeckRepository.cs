using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flashcards_backend.Core.Models;
using Flashcards.DataAccess.Entities;
using Flashcards.Domain.IRepositories;
using Flashcards.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Flashcards.DataAccess.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly MainDbContext _ctx;
        public DeckRepository(MainDbContext ctx)
        {
            if (ctx == null)
                throw new InvalidDataException("Repository must have a dbContext");
            _ctx = ctx;
        }
        public List<Deck> GetAllPublic()
        {
            return _ctx.Decks
                .Include(d=>d.UserEntity)
                .Include(d=>d.Cards)
                .Where(d => d.isPublic == true)
                .Select(de => new Deck
                {
                    Id = de.Id,
                    Description = de.Description,
                    Name = de.Name,
                    isPublic = de.isPublic,
                    User = new User
                    {
                        Id = de.UserEntity.Id,
                        Email = de.UserEntity.Email
                    },
                    Cards = de.Cards.Select(c=>new Card{Id = c.Id}).ToList()
                })
                .ToList();
        }

        public List<Deck> GetByUserId(int userId, string search)
        {
            var decks = _ctx.Decks
                .Include(d => d.UserEntity)
                .Include(d => d.Cards)
                .Where(d => d.UserEntity.Id == userId);

            if (search != null && search.Length > 0)
            {
                decks = decks
                    .Where(d => d.Name.ToLower().Contains(search.ToLower()) ||
                                d.Description.ToLower().Contains(search.ToLower()));
            }

            return decks.Select(de => new Deck
                {
                    Id = de.Id,
                    Description = de.Description,
                    Name = de.Name,
                    isPublic = de.isPublic,
                    User = new User
                    {
                        Id = de.UserEntity.Id,
                        Email = de.UserEntity.Email
                    },
                    Cards = de.Cards.Select(c=>new Card{Id = c.Id}).ToList()
                })
                .ToList();
        }

        public Deck GetById(int deckId, string sortOrder)
        {
            var cards = _ctx.Cards
                .Where(c => c.Deck.Id == deckId)
                .Select(ca => new Card
                {
                    Id = ca.Id,
                    Question = ca.Question,
                    Answer = ca.Answer,
                    Correctness = ca.Correctness,
                    Deck = new Deck{Id = deckId}
                });
            if (!string.IsNullOrEmpty(sortOrder))
            {
                cards = sortOrder switch
                {
                    "correctness_desc" => cards.OrderByDescending(c => c.Correctness),
                    "correctness_asc" => cards.OrderBy(c => c.Correctness),
                    "question_asc" => cards.OrderBy(c => c.Question.ToLower()),
                    _ => cards
                };
            }

            var entity = _ctx.Decks
                .Include(d=>d.UserEntity)
                .FirstOrDefault(de => de.Id == deckId);
            
            return entity==null ? null : new Deck
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                isPublic = entity.isPublic,
                User = new User
                {
                    Id = entity.UserEntity.Id,
                    Email = entity.UserEntity.Email
                },
                Cards = cards.ToList()
            };

        }

        public Deck Create(Deck deck)
        {
            var newDeck = new DeckEntity
            {
                Id = deck.Id,
                Description = deck.Description,
                Name = deck.Name,
                isPublic = deck.isPublic,
                UserId = deck.User.Id,
                UserEntity = new UserEntity
                {
                    Id = deck.User.Id,
                    Email = deck.User.Email,
                    PasswordHash = deck.User.PasswordHash,
                    PasswordSalt = deck.User.PasswordSalt
                },
                Cards = new List<CardEntity>()
            };
            _ctx.Attach(newDeck).State = EntityState.Added;
            _ctx.SaveChanges();
            return deck;
        }

        public Deck Delete(int deckId)
        {
            var deckToDelete = _ctx.Decks
                .Include(de=>de.Cards)
                .Include(de=>de.UserEntity)
                .FirstOrDefault(d => d.Id == deckId);
            _ctx.Decks.Remove(deckToDelete);
            _ctx.SaveChanges();
            
            return new Deck
            {
                Id = deckToDelete.Id,
                Name = deckToDelete.Name,
                Description = deckToDelete.Description,
                isPublic = deckToDelete.isPublic,
                User = new User
                {
                    Id = deckToDelete.UserEntity.Id,
                    Email = deckToDelete.UserEntity.Email
                },
            };
        }

        public Deck Update(Deck deck)
        {
            _ctx.Attach(new DeckEntity
            {
                Id = deck.Id,
                Description = deck.Description,
                Name = deck.Name,
                isPublic = deck.isPublic
            }).State = EntityState.Modified;
            _ctx.SaveChanges();

            return deck;
        }
    }
}