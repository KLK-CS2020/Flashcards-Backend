﻿using System.Collections.Generic;
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
        public void Create_ReturnsCreatedProductWithId()
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
        
        #region Delete
        [Fact]
        public void ProductService_Delete_ParameterProduct_ReturnProduct()
        {
            // Arrange
            var product = new Card
            {
                Id = 1,
                Question = "Pig?",
                Answer = "No!!!",
                Correctness = 0
            };
            
            _mock.Setup(r => r.Delete(product.Id))
                .Returns(product);
            // Act
            var actual = _service.Delete(product.Id);
            // Assert
            Assert.Equal(product,actual);
        }
        
        [Fact]
        public void DeleteProduct_WithParams_CallsProductRepositoryOnce()
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
        public void GetAllProducts_CallsProductRepositoriesFindAll_ExactlyOnce()
        {
            int deckId = 1;
            
            _service.GetAllCardsByDeckId(deckId);
            _mock.Verify(r=>r.ReadAllCardsByDeckId(deckId), Times.Once);
        }
        

        #endregion
    }
}