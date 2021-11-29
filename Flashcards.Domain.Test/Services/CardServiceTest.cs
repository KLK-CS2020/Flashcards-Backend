using System.Collections.Generic;
using System.IO;
using Flashcards.Domain.IRepositories;
using Flashcards.Domain.Services;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Moq;
using Xunit;

namespace Flashcards.Domain.Test.Services
{
    public class CardServiceTest
    {
        private readonly Mock<ICardRepository> _mock;
        private readonly CardService _service;

        public CardServiceTest()
        {
            _mock = new Mock<ICardRepository>();
            _service = new CardService(_mock.Object);
        }
        
        [Fact]
        public void CardService_IsICardService()
        {
            Assert.True(_service is ICardService);
        }

        #region Create
        [Fact]
        public void Create_ReturnsCreatedCardId()
        {
            var passedCard = new Card
            {
                Question = "Pig?",
                Answer = "No!!!",
                Correctness = 0,
                Deck = new Deck
                {
                    Id = 1
                }

            };
            var expected = new Card
            {
               Id = 1,
               Question = "Pig?",
               Answer = "No!!!",
               Correctness = 0,
               Deck = new Deck
               {
                   Id = 1
               }
            };
            _mock.Setup(r => r.Create(passedCard)).Returns(expected);
            var actualCard = _service.Create(passedCard);
            Assert.Equal(expected, actualCard);
        }
        
        [Fact]
        public void Create_EmptyQuestion_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Question = "",
                Answer = "answer",
                Deck = new Deck {Id = 1}
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Create(card));
            Assert.Equal("Question cannot be empty", ex.Message);
        }
        
        [Fact]
        public void Create_EmptyAnswer_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Question = "question",
                Answer = "",
                Deck = new Deck {Id = 1}
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Create(card));
            Assert.Equal("Answer cannot be empty", ex.Message);
        }
        
        [Fact]
        public void Create_InvalidDeckId_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Question = "question",
                Answer = "answer",
                Deck = new Deck {Id = -1}
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Create(card));
            Assert.Equal("Deck id cannot be less than 0", ex.Message);
        }
        
        #endregion
        
        #region Delete
        [Fact]
        public void CardService_Delete_Card_ReturnCard()
        {
            // Arrange
            var card = new Card
            {
                Id = 1,
                Question = "Pig?",
                Answer = "No!!!",
                Correctness = 0
            };
            
            _mock.Setup(r => r.Delete(card.Id))
                .Returns(card);
            // Act
            var actual = _service.Delete(card.Id);
            // Assert
            Assert.Equal(card,actual);
        }
        
        [Fact]
        public void DeleteCard_WithParams_CallsCardRepositoryOnce()
        {
            
            var cardId = (int) 1;
            
            //Act
            _service.Delete(cardId);
            
            //Assert
            _mock.Verify(r => r.Delete(cardId), Times.Once);
        }

        #endregion

        #region GetAll
        [Fact]
        public void GetAllCards_FindAll_ExactlyOnce()
        {
            int deckId = 1;
            
            _service.GetAllCardsByDeckId(deckId);
            _mock.Verify(r=>r.ReadAllCardsByDeckId(deckId), Times.Once);
        }
        

        #endregion

        #region Update
        
        [Fact]
        public void Update_ReturnsUpdatedCard()
        {
            var passedCard = new Card
            {
                Id = 1,
                Question = "Pig?",
                Answer = "No!!!",
                Correctness = 0
            };
            var expected = new Card
            {
                Id = 1,
                Question = "Pig?",
                Answer = "No!!!",
                Correctness = 0
            };
            _mock.Setup(r => r.Update(passedCard)).Returns(expected);
            var actualCard = _service.Update(passedCard);
            Assert.Equal(expected, actualCard);
        }

        [Fact]
        public void Update_EmptyQuestion_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Id = 1,
                Question = "",
                Answer = "answer",
                Correctness = 100
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Update(card));
            Assert.Equal("Question cannot be empty", ex.Message);
        }
        
        [Fact]
        public void Update_EmptyAnswer_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Id = 1,
                Question = "question",
                Answer = "",
                Correctness = 100
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Update(card));
            Assert.Equal("Answer cannot be empty", ex.Message);
        }
        
        [Fact]
        public void Update_CorrectnessLowerThanZero_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Id = 1,
                Question = "question",
                Answer = "answer",
                Correctness = -1
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Update(card));
            Assert.Equal("Correctness cannot be less than 0", ex.Message);
        }
        
        [Fact]
        public void Update_InvalidId_ThrowsInvalidDataException()
        {
            var card = new Card
            {
                Id = -1,
                Question = "question",
                Answer = "answer",
                Correctness = 1
            };
            var ex = Assert.Throws<InvalidDataException>(()=>_service.Update(card));
            Assert.Equal("Id cannot be less than 0", ex.Message);
        }

        #endregion
    }
}