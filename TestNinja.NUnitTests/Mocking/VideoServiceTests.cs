using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> _moqFileReader;
    private Mock<IVideoRepository> _moqVideoRepository;
    private VideoService _videoService;

    [SetUp]
    public void SetUp()
    {
        _moqFileReader = new Mock<IFileReader>();
        _moqVideoRepository = new Mock<IVideoRepository>();
        _videoService = new VideoService(_moqFileReader.Object, _moqVideoRepository.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyPath_ReturnError()
    {
        const string path = "video.txt";
        _moqFileReader.Setup(x => x.Read(path)).Returns(""); //empty
        var result = _videoService.ReadVideoTitle(path);
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
    {
        _moqVideoRepository.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>());
        var result = _videoService.GetUnprocessedVideosAsCsv();
        Assert.That(result, Is.Empty);
    }

    [Test]
    [TestCase("1,2,3")]
    public void GetUnprocessedVideosAsCsv_AFewUnProcessedVideos_ReturnStringWithJoinIds(string expectedResult)
    {
        _moqVideoRepository.Setup(x => x.GetUnprocessedVideos())
            .Returns(new List<Video>
            {
                new() {Id = 1, IsProcessed = false},
                new() {Id = 2, IsProcessed = false},
                new() {Id = 3, IsProcessed = false},
            });
        var result = _videoService.GetUnprocessedVideosAsCsv();
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}