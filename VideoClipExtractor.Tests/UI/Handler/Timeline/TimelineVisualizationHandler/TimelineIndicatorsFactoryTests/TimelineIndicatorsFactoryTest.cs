using FFMpeg.Wrapper.Data;
using Moq;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.Handler.Timeline;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicationsVisibility;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicationsRangeService;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicatorsFactory;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicatorsFactoryTests;

[TestFixture]
[TestOf(typeof(TimelineIndicatorsFactory))]
public class TimelineIndicatorsFactoryTest : BaseDependencyTest
{
    private Mock<IVideoManager> _videoManager = null!;
    private Mock<ITimelineIndicationsRangeService> _timelineIndicationsRange = null!;
    private Mock<ITimeIndicationsVisibility> _timeIndicationsVisibility = null!;
    private Mock<ITimelineFrameWidthHandler> _timelineFrameWidthHandler = null!;
    private TimelineIndicatorsFactory _timelineIndicatorsFactory = null!;

    public override void Setup()
    {
        base.Setup();
        _videoManager = DependencyMock.CreateMockDependency<IVideoManager>();
        _timelineIndicationsRange = DependencyMock.CreateMockDependency<ITimelineIndicationsRangeService>();
        _timeIndicationsVisibility = DependencyMock.CreateMockDependency<ITimeIndicationsVisibility>();
        _timelineFrameWidthHandler = DependencyMock.CreateMockDependency<ITimelineFrameWidthHandler>();
        _timelineIndicatorsFactory = new TimelineIndicatorsFactory(DependencyMock.Object);
    }

    [Test]
    public void GetTimelineIndicatorsReturnsCorrectAmount()
    {
        var expected = 30;

        _timeIndicationsVisibility.Setup(x => x.GetIndicationStep(It.IsAny<int>())).Returns(20);
        _timelineFrameWidthHandler.Setup(x => x.GetFrameWidth(It.IsAny<int>())).Returns(10);
        _timelineIndicationsRange.Setup(x => x.GetFirstFrame(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()))
            .Returns(0);

        _timelineIndicationsRange.Setup(x => x.GetLastFrame(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>()))
            .Returns(30);

        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = new VideoInfo(TimeSpan.Zero, 50);
        _videoManager.SetupGet(x => x.Video).Returns(video);


        var timelineIndicators = _timelineIndicatorsFactory.GetTimelineIndicators(0, 1, 100);

        Assert.That(timelineIndicators, Has.Count.EqualTo(expected));
    }

    [Test]
    public void GetTimelineSupportersReturnsCorrectAmount()
    {
        var expected = 120;

        _timeIndicationsVisibility.Setup(x => x.GetIndicationStep(It.IsAny<int>())).Returns(20);
        _timelineFrameWidthHandler.Setup(x => x.GetFrameWidth(It.IsAny<int>())).Returns(10);
        _timelineIndicationsRange.Setup(x => x.GetFirstFrame(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()))
            .Returns(0);

        _timelineIndicationsRange.Setup(x => x.GetLastFrame(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>()))
            .Returns(30);

        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = new VideoInfo(TimeSpan.Zero, 50);
        _videoManager.SetupGet(x => x.Video).Returns(video);

        var timelineSupporters = _timelineIndicatorsFactory.GetTimelineSupporters(0, 1, 100);

        Assert.That(timelineSupporters, Has.Count.EqualTo(expected));
    }
}