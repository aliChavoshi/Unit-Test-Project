using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
internal class ProductTests
{
    [Test]
    [TestCase(10000, 7000)]
    [TestCase(100, 70)]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount(float listPrice, float expectedResult)
    {
        //arrange
        var product = new Product
        {
            ListPrice = listPrice
        };
        var customer = new Customer {IsGold = true};
        //act
        var result = product.GetPrice(customer);
        //assert
        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(10000, 7000)]
    [TestCase(100, 70)]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount2(float listPrice, float expectedResult)
    {
        //arrange
        var moqCustomer = new Moq.Mock<ICustomer>();
        moqCustomer.Setup(x => x.IsGold).Returns(true);
        var product = new Product
        {
            ListPrice = listPrice
        };
        //act
        var result = product.GetPrice(moqCustomer.Object);
        //assert
        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}