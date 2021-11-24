using System;
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
        private readonly Mock<IUserRepository> _mockUser;
        private readonly DeckService _service;
        private readonly List<Deck> _expected;

        public DeckServiceTest()
        {
            _mock = new Mock<IDeckRepository>();
            _mockUser = new Mock<IUserRepository>();
            _service = new DeckService(_mock.Object, _mockUser.Object);
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
                () => new DeckService(null, _mockUser.Object)
            );

        }
        
        [Fact]
        public void DeckService_WithNullUserRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new DeckService(_mock.Object, null)
            );

        }
        
        [Fact]
        public void DeckService_WithNullDeckRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new DeckService(null, null)
            );
            Assert.Equal("Repository cannot be null",exception.Message);
        }
        
        [Fact]
        public void DeckService_WithNullUserRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new DeckService(_mock.Object, null)
            );
            Assert.Equal("Repository cannot be null",exception.Message);
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

        #region Create
        [Fact]
        public void Create_ReturnsCreatedDeckWithId()
        {
            var passedDeck = new Deck()
            {
                Name = "kuba",
               Description = "aaaaaaaaaaaaaa",
               isPublic = true,
               User = new User{Id = 1}
            };
            var expectedDeck = new Deck
            {
                Id =1,
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 1}
            };
            _mock.Setup(repo => repo.Create(passedDeck)).Returns(expectedDeck);
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 1}});
            var actualStudent = _service.Create(passedDeck);
            Assert.Equal(expectedDeck, actualStudent);
        }

        [Fact]
        public void Create_DeckNull_ThrowsArgumentNullException()
        {
            Deck invalidDeck = null;
            void Actual() => _service.Create(invalidDeck);
            Assert.Throws<ArgumentNullException>(Actual);
        }

        [Fact]
        public void Create_IdIsSpecified_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck()
            {
                Id = 1,
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            Assert.Throws<InvalidDataException>(Actual);
        }
        [Fact]
        public void Create_IdIsSpecified_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck()
            {
                Id = 1,
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("Id cannot be specified", exception.Message);
        }
        
        [Fact]
        public void Create_NameIsntSpecified_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck()
            {
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            Assert.Throws<InvalidDataException>(Actual);
        }
        
        [Fact]
        public void Create_NameIsntSpecified_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck()
            {
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("Name must be specified", exception.Message);
        }
        
        [Fact]
        public void Create_DescriptionIsntSpecified_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck()
            {
                Name = "kuba",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            Assert.Throws<InvalidDataException>(Actual);
        }
        
        [Fact]
        public void Create_DescriptionIsntSpecified_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck()
            {
                Name = "kuba",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("Description must be specified", exception.Message);
        }
        
        [Fact]
        public void Create_WhenSuchUserDoesntExist_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck()
            {
                Name = "kuba",
                Description = "aaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            Assert.Throws<InvalidDataException>(Actual);
        }
        
        [Fact]
        public void Create_WhenSuchUserDoesntExist_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck()
            {
                Name = "kuba",
                Description = "aaaaaaaaaaaa",
                isPublic = true,
                User = new User{Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User>() {new User {Id = 6}});
            void Actual() => _service.Create(invalidDeck);
            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("specified user doesnt exist", exception.Message);
        }

        #endregion
        
    }
}