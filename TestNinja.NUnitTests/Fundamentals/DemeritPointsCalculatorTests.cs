using NUnit.Framework.Constraints;
using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    [Test]
    [TestCase(-10)]
    [TestCase(350)] //-10<x<0
    public void CalculateDemeritPoints_SpeedIsNegativeAndGraterThanMaxSpeed_ThrowException(int speed)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => { DemeritPointsCalculator.CalculateDemeritPoints(speed); });
    }

    [Test]
    [TestCase(50)]
    [TestCase(60)]
    [TestCase(65)]
    [TestCase(1)] //0<=x<=65
    public void CalculateDemeritPoints_SpeedBetweenZeroAndSpeedLimit_ReturnZero(int speed)
    {
        Assert.Zero(DemeritPointsCalculator.CalculateDemeritPoints(speed));
    }

    [Test]
    [TestCase(100, 7)]
    [TestCase(300, 47)]
    [TestCase(150, 17)] //66<x<300
    public void CalculateDemeritPoints_WhenCalled_ReturnKmPerDemeritPoint(int speed, int expectedResult)
    {
        var result = DemeritPointsCalculator.CalculateDemeritPoints(speed);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}