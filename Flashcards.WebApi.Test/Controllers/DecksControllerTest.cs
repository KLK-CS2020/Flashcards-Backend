using System.IO;
using System.Linq;
using System.Reflection;
using Flashcards_backend.Core.IServices;
using Flashcards.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Flashcards.Presentation_Test.Controllers
{
    public class DecksControllerTest
    {
        
        private readonly DecksController _controller;
        private readonly Mock<IDeckService> _mockService;

        public DecksControllerTest()
        {
           _mockService = new Mock<IDeckService>();
           _controller = new DecksController(_mockService.Object);
        }
        
        #region controller initialization
        
        [Fact]
        public void DecksController_IsOfTypeControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }

        [Fact]
        public void DecksController_UsesApiControllerAttribute()
        {
            var typeInfo = typeof(DecksController).GetTypeInfo();
            var attribute = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attribute);
        }
        
        [Fact]
        public void DecksController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            var typeInfo = typeof(DecksController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType()
                .Name.Equals("RouteAttribute"));
            
            var routeAttr = attr as RouteAttribute;
            Assert.Equal("api/[controller]", routeAttr.Template);
        }
        
        [Fact]
        public void DecksController_UsesRouteAttribute()
        {
            var typeInfo = typeof(DecksController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType()
                .Name.Equals("RouteAttribute"));
            
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void DecksController_WithNullProductService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new DecksController(null));
        }
        
        [Fact]
        public void DecksController_WithNullProductService_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new DecksController(null));
            Assert.Equal("Deck service cannot be null", exception.Message);
        }
        
        #endregion
        
    }
}