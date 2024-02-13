using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using BaseUI.Events;
using Moq;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Handler.Timeline;
using VideoClipExtractor.UI.Handler.Timeline.Events.ZoomEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events;

public class TimelineZoomEventHandlerTests : BaseDependencyTest
{
    private Mock<IFrameworkElement> _timelineControl = null!;
    private Mock<ITimelineNavigationViewModel> _timelineNavigationViewModel = null!;


    private Mock<ITimelinePositionHandler> _timelinePositionHandler = null!;

    private TimelineZoomEventHandler _timelineZoomEventHandler = null!;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _timelineControl = new Mock<IFrameworkElement>();
        var vmProvider = DependencyMock.AddViewModelProvider();
        _timelineNavigationViewModel = vmProvider.CreateViewModelMock<ITimelineNavigationViewModel>();

        _timelinePositionHandler = DependencyMock.CreateMockDependency<ITimelinePositionHandler>();

        _timelineZoomEventHandler =
            new TimelineZoomEventHandler(DependencyMock.Object);
        _timelineZoomEventHandler.Setup(_timelineControl.Object);
    }

    [Test]
    public void ZoomInAtZoomLevel1DoesNothing()
    {
        _timelineNavigationViewModel.SetupProperty(x => x.MovementState);
        _timelineNavigationViewModel.Object.MovementState = MovementState.None;

        _timelineNavigationViewModel.SetupProperty(x => x.ZoomLevel);
        _timelineNavigationViewModel.Object.ZoomLevel = 1;

        Zoom(ZoomDirection.In);
        Assert.That(_timelineNavigationViewModel.Object.ZoomLevel, Is.EqualTo(1));
    }

    [Test]
    public void ZoomOutAtBeginningDoesNotMoveTimeline()
    {
        _timelineNavigationViewModel.SetupProperty(x => x.ZoomLevel);
        _timelineNavigationViewModel.Object.ZoomLevel = 27;

        _timelineNavigationViewModel.SetupProperty(x => x.MovementState);
        _timelineNavigationViewModel.Object.MovementState = MovementState.None;

        _timelinePositionHandler.Setup(x =>
            x.GetCenterPosition(It.IsAny<double>(), It.IsAny<double>())).Returns(500);
        _timelinePositionHandler.Setup(x =>
            x.GetFrameAtPosition(It.IsAny<double>(), It.IsAny<int>())).Returns(3);
        _timelinePositionHandler.Setup(x =>
            x.GetPositionAtFrame(It.IsAny<double>(), It.IsAny<int>())).Returns(440);

        Zoom(ZoomDirection.Out);
        Assert.That(_timelineNavigationViewModel.Object.MovementPosition, Is.EqualTo(0));
    }

    private void Zoom(ZoomDirection direction)
    {
        var delta = direction == ZoomDirection.In ? 120 : -120;
        _timelineControl.Raise(x => x.MouseWheel += null, new MouseWheelEventArgsWrapper(delta));
    }
}