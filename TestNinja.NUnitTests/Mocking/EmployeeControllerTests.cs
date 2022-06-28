using System.Net.NetworkInformation;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class EmployeeControllerTests
{
    private Mock<IEmployeeRepository> _mockRepository;
    private EmployeeController _employeeController;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _employeeController = new EmployeeController(_mockRepository.Object);
    }

    [Test]
    public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
    {
        _employeeController.DeleteEmployee(1);
        _mockRepository.VerifyAll();
    }
}