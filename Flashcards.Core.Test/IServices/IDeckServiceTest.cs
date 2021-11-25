using System.Collections.Generic;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Moq;
using Xunit;

namespace Flashcards.Core.Test.IServices
{
    public class IDeckServiceTest
    {
        [Fact]
        public void IDeckService_IsAvailable()
        {
            var service = new Mock<IDeckService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetPublicDecks_WithNoParam_ReturnsListOfAllDecks()
        {
            var mock = new Mock<IDeckService>();
            var fakeList = new List<Deck>();
            mock.Setup(s => s.GetAllPublic())
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.GetAllPublic());
        }
    }
}