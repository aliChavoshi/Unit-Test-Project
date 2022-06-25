using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    [Test]
    public void ReadVideoTitle_EmptyPath_ReturnError()
    {
        var service = new VideoService();
        var result = service.ReadVideoTitle(new FakeFileReader());
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}