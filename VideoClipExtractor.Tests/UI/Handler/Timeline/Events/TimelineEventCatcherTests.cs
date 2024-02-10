using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using BaseUI.Events;
using Moq;
using VideoClipExtractor.UI.Handler.Timeline.Events;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events;

public class TimelineEventCatcherTests
{
    private Mock<IFrameworkElement> _timelineControl = null!;

    private TimelineEventCatcher _timelineEventCatcher = null!;
    private Mock<ITimelineEventHandler> _timelineEventHandler = null!;
    private TimelineControlViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _timelineControl = new Mock<IFrameworkElement>();
        _viewModel = new TimelineControlViewModel();
        _timelineEventHandler = new Mock<ITimelineEventHandler>();

        _timelineControl.Setup(x => x.ActualWidth).Returns(1000);
        _timelineEventCatcher =
            new TimelineEventCatcher(_timelineControl.Object, _viewModel, _timelineEventHandler.Object);
    }

    [Test]
    public void ActualWidthIsSet()
    {
        Assert.That(_viewModel.TimelineNavigationViewModel.TimelineControlWidth, Is.EqualTo(1000));
    }

    [Test]
    public void ActualWidthIsUpdated()
    {
        _timelineControl.Setup(x => x.ActualWidth).Returns(2000);
        _timelineControl.Raise(x => x.SizeChanged += null!, EventArgs.Empty);
        Assert.That(_viewModel.TimelineNavigationViewModel.TimelineControlWidth, Is.EqualTo(2000));
    }

    [Test]
    [TestCase(120, ZoomDirection.In)]
    [TestCase(-120, ZoomDirection.Out)]
    public void MouseWheelZooms(int zoomDelta, ZoomDirection direction)
    {
        _timelineControl.Raise(x => x.MouseWheel += null!, new MouseWheelEventArgsWrapper(zoomDelta));
        _timelineEventHandler.Verify(x => x.Zoom(direction), Times.Once);
    }
}