namespace TestNinja.NUnitTests.Fundamentals;

[TestFixture]
public class StackTests
{
    private TestNinja.Fundamentals.Stack<int> _stack;

    [SetUp]
    public void SetUp()
    {
        _stack = new TestNinja.Fundamentals.Stack<int>();
    }

    [Test]
    public void Push_ItemIsNull_ThrowArgumentNullException()
    {
        var stack = new TestNinja.Fundamentals.Stack<string>();
        Assert.Throws<ArgumentNullException>(() => stack.Push(null));
    }

    [Test]
    public void Push_WhenCalled_AddOneItemToList()
    {
        _stack.Push(1);
        Assert.That(actual: 1, expression: Is.EqualTo(_stack.Count));
        _stack.Push(2);
        Assert.That(actual: 2, expression: Is.EqualTo(_stack.Count));
    }

    [Test] //for property
    public void Count_EmptyStack_ReturnZero()
    {
        Assert.Zero(_stack.Count);
    }

    [Test]
    public void Pop_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => { _stack.Pop(); });
    }

    [Test]
    public void Pop_WhenCall_PopItemFromList()
    {
        _stack.Push(1);
        _stack.Push(2);
        Assert.That(2, Is.EqualTo(_stack.Pop()));
        Assert.That(actual: 1, expression: Is.EqualTo(_stack.Count));
        Assert.That(1, Is.EqualTo(_stack.Pop()));
        Assert.Zero(_stack.Count);
    }

    [Test]
    public void Peek_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => { _stack.Peek(); });
    }

    [Test]
    public void Peek_WhenCall_ReturnLastItem()
    {
        _stack.Push(1);
        _stack.Push(3);
        _stack.Push(2);
        Assert.That(2, Is.EqualTo(_stack.Peek()));
    }
}