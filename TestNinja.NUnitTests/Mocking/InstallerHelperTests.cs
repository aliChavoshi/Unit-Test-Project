using System.Net;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class InstallerHelperTests
{
    private Mock<IFileDownloader> _mockFile;
    private InstallerHelper _installerHelper;

    [SetUp]
    public void SetUp()
    {
        _mockFile = new Mock<IFileDownloader>();
        _installerHelper = new InstallerHelper(null, _mockFile.Object);
    }


    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        _mockFile.Setup(x => x.DownloadFileFromWeb(It.IsAny<string>(), null))
            .Throws<WebException>();
        var result = _installerHelper.DownloadInstaller("customer", "installer");
        Assert.IsFalse(result);
    }


    [Test]
    public void DownloadInstaller_DownloadSuccess_ReturnTrue()
    {
        var result = _installerHelper.DownloadInstaller("customer", "installer");
        Assert.IsTrue(result);
    }
}