using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly string _setupDestinationFile;
        private readonly IFileDownloader _fileDownloader;

        public InstallerHelper(string setupDestinationFile, IFileDownloader fileDownloader)
        {
            _setupDestinationFile = setupDestinationFile;
            _fileDownloader = fileDownloader;
        }

        //=>return true
        //=>return false
        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFileFromWeb($"http://example.com/{customerName}/{installerName}",
                    _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}