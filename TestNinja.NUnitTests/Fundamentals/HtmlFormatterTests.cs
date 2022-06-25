using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals;

[TestFixture]
public class HtmlFormatterTests
{
    [Test]
    [TestCase("abc")]
    public void FormatAsBold_WhenCalled_ShouldBeString(string content)
    {
        var formatter = new HtmlFormatter();
        var result = formatter.FormatAsBold(content);
        //special
        Assert.That(result, Is.EqualTo($"<strong>{content}</strong>").IgnoreCase);
        //general
        Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
        Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
        Assert.That(result, Does.Contain(content).IgnoreCase);
    }
}