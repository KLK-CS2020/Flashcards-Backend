using System.Collections.Generic;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Moq;
using Xunit;

namespace Flashcards.Core.Test.IServices
{
    public class ICardServiceTest
    {
        [Fact]
        public void ICardService_IsAvailabe()
        {
            var service = new Mock<ICardService>().Object;
            Assert.NotNull(service);
        }
        
        #region Delete
        [Fact]
        public void DeleteCard_WithParams_ReturnsDeletedProduct()
        {
            var serviceMoc = new Mock<ICardService>();
            var CardId = 1;
            serviceMoc.Setup(c => c.Delete(CardId)).Returns(new Card());
            Assert.NotNull(serviceMoc.Object);

        }
        #endregion

        #region Update

        [Fact]
        public void Update_ReturnsUpdated()
        {
            var card = new Card
            {
                Id = 1,Question ="Pig", Answer = "Malac",Correctness = 0
            };
            var serviceMoc = new Mock<ICardService>();

            serviceMoc.Setup(x => x.Update(card)).Returns(card);
            var result = serviceMoc.Object;
            Assert.Equal(card, result.Update(card));
        }

        #endregion

        #region ReadAllCardsByDeckId

        [Fact]
        public void ReadAllCardsByDeckId_ReturnsUpdated()
        {
            var deckId = new int();
            var mock = new Mock<ICardService>(); 
            mock.Setup(s => s.GetAllCardsByDeckId(deckId))
                .Returns(new List<Card>()); 
            var service = mock.Object; 
            Assert.Equal(new List<Card>(), service.GetAllCardsByDeckId(deckId));
        }
        

        #endregion
    }
}