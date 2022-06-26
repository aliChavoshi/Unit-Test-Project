using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> _fileReader;
    private VideoService _videoService;

    [SetUp]
    public void SetUp()
    {
        _fileReader = new Mock<IFileReader>();
        _videoService = new VideoService(_fileReader.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyPath_ReturnError()
    {
        const string path = "video.txt";
        _fileReader.Setup(x => x.Read(path)).Returns("");  //empty
        var result = _videoService.ReadVideoTitle(path);
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}