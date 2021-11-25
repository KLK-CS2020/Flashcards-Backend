using System;
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
    public class DeckServiceTest
    {
        private readonly List<Deck> _expected;
        private readonly Mock<IDeckRepository> _mock;
        private readonly Mock<IUserRepository> _mockUser;
        private readonly DeckService _service;

        public DeckServiceTest()
        {
            _mock = new Mock<IDeckRepository>();
            _mockUser = new Mock<IUserRepository>();
            _service = new DeckService(_mock.Object, _mockUser.Object);
            _expected = new List<Deck>
            {
                new()
                {
                    Id = 1,
                    Name = "Maths",
                    Description = "summing the numbers",
                    isPublic = true,
                    User = new User {Id = 1},
                    Cards = new List<Card>()
                },
                new()
                {
                    Id = 2,
                    Name = "Maths2",
                    Description = "summing the numbers2",
                    isPublic = false,
                    User = new User {Id = 1},
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
            Assert.Equal("Repository cannot be null", exception.Message);
        }

        [Fact]
        public void DeckService_WithNullUserRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new DeckService(_mock.Object, null)
            );
            Assert.Equal("Repository cannot be null", exception.Message);
        }

        #region GetAllPublic

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

        #endregion

        #region GetByUserId

        [Fact]
        public void GetByUserId_ParameterUserId_ReturnListOfDecks()
        {
            var userId = 1;
            _mock.Setup(r => r.GetByUserId(userId))
                .Returns(_expected);

            var actual = _service.GetByUserId(userId);

            Assert.Equal(_expected, actual);
        }

        [Fact]
        public void GetByUserId_ParameterLessThan0_ThrowsException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.GetByUserId(-1));
            Assert.Equal("userId cannot be less than 0", ex.Message);
        }

        [Fact]
        public void GetByUserId_WithParams_CallsDeckRepositoryOnce()
        {
            var userId = 1;

            _service.GetByUserId(userId);

            _mock.Verify(r => r.GetByUserId(userId), Times.Once);
        }

        #endregion

        #region GetByDeckId

        [Fact]
        public void GetByDeckId_ParameterUserId_ReturnsDeck()
        {
            var deckId = 1;
            var expected = new Deck {Id = 1};
            _mock.Setup(r => r.GetById(deckId))
                .Returns(expected);

            var actual = _service.GetById(deckId);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByDeckId_ParameterLessThan0_ThrowsException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.GetById(-1));
            Assert.Equal("deckId cannot be less than 0", ex.Message);
        }

        [Fact]
        public void GetByDeckId_WithParams_CallsDeckRepositoryOnce()
        {
            var deckId = 1;

            _service.GetById(deckId);

            _mock.Verify(r => r.GetById(deckId), Times.Once);
        }

        #endregion

        #region Create

        [Fact]
        public void Create_ReturnsCreatedDeckWithId()
        {
            var passedDeck = new Deck
            {
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 1}
            };
            var expectedDeck = new Deck
            {
                Id = 1,
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 1}
            };
            _mock.Setup(repo => repo.Create(passedDeck)).Returns(expectedDeck);
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 1}});
            var actualStudent = _service.Create(passedDeck);
            Assert.Equal(expectedDeck, actualStudent);
        }

        [Fact]
        public void Create_DeckNull_ThrowsArgumentNullException()
        {
            Deck invalidDeck = null;

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            Assert.Throws<ArgumentNullException>(Actual);
        }

        [Fact]
        public void Create_IdIsSpecified_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck
            {
                Id = 1,
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 6}});

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            Assert.Throws<InvalidDataException>(Actual);
        }

        [Fact]
        public void Create_IdIsSpecified_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck
            {
                Id = 1,
                Name = "kuba",
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 6}});

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("Id cannot be specified", exception.Message);
        }

        [Fact]
        public void Create_NameIsntSpecified_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck
            {
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 6}});

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            Assert.Throws<InvalidDataException>(Actual);
        }

        [Fact]
        public void Create_NameIsntSpecified_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck
            {
                Description = "aaaaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 6}});

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("Name must be specified", exception.Message);
        }


        [Fact]
        public void Create_WhenSuchUserDoesntExist_ThrowsInvalidDataException()
        {
            var invalidDeck = new Deck
            {
                Name = "kuba",
                Description = "aaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 6}});

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            Assert.Throws<InvalidDataException>(Actual);
        }

        [Fact]
        public void Create_WhenSuchUserDoesntExist_ThrowsInvalidDataExceptionWithMessage()
        {
            var invalidDeck = new Deck
            {
                Name = "kuba",
                Description = "aaaaaaaaaaaa",
                isPublic = true,
                User = new User {Id = 7}
            };
            _mockUser.Setup(repo => repo.GetAll()).Returns(new List<User> {new() {Id = 6}});

            void Actual()
            {
                _service.Create(invalidDeck);
            }

            var exception = Assert.Throws<InvalidDataException>(Actual);
            Assert.Equal("specified user doesnt exist", exception.Message);
        }

        #endregion

        #region Update

        [Fact]
        public void Update_ReturnsUpdatedDeck()
        {
            var deck = new Deck
            {
                Name = "one",
                Description = "oneone",
                isPublic = true,
                User = new User {Id = 1}
            };
            var expected = new Deck
            {
                Id = 1,
                Name = "one",
                Description = "oneone",
                isPublic = true,
                User = new User {Id = 1}
            };
            _mock.Setup(repo => repo.Update(deck))
                .Returns(expected);

            Assert.Equal(expected, _service.Update(deck));
        }

        [Fact]
        public void Update_DeckIdLessThan0_ThrowsException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.Update(new Deck {Id = -1}));
            Assert.Equal("deckId cannot be less than 0", ex.Message);
        }

        [Fact]
        public void Update_DeckEmptyName_ThrowsException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.Update(new Deck {Id = 1, Name = ""}));
            Assert.Equal("name cannot be empty", ex.Message);
        }

        [Fact]
        public void Update_DescriptionTooLong_ThrowsException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.Update(new Deck
            {
                Id = 1, Name = "aa",
                Description =
                    "hVXPcIwTa3A9Velwu3YlXNQWthSOCl50bGUby4R0SJuCzuvqa1voltzjwtg13koc8gI6nO3vDMGJMgVTzLfXHGQWLLal1cQbqsePThZwmlGXxzjFfBhR587lKRJ2lcIcn4NCysL3k1H3n7ldmocGVbyMdNvH318yhA5x7dfKlMb1PSmAfMW3zVFmm7UJOU1detmSpDpnN20thUeNvtwkVUfEQq0fFhKBVDmDTn9EaK2VksQcdIPGgnDdspj"
            }));
            Assert.Equal("description cannot be longer than 250 characters", ex.Message);
        }

        #endregion


        #region Delete

        [Fact]
        public void Delete_ParameterDeckId_ReturnDeck()
        {
            var deck = new Deck
            {
                Id = 1,
                Name = "Maths",
                Description = "summing the numbers",
                isPublic = true,
                User = new User {Id = 1},
                Cards = new List<Card>()
            };

            _mock.Setup(r => r.Delete(deck.Id))
                .Returns(deck);

            var actual = _service.Delete(deck.Id);

            Assert.Equal(deck, actual);
        }

        [Fact]
        public void Delete_ParameterDeckIdLessThan0_ThrowsException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.Delete(-1));
            Assert.Equal("deckId cannot be less than 0", ex.Message);
        }

        [Fact]
        public void DeleteDeck_WithParams_CallsDeckRepositoryOnce()
        {
            var deckId = 1;

            _service.Delete(deckId);

            _mock.Verify(r => r.Delete(deckId), Times.Once);
        }

        #endregion
    }
}