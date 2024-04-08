using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicationsRangeService;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.TimelineVisualizationHandler;

[TestFixture]
[TestOf(typeof(TimelineIndicationsRangeService))]
public class TimelineIndicationsRangeServiceTest
{
    [SetUp]
    public void Setup()
    {
        _timelineIndicationsRangeService = new TimelineIndicationsRangeService();
    }

    private TimelineIndicationsRangeService _timelineIndicationsRangeService = null!;


    [Test]
    [TestCase(0, 10, 5, 0)]
    [TestCase(1, 10, 5, 0)]
    [TestCase(200, 10, 5, 0)]
    [TestCase(201, 10, 5, 0)]
    [TestCase(250, 10, 5, 0)]
    [TestCase(299, 10, 5, 0)]
    [TestCase(300, 10, 5, 5)]
    public void FirstFrameIsReturnedCorrectly(double movement, double frameWidth, int stepSize, int expected)
    {
        var firstFrame = _timelineIndicationsRangeService.GetFirstFrame(movement, frameWidth, stepSize);

        Assert.That(firstFrame, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(5, 10, 1000, 23)]
    public void GetLastFrameReturnedCorrectly(int indicationStepSize, double frameWidth, double width, int expected)
    {
        var lastFrame = _timelineIndicationsRangeService.GetLastFrame(indicationStepSize, frameWidth, width);

        Assert.That(lastFrame, Is.EqualTo(expected));
    }
}