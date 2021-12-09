using System.Collections.Generic;
using Flashcards_backend.Core.Filtering;
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
        public void GetPublicDecks_ReturnsListOfAllDecks()
        {
            var mock = new Mock<IDeckService>();
            var fakeList = new List<Deck>();
            string search = "";
            Filter filter = new Filter();
            mock.Setup(s => s.GetAllPublic(search, filter))
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.GetAllPublic(search, filter));
        }
        
    }
}