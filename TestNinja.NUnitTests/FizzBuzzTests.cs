using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests;

[TestFixture]
public class FizzBuzzTests
{
    [Test]
    [TestCase(15, "FizzBuzz")]
    [TestCase(10, "Buzz")]
    [TestCase(6, "Fizz")]
    [TestCase(1, "1")]
    public void GetOutput_WhenCalled_ReturnValue(int number, string expectedValue)
    {
        var result = FizzBuzz.GetOutput(number);
        Assert.That(result, Is.EqualTo(expectedValue));
    }
}