using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.NUnitTests;

[TestFixture]
public class MathTests
{
    [Test]
    public void Add_WhenCalled_ReturnTheSumOfArguments()
    {
        //arrange
        var math = new Math();
        //act
        var result = math.Add(6, 5);
        //assert
        Assert.That(result, Is.EqualTo(11));
    }

    [Test]
    public void Max_FirstArgGraterSecond_ReturnFirstArg()
    {
        //arrange
        var math = new Math();
        //act
        var result = math.Max(2, 1);
        //assert
        Assert.That(result,Is.EqualTo(2));
    }

    [Test]
    public void Max_SecondArgGraterSecond_ReturnSecondArg()
    {
        //arrange
        var math = new Math();
        //act
        var result = math.Max(1, 2);
        //assert
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void Max_ArgIsEqual_ReturnTheSameArg()
    {
        //arrange
        var math = new Math();
        //act
        var result = math.Max(1, 1);
        //assert
        Assert.That(result, Is.EqualTo(1));
    }
}