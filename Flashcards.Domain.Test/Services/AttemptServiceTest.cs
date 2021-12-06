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
    public class AttemptServiceTest
    {
        private readonly Mock<IAttemptRepository> _mock;
        private readonly Mock<ICardRepository> _cardMock;
        private readonly AttemptService _service;

        public AttemptServiceTest()
        {
            _mock = new Mock<IAttemptRepository>();
            _cardMock = new Mock<ICardRepository>();
            _service = new AttemptService(_mock.Object, _cardMock.Object);
        }
        [Fact]
        public void AttemptService_IsIAttemptService()
        {
            Assert.True(_service is IAttemptService);
        }
        
        
        [Fact]
        public void AttemptService_WithNullAttemptRepository_ThrowsInvalidDataException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new AttemptService(null, _cardMock.Object));
            Assert.Equal("repo cannot be null", ex.Message);
        }
        
        [Fact]
        public void AttemptService_WithNullCardRepository_ThrowsInvalidDataException()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new AttemptService(_mock.Object, null));
            Assert.Equal("repo cannot be null", ex.Message);
        }

        [Fact]
        public void Get_UserIdLessThan0_ThrowsException()
        {
            var userId = -1;
            var cardId = 1;
            var quantity = 3;
            
            var ex = Assert.Throws<InvalidDataException>(() => _service.Get(userId, cardId, quantity));
            Assert.Equal("userId cannot be less than 0", ex.Message);
        }

        [Fact]
        public void Get_CardIdLessThan0_ThrowsException()
        {
            var userId = 1;
            var cardId = -1;
            var quantity = 3;
            
            var ex = Assert.Throws<InvalidDataException>(() => _service.Get(userId, cardId, quantity));
            Assert.Equal("cardId cannot be less than 0", ex.Message);
        }
        
        [Fact]
        public void Get_QuantityLessThan1_ThrowsException()
        {
            var userId = 1;
            var cardId = 1;
            var quantity = 0;
            
            var ex = Assert.Throws<InvalidDataException>(() => _service.Get(userId, cardId, quantity));
            Assert.Equal("quantity must be at least 1", ex.Message);
        }

        [Fact]
        public void Create_userIdLessThan0_ThrowsException()
        {
            var attempt = new Attempt
            {
                UserId = -1,
                CardId = 1,
                WasCorrect = true,
                Date = new DateTime(2021, 11, 28)
            };

            var ex = Assert.Throws<InvalidDataException>(() => _service.Create(attempt));
            Assert.Equal("userId cannot be less than 0", ex.Message);
        }
        
        [Fact]
        public void Create_CardIdLessThan0_ThrowsException()
        {
            var attempt = new Attempt
            {
                UserId = 1,
                CardId = -1,
                WasCorrect = true,
                Date = new DateTime(2021, 11, 28)
            };

            var ex = Assert.Throws<InvalidDataException>(() => _service.Create(attempt));
            Assert.Equal("cardId cannot be less than 0", ex.Message);
        }
        
        [Fact]
        public void Create_DateNull_ThrowsException()
        {
            var attempt = new Attempt
            {
                UserId = 1,
                CardId = 1,
                WasCorrect = true,
                Date = new DateTime()
            };

            var ex = Assert.Throws<InvalidDataException>(() => _service.Create(attempt));
            Assert.Equal("date must be specified", ex.Message);
        }
        
    }
}