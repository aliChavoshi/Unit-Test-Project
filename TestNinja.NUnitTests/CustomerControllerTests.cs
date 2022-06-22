using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _customerController;

    [SetUp]
    public void SetUp()
    {
        _customerController = new CustomerController();
    }

    [Test]
    public void GetCustomer_IdIsZero_ReturnNotFound()
    {
        var result = _customerController.GetCustomer(0);
        //just not found type
        Assert.That(result, Is.TypeOf<NotFound>());
        //found type or derivatives of not fount for ex action result
        Assert.That(result, Is.InstanceOf<ActionResult>());
    }

    [Test]
    [TestCase(5)]
    [TestCase(6)]
    public void GetCustomer_IdIsNotZero_ReturnOK(int id)
    {
        var result = _customerController.GetCustomer(id);
        Assert.That(result, Is.TypeOf<Ok>());
        Assert.That(result, Is.InstanceOf<ActionResult>());
    }
}