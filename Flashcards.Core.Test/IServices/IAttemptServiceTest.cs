using System.Collections.Generic;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Moq;
using Xunit;

namespace Flashcards.Core.Test.IServices
{
    public class IAttemptServiceTest
    {
        [Fact]
        public void IAttemptService_IsAvailable()
        {
            var service = new Mock<IAttemptService>().Object;
            Assert.NotNull(service);
        }
        
        [Fact]
        public void GetAttempts_WithParams_ReturnsListOfAttempts()
        {
            var mock = new Mock<IAttemptService>();
            var fakeList = new List<Attempt>();
            var userId = 1;
            var cardId = 1;
            var quantity = 3;
            mock.Setup(s => s.Get(userId, cardId, quantity))
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.Get(userId, cardId, quantity));
        }
        
        [Fact]
        public void Create_WithParams_ReturnsCreated()
        {
            var mock = new Mock<IAttemptService>();
            var fakeAttempt = new Attempt();
            var attempt = new Attempt();
            mock.Setup(s => s.Create(attempt))
                .Returns(fakeAttempt);
            var service = mock.Object;
            Assert.Equal(fakeAttempt, service.Create(attempt));
        }
    }
}