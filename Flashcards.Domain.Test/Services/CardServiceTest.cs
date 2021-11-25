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

        #endregion

        #region GetAll

        [Fact]
        public void GetAllCards_FindAll_ExactlyOnce()
        {
            var deckId = 1;

            _service.GetAllCardsByDeckId(deckId);
            _mock.Verify(r => r.ReadAllCardsByDeckId(deckId), Times.Once);
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
            Assert.Equal(card, actual);
        }

        [Fact]
        public void DeleteCard_WithParams_CallsCardRepositoryOnce()
        {
            var cardId = 1;

            //Act
            _service.Delete(cardId);

            //Assert
            _mock.Verify(r => r.Delete(cardId), Times.Once);
        }

        #endregion
    }
}