using BaseUI.Basics.FrameworkElementWrapper;
using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Handler.Timeline.Events;
using VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MouseButtonEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.ZoomEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events;

public class TimelineEventHandlerTests : BaseDependencyTest
{
    private Mock<ITimelineMarkerEventHandler> _markerEventHandler = null!;
    private Mock<ITimelineMouseButtonEventHandler> _mouseButtonEventHandler = null!;
    private Mock<ITimelineMovementEventHandler> _movementEventHandler = null!;
    private Mock<IFrameworkElement> _timelineControl = null!;
    private Mock<ITimelineControlViewModel> _timelineControlViewModel = null!;

    private TimelineEventHandler _timelineEventHandler = null!;

    private Mock<ITimelineNavigationViewModel> _timelineNavigationViewModel = null!;
    private Mock<ITimelineZoomEventHandler> _timelineZoomEventHandler = null!;

    public override void Setup()
    {
        base.Setup();
        _timelineControl = new Mock<IFrameworkElement>();
        _timelineControl.Setup(x => x.ActualWidth).Returns(1000);
        _timelineControlViewModel = new Mock<ITimelineControlViewModel>();
        _timelineControlViewModel.SetupGet(x => x.Provider).Returns(DependencyMock.Object);

        _timelineZoomEventHandler = DependencyMock.CreateMockDependency<ITimelineZoomEventHandler>();
        _mouseButtonEventHandler = DependencyMock.CreateMockDependency<ITimelineMouseButtonEventHandler>();
        _markerEventHandler = DependencyMock.CreateMockDependency<ITimelineMarkerEventHandler>();
        _movementEventHandler = DependencyMock.CreateMockDependency<ITimelineMovementEventHandler>();
        var viewModelProvider = DependencyMock.AddViewModelProvider();
        _timelineNavigationViewModel = viewModelProvider.CreateViewModelMock<ITimelineNavigationViewModel>();
        _timelineEventHandler = new TimelineEventHandler(_timelineControl.Object, _timelineControlViewModel.Object);
    }

    [Test]
    public void ActualWidthIsSet()
    {
        _timelineNavigationViewModel.VerifySet(x => x.TimelineControlWidth = 1000, Times.Once);
    }

    [Test]
    public void EventHandlerAreSetup()
    {
        _timelineZoomEventHandler.Verify(x => x.Setup(_timelineControl.Object), Times.Once);
        _mouseButtonEventHandler.Verify(x => x.Setup(_timelineControl.Object), Times.Once);
        _markerEventHandler.Verify(x => x.Setup(_timelineControl.Object), Times.Once);
        _movementEventHandler.Verify(x => x.Setup(_timelineControl.Object), Times.Once);
    }

    [Test]
    public void ActualWidthIsUpdated()
    {
        _timelineControl.Setup(x => x.ActualWidth).Returns(2000);
        _timelineControl.Raise(x => x.SizeChanged += null!, EventArgs.Empty);
        _timelineNavigationViewModel.VerifySet(x => x.TimelineControlWidth = 2000, Times.Once);
    }
}