using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests;

[TestFixture]
public class ErrorLoggerTests
{
    [Test]
    [TestCase("a")]
    public void Log_WhenCalled_SetMessageInLastError(string message)
    {
        var logger = new ErrorLogger();
        logger.Log(message);
        Assert.That(logger.LastError, Is.EqualTo(message));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_InvalidError_ThrowArgumentNullException(string error)
    {
        var logger = new ErrorLogger();
        Assert.Throws<ArgumentNullException>(() => logger.Log(error));
    }
}