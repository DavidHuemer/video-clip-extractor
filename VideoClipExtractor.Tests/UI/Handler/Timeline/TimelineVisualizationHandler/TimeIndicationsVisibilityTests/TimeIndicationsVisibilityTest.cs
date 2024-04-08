using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicationsVisibility;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicationsVisibilityTests;

[TestFixture]
[TestOf(typeof(TimeIndicationsVisibility))]
public class TimeIndicationsVisibilityTest
{
    [SetUp]
    public void Setup()
    {
        _timeIndicationsVisibility = new TimeIndicationsVisibility();
    }

    private TimeIndicationsVisibility _timeIndicationsVisibility = null!;

    [Test]
    [TestCase(0, 5)]
    [TestCase(-1, 5)]
    [TestCase(1, 5)]
    public void GetIndicationStepReturnsCorrectValue(int zoomLevel, int expected)
    {
        var indicationStep = _timeIndicationsVisibility.GetIndicationStep(zoomLevel);
        Assert.That(indicationStep, Is.EqualTo(expected));
    }
}