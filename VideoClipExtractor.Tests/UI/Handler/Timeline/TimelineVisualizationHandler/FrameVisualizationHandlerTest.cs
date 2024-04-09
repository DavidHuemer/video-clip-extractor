using System.Collections.ObjectModel;
using Moq;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicatorsUpdateListener;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicatorsFactory;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.TimelineVisualizationHandler;

[TestFixture]
[TestOf(typeof(FrameVisualizationHandler))]
public class FrameVisualizationHandlerTest : BaseViewModelTest
{
    private Mock<ITimelineIndicatorsUpdateListener> _timelineIndicatorsUpdateListenerMock = null!;
    private Mock<ITimelineIndicatorsFactory> _timelineIndicatorsFactoryMock = null!;
    private Mock<ITimelineControlViewModel> _timelineControlViewModelMock = null!;
    private Mock<ITimelineNavigationViewModel> _timelineNavigationViewModelMock = null!;
    private FrameVisualizationHandler _frameVisualizationHandler = null!;

    public override void Setup()
    {
        base.Setup();
        _timelineIndicatorsUpdateListenerMock =
            DependencyMock.CreateMockDependency<ITimelineIndicatorsUpdateListener>();
        _timelineIndicatorsFactoryMock = DependencyMock.CreateMockDependency<ITimelineIndicatorsFactory>();
        _timelineControlViewModelMock = new Mock<ITimelineControlViewModel>();
        _timelineNavigationViewModelMock = new Mock<ITimelineNavigationViewModel>();
        _timelineControlViewModelMock.Setup(x => x.TimelineNavigationViewModel)
            .Returns(_timelineNavigationViewModelMock.Object);

        _frameVisualizationHandler = new FrameVisualizationHandler(DependencyMock.Object);
    }

    [Test]
    public void SetupSetsUpTimelineIndicatorsUpdateListener()
    {
        // Arrange
        _timelineIndicatorsFactoryMock
            .Setup(x => x.GetTimelineIndicators(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<double>()))
            .Returns(new List<VideoPosition>());

        _timelineIndicatorsFactoryMock.Setup(x =>
                x.GetTimelineSupporters(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<double>()))
            .Returns(new List<VideoPosition>());

        // Act
        _frameVisualizationHandler.Setup(_timelineControlViewModelMock.Object);

        // Assert
        _timelineIndicatorsUpdateListenerMock.Verify(x => x.Setup(_timelineControlViewModelMock.Object), Times.Once);
    }

    [Test]
    public void UpdateUpdatesTimelineIndicators()
    {
        // Arrange
        var timelineIndicators = new List<VideoPosition>
        {
            new VideoPosition(0, 50),
            new VideoPosition(1, 50),
            new VideoPosition(2, 50)
        };
        var timelineSupporters = new List<VideoPosition>
        {
            new VideoPosition(3, 50),
            new VideoPosition(4, 50),
            new VideoPosition(5, 50)
        };
        _timelineIndicatorsFactoryMock
            .Setup(x => x.GetTimelineIndicators(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<double>()))
            .Returns(timelineIndicators);

        _timelineIndicatorsFactoryMock.Setup(x =>
                x.GetTimelineSupporters(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<double>()))
            .Returns(timelineSupporters);

        // Act
        _frameVisualizationHandler.Setup(_timelineControlViewModelMock.Object);

        _timelineIndicatorsUpdateListenerMock.Raise(x => x.TimelineIndicatorsUpdateRequested += null!, EventArgs.Empty);

        // Assert
        _timelineControlViewModelMock.VerifySet(x => x.TimelineIndicators = It.Is<ObservableCollection<VideoPosition>>(
            y => y.SequenceEqual(timelineIndicators)), Times.Exactly(2));
    }
}