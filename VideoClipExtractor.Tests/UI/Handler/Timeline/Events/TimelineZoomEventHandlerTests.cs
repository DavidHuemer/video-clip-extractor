using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using Moq;
using VideoClipExtractor.UI.Handler.Timeline;
using VideoClipExtractor.UI.Handler.Timeline.Events;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events;

public class TimelineZoomEventHandlerTests
{
    private Mock<IFrameworkElement> _timelineControl = null!;

    private Mock<ITimelinePositionHandler> _timelinePositionHandler = null!;

    private TimelineZoomEventHandler _timelineZoomEventHandler = null!;
    private TimelineControlViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _timelineControl = new Mock<IFrameworkElement>();
        _viewModel = new TimelineControlViewModel();
        _timelinePositionHandler = new Mock<ITimelinePositionHandler>();
        _timelineZoomEventHandler =
            new TimelineZoomEventHandler(_timelineControl.Object, _viewModel, _timelinePositionHandler.Object);
    }

    [Test]
    public void ZoomAtZoomLevel1DoesNothing()
    {
        _viewModel.TimelineNavigationViewModel.ZoomLevel = 1;
        _timelineZoomEventHandler.Zoom(ZoomDirection.In);
        Assert.That(_viewModel.TimelineNavigationViewModel.ZoomLevel, Is.EqualTo(1));
    }

    [Test]
    public void ZoomOutAtBeginningDoesNotMoveTimeline()
    {
        _timelinePositionHandler.Setup(x =>
            x.GetCenterPosition(It.IsAny<double>(), It.IsAny<double>())).Returns(500);
        _timelinePositionHandler.Setup(x =>
            x.GetFrameAtPosition(It.IsAny<double>(), It.IsAny<int>())).Returns(3);
        _timelinePositionHandler.Setup(x =>
            x.GetPositionAtFrame(It.IsAny<double>(), It.IsAny<int>())).Returns(440);

        _timelineZoomEventHandler.Zoom(ZoomDirection.Out);
        Assert.That(_viewModel.TimelineNavigationViewModel.MovementPosition, Is.EqualTo(0));
    }
}