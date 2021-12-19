using System.Collections.Generic;
using Flashcards.DataAccess.Entities;

namespace Flashcards.DataAccess
{
    public class DbSeeder
    {
        private readonly MainDbContext _ctx;

        public DbSeeder(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var user1 = new UserEntity
            {
                Id = 1,
                Email = "tom123@email.com",
                PasswordHash = new byte[20],
                PasswordSalt = new byte[20]
            };
            var user2 = new UserEntity
            {
                Id = 2,
                Email = "nick543@email.com",
                PasswordHash = new byte[20],
                PasswordSalt = new byte[20]
            };
            _ctx.Users.Add(user1);
            _ctx.Users.Add(user2);

            var deck1 = new DeckEntity
            {
                Id = 1,
                Name = "Maths",
                Description = "summing the numbers",
                isPublic = true,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck2 = new DeckEntity
            {
                Id = 2,
                Name = "English",
                Description = "synonyms",
                isPublic = true,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck3 = new DeckEntity
            {
                Id = 3,
                Name = "Maths",
                Description = "subtracting the numbers",
                isPublic = false,
                UserEntity = user2,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck4 = new DeckEntity
            {
                Id = 4,
                Name = "Biology",
                Description = "animals and trees and bugs and people and humans and gems and bacteries and many other things and somethng else",
                isPublic = false,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck5 = new DeckEntity
            {
                Id = 5,
                Name = "Maths",
                Description = "subtracting the numbers",
                isPublic = false,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck6 = new DeckEntity
            {
                Id = 6,
                Name = "Maths",
                Description = "subtracting the numbers",
                isPublic = false,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck7 = new DeckEntity
            {
                Id = 7,
                Name = "Maths",
                Description = "subtracting the numbers",
                isPublic = false,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };
            var deck8 = new DeckEntity
            {
                Id = 8,
                Name = "Maths",
                Description = "subtracting the numbers",
                isPublic = false,
                UserEntity = user1,
                Cards = new List<CardEntity>(),
                WasCopied = false
            };

            _ctx.Decks.Add(deck1);
            _ctx.Decks.Add(deck2);
            _ctx.Decks.Add(deck3);
            _ctx.Decks.Add(deck4);
            _ctx.Decks.Add(deck5);
            _ctx.Decks.Add(deck6);
            _ctx.Decks.Add(deck7);
            _ctx.Decks.Add(deck8);

            _ctx.Cards.Add(new CardEntity
            {
                Id = 1,
                Question = "2+2",
                Answer = "4",
                Correctness = 100,
                Deck = deck1
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 2,
                Question = "2+4",
                Answer = "6",
                Correctness = 70,
                Deck = deck1
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 3,
                Question = "big",
                Answer = "huge",
                Correctness = 100,
                Deck = deck2
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 4,
                Question = "2-2",
                Answer = "0",
                Correctness = 50,
                Deck = deck3
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 5,
                Question = "Big cat",
                Answer = "Lion",
                Correctness = 82,
                Deck = deck4
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 6,
                Question = "Scary dog",
                Answer = "Wolf",
                Correctness = 50,
                Deck = deck4
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 7,
                Question = "In the middle of the face",
                Answer = "Nose",
                Correctness = 50,
                Deck = deck4
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 8,
                Question = "Two of them on teh face",
                Answer = "Eyes",
                Correctness = 50,
                Deck = deck4
            });
            _ctx.Cards.Add(new CardEntity
            {
                Id = 9,
                Question = "Brain",
                Answer = "brain",
                Correctness = 70,
                Deck = deck4
            });

            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}