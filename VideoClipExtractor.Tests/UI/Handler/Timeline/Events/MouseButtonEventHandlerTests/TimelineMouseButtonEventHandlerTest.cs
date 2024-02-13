using System.Windows;
using System.Windows.Input;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Events;
using Moq;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MouseButtonEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events.MouseButtonEventHandlerTests;

[TestFixture]
[TestOf(typeof(TimelineMouseButtonEventHandler))]
public class TimelineMouseButtonEventHandlerTest : BaseDependencyTest
{
    private Mock<ITimelineNavigationViewModel> _timelineNavigationVm = null!;
    private Mock<ITimelineMarkerEventHandler> _timelineMarkerEventHandler = null!;
    private Mock<ITimelineMovementEventHandler> _timelineMovementEventHandler = null!;
    private Mock<IFrameworkElement> _timelineControl = null!;

    private TimelineMouseButtonEventHandler _mouseButtonEventHandler = null!;

    public override void Setup()
    {
        base.Setup();
        var viewModelProvider = DependencyMock.AddViewModelProvider();
        _timelineNavigationVm = viewModelProvider.CreateViewModelMock<ITimelineNavigationViewModel>();

        _timelineMarkerEventHandler = DependencyMock.CreateMockDependency<ITimelineMarkerEventHandler>();
        _timelineMovementEventHandler = DependencyMock.CreateMockDependency<ITimelineMovementEventHandler>();

        _timelineControl = new Mock<IFrameworkElement>();
        _mouseButtonEventHandler = new TimelineMouseButtonEventHandler(DependencyMock.Object);
        _mouseButtonEventHandler.Setup(_timelineControl.Object);
    }

    [Test]
    public void MouseDownWhenMovementStateNotNoneReleasesEventHandler()
    {
        _timelineNavigationVm.SetupGet(x => x.MovementState)
            .Returns(MovementState.MarkerMovement);

        MouseDown(MouseButton.Left);

        _timelineMovementEventHandler.Verify(x => x.StopMovement(), Times.Once);
        _timelineControl.Verify(x => x.ReleaseMouseCapture(), Times.Once);
        _timelineNavigationVm.VerifySet(x => x.MovementState = MovementState.None, Times.Once);
    }

    [Test]
    public void MouseDownWhenMovementStateNoneCapturesMouse()
    {
        _timelineNavigationVm.SetupGet(x => x.MovementState)
            .Returns(MovementState.None);

        MouseDown(MouseButton.Left);

        _timelineControl.Verify(x => x.CaptureMouse(), Times.Once);
    }

    [Test]
    public void MouseDownWhenMovementStateNoneHandlesMarkerMouseButton()
    {
        _timelineNavigationVm.SetupGet(x => x.MovementState)
            .Returns(MovementState.None);

        MouseDown(MouseButton.Right);

        _timelineMarkerEventHandler.Verify(x => x.StartMarkerMovement(It.IsAny<Point>()), Times.Once);
        _timelineNavigationVm.VerifySet(x => x.MovementState = MovementState.MarkerMovement, Times.Once);
    }

    [Test]
    public void MouseDownWhenMovementStateNoneHandlesMovementMouseButton()
    {
        _timelineNavigationVm.SetupGet(x => x.MovementState)
            .Returns(MovementState.None);

        MouseDown(MouseButton.Middle);

        _timelineMovementEventHandler.Verify(x => x.StartMovement(It.IsAny<Point>()), Times.Once);
        _timelineNavigationVm.VerifySet(x => x.MovementState = MovementState.TimelineMovement, Times.Once);
    }

    [Test]
    public void MouseUpReleasesEventHandler()
    {
        MouseUp(MouseButton.Left);

        _timelineMovementEventHandler.Verify(x => x.StopMovement(), Times.Once);
        _timelineControl.Verify(x => x.ReleaseMouseCapture(), Times.Once);
        _timelineNavigationVm.VerifySet(x => x.MovementState = MovementState.None, Times.Once);
    }

    private void MouseDown(MouseButton button)
    {
        _timelineControl.Raise(x => x.MouseDown += null, _timelineControl.Object,
            new MouseButtonEventArgsWrapper(button, (_) => new Point()));
    }

    private void MouseUp(MouseButton button)
    {
        _timelineControl.Raise(x => x.MouseUp += null, _timelineControl.Object,
            new MouseButtonEventArgsWrapper(button, (_) => new Point()));
    }
}