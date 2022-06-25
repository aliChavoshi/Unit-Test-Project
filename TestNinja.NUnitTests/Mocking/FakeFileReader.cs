using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

public class FakeFileReader : IFileReader
{
    public string Read(string path)
    {
        return "";
    }
}