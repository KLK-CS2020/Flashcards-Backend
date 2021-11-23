using System.Collections.Generic;
using System.IO;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Flashcards.Domain.IRepositories;
using Flashcards.Domain.Services;
using Moq;
using Xunit;

namespace Flashcards.Domain.Test.Services
{
    public class DeckServiceTest
    {
        private readonly Mock<IDeckRepository> _mock;
        private readonly DeckService _service;
        private readonly List<Deck> _expected;

        public DeckServiceTest()
        {
            _mock = new Mock<IDeckRepository>();
            _service = new DeckService(_mock.Object);
            _expected = new List<Deck>
            {
                new Deck
                {
                    Id = 1,
                    Name = "Maths",
                    Description = "summing the numbers",
                    isPublic = true,
                    User = new User{Id = 1},
                    Cards = new List<Card>()
                },
                new Deck
                {
                    Id = 2,
                    Name = "Maths2",
                    Description = "summing the numbers2",
                    isPublic = false,
                    User = new User{Id = 1},
                    Cards = new List<Card>()
                }
            };
        }
        
        [Fact]
        public void ProductService_IsIProductService()
        {
            Assert.True(_service is IDeckService);
        }
        
        
        [Fact]
        public void DeckService_WithNullDeckRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new DeckService(null)
            );

        }
        
        [Fact]
        public void DeckService_WithNullDeckRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new DeckService(null)
            );
            Assert.Equal("repository cannot be null",exception.Message);
        }

        [Fact]
        public void GetPublicDecks_CallsDecksRepository_ExactlyOnce()
        {
            _service.GetAllPublic();
            _mock.Verify(r => r.GetAllPublic(), Times.Once);
        }
        
        [Fact]
        public void GetPublicDecks_NoFilter_ReturnsListOfAllPublicDecks()
        {
            _mock.Setup(r => r.GetAllPublic())
                .Returns(_expected);
            var actual = _service.GetAllPublic();
            Assert.Equal(_expected, actual);
        }
        
    }
}