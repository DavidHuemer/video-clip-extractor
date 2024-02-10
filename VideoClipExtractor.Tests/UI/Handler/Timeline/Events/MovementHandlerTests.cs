using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Basics.MouseCursorHandler;
using Moq;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events;

public class MovementHandlerTests
{
    private Mock<IFrameworkElement> _frameworkElementMock = null!;
    private Mock<IMouseCursorHandler> _mouseCursorHandlerMock = null!;
    private TimelineControlViewModel _timelineControlViewModel = null!;

    private TimelineMovementHandler _timelineMovementHandler = null!;

    [SetUp]
    public void Setup()
    {
        _frameworkElementMock = new Mock<IFrameworkElement>();
        _timelineControlViewModel = new TimelineControlViewModel();
        _mouseCursorHandlerMock = new Mock<IMouseCursorHandler>();

        _timelineMovementHandler = new TimelineMovementHandler(_frameworkElementMock.Object, _timelineControlViewModel,
            _mouseCursorHandlerMock.Object);
    }

    [Test]
    public void MoveWithoutStartDoesNotMove()
    {
        _timelineMovementHandler.Move(new Point(50, 0));

        Assert.That(_timelineControlViewModel.TimelineNavigationViewModel.MovementPosition, Is.EqualTo(0));
    }

    [Test]
    [TestCase(0, 50, 40, 10)]
    [TestCase(0, 50, 60, 0)]
    [TestCase(100, 50, 20, 130)]
    [TestCase(100, 50, 70, 80)]
    [TestCase(100, 50, 100, 50)]
    [TestCase(100, 50, 150, 0)]
    [TestCase(100, 50, 200, 0)]
    [TestCase(0, 10, 0, 10)]
    [TestCase(0, 10, -10, 20)]
    public void MoveMovesTimelineCorrectly(double defaultPosition, double startPosition, double movePosition,
        double expectedTimelinePosition)
    {
        _timelineControlViewModel.TimelineNavigationViewModel.MovementPosition = defaultPosition;

        _timelineMovementHandler.StartMovement(new Point(startPosition, 0));
        _timelineMovementHandler.Move(new Point(movePosition, 0));

        Assert.That(_timelineControlViewModel.TimelineNavigationViewModel.MovementPosition,
            Is.EqualTo(expectedTimelinePosition));
    }

    [Test]
    public void LeftMouseLeaveHandled()
    {
        _timelineControlViewModel.TimelineNavigationViewModel.MovementPosition = 0;
        _timelineMovementHandler.StartMovement(new Point(10, 0));

        var relativePosition = new Point(990, 0);
        _frameworkElementMock.Setup(x => x.ActualWidth).Returns(1000);
        _frameworkElementMock.Setup(x => x.PointToScreen(It.IsAny<Point>())).Returns(relativePosition);

        _timelineMovementHandler.Move(new Point(-10, 0));
        _frameworkElementMock.Verify(x =>
            x.PointToScreen(It.Is<Point>(p => Math.Abs(p.X - 990) < 0.1)), Times.Once);

        _mouseCursorHandlerMock.Verify(x =>
                x.SetCursorPosition(It.Is<Point>(p => Math.Abs(p.X - 990) < 0.1)),
            Times.Once);
    }

    [Test]
    public void RightMouseLeaveHandled()
    {
        _timelineControlViewModel.TimelineNavigationViewModel.MovementPosition = 0;
        _timelineMovementHandler.StartMovement(new Point(980, 0));

        var relativePosition = new Point(15, 0);
        _frameworkElementMock.Setup(x => x.ActualWidth).Returns(1000);
        _frameworkElementMock.Setup(x => x.PointToScreen(It.IsAny<Point>())).Returns(relativePosition);

        _timelineMovementHandler.Move(new Point(1015, 0));
        _frameworkElementMock.Verify(x =>
            x.PointToScreen(It.Is<Point>(p => Math.Abs(p.X - 15) < 0.1)), Times.Once);

        _mouseCursorHandlerMock.Verify(x =>
                x.SetCursorPosition(It.Is<Point>(p => Math.Abs(p.X - 15) < 0.1)),
            Times.Once);
    }
}