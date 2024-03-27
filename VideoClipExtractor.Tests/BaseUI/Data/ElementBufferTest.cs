using BaseUI.Data;

namespace VideoClipExtractor.Tests.BaseUI.Data;

[TestFixture]
[TestOf(typeof(ElementBuffer<>))]
public class ElementBufferTest
{
    [Test]
    [TestCase(10, 5, 0)]
    [TestCase(10, 10, 1)]
    [TestCase(10, 11, 1)]
    [TestCase(10, 20, 2)]
    [TestCase(10, 21, 2)]
    public void FlushIsCalledExactTimes(int bufferSize, int nrAdded, int expected)
    {
        var flushCounter = 0;
        var buffer = new ElementBuffer<int>(bufferSize, (_) => { flushCounter++; });

        for (var i = 0; i < nrAdded; i++)
        {
            buffer.Add(i);
        }

        Assert.That(flushCounter, Is.EqualTo(expected));
    }

    [Test]
    public void FlushIsCalledWithCorrectElements()
    {
        var buffer = new ElementBuffer<int>(3,
            (elements) => { Assert.That(elements, Is.EquivalentTo(new[] { 0, 1, 2 })); });

        buffer.Add(0);
        buffer.Add(1);
        buffer.Add(2);
    }

    [Test]
    public void FlushIsCalledWithCorrectElementsWhenFlushIsCalled()
    {
        var buffer =
            new ElementBuffer<int>(3, (elements) => { Assert.That(elements, Is.EquivalentTo(new[] { 0, 1 })); });

        buffer.Add(0);
        buffer.Add(1);
        buffer.Flush();
    }
}