using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger _logger;

    [SetUp]
    public void SetUp()
    {
        _logger = new ErrorLogger();
    }

    [Test]
    [TestCase("a")]
    public void Log_WhenCalled_SetMessageInLastError(string message)
    {
        _logger.Log(message);
        Assert.That(_logger.LastError, Is.EqualTo(message));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_InvalidError_ThrowArgumentNullException(string error)
    {
        Assert.Throws<ArgumentNullException>(() => _logger.Log(error));
    }

    [Test]
    public void Log_ValidError_RaiseEventHandler()
    {
        var id = Guid.Empty;
        //first describe event
        _logger.ErrorLogged += (_, guid) => { id = guid; };
        //second Call method Log
        _logger.Log("a");
        Assert.That(id, Is.Not.Empty);
    }
}