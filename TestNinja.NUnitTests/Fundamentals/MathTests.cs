using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.NUnitTests.Fundamentals;

[TestFixture]
public class MathTests
{
    private Math _math;

    [SetUp]
    public void SetUp()
    {
        _math = new Math();
    }


    [Test]
    // [Ignore("todo after")]
    public void Add_WhenCalled_ReturnTheSumOfArguments()
    {
        //act
        var result = _math.Add(6, 5);
        //assert
        Assert.That(result, Is.EqualTo(11));
    }

    [Test]
    [TestCase(1, 2, 2)]
    [TestCase(2, 1, 2)]
    [TestCase(1, 1, 1)]
    public void Max_WhenCalled_ReturnGreatestArg(int a, int b, int expectedResult)
    {
        //act
        var result = _math.Max(a, b);
        //assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersToLimit()
    {
        var result = _math.GetOddNumbers(5);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result, Does.Contain(1));
        Assert.That(result, Does.Contain(3));
        Assert.That(result, Does.Contain(5));
        Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        Assert.That(result, Is.Ordered);
        Assert.That(result, Is.Unique);
    }
}