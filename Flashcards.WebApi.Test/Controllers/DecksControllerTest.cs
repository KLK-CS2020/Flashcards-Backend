namespace Flashcards.Presentation_Test.Controllers
{
    public class DecksControllerTest
    {
        /*
        private readonly DecksController _controller;
        private readonly Mock<IDeckService> _mockService;

        public DecksControllerTest()
        {
           _mockService = new Mock<IProductService>();
           _controller = new ProductController(_mockService.Object);
        }
        
        #region controller initialization
        
        [Fact]
        public void ProductController_IsOfTypeControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }

        [Fact]
        public void ProductController_UsesApiControllerAttribute()
        {
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attribute = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attribute);
        }
        
        [Fact]
        public void ProductController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType()
                .Name.Equals("RouteAttribute"));
            //Assert
            var routeAttr = attr as RouteAttribute;
            Assert.Equal("api/[Controller]", routeAttr.Template);
        }
        
        [Fact]
        public void ProductController_UsesRouteAttribute()
        {
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType()
                .Name.Equals("RouteAttribute"));
            //Assert
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void ProductController_WithNullProductService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
        }
        
        [Fact]
        public void ProductController_WithNullProductService_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
            Assert.Equal("Product service cannot be null", exception.Message);
        }
        
        #endregion
        */
    }
}