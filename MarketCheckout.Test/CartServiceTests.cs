using MarketCheckout.Application.Request;
using MarketCheckout.Application.Services;
using MarketCheckout.Application.Services.Interface;
using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckoutApi.Domain.Entities;
using Moq;

namespace MarketCheckout.Test
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IBaseRepository<ItemCart>> _itemCartRepositoryMock;
        private readonly Mock<IBaseRepository<Cart>> _cartRepositoryForItemMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IItemCartService> _itemCartServiceMock;
        private readonly CartService _cartService;
        private readonly ItemCartService _itemCartService;

        public CartServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _itemCartRepositoryMock = new Mock<IBaseRepository<ItemCart>>();
            _cartRepositoryForItemMock = new Mock<IBaseRepository<Cart>>();
            _productServiceMock = new Mock<IProductService>();
            _itemCartServiceMock = new Mock<IItemCartService>();

            _cartService = new CartService(
                _cartRepositoryMock.Object,
                _productServiceMock.Object,
                _itemCartServiceMock.Object
            );

            _itemCartService = new ItemCartService(
                _itemCartRepositoryMock.Object,
                _cartRepositoryForItemMock.Object,
                _productServiceMock.Object
            );
        }        

        [Fact]
        public async Task ProcessCart_ShouldThrowArgumentException_WhenItemHasInvalidProductId()
        {
            //monta um CartRequest com um item inválido (ProductId = 0)
            var cartRequest = new CartRequest
            {
                BuyerName = "João Silva",
                BuyerCpf = "12345678901234",
                CreatedBy = "sistema",
                Items = new List<ItemCartRequest>
                {
                    new ItemCartRequest { ProductId = 0, Quantity = 2 }
                }
            };

            //espera que o serviço lance ArgumentException
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _cartService.ProcessCart(cartRequest, CancellationToken.None)
            );

            Assert.Contains("Cart items cannot be empty", exception.Message);
        }

        [Fact]
        public async Task AddItemCartAsync_ShouldThrowOperationCanceledException_WhenCartNotFound()
        {
            //repositório retorna null para qualquer ID de carrinho
            _cartRepositoryForItemMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cart?)null);

            var items = new List<ItemCartRequest>
            {
                new ItemCartRequest { ProductId = 1, Quantity = 1 }
            };

            //espera que o serviço lance OperationCanceledException
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _itemCartService.AddItemCartAsync(99, items, CancellationToken.None)
            );
        }
    }
}