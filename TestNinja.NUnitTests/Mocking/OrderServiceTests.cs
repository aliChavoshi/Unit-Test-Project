using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IStorage> _mockStorage;
    private OrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        _mockStorage = new Mock<IStorage>();
        _orderService = new OrderService(_mockStorage.Object);
    }

    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var order = new Order();
        _orderService.PlaceOrder(order);
        _mockStorage.Verify(s => s.Store(order));
        _mockStorage.VerifyAll();
    }
}