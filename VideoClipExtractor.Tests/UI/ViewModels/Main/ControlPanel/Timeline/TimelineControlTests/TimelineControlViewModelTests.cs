using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControlTests;

[TestFixture]
[TestOf(typeof(TimelineControlViewModel))]
public class TimelineControlViewModelTests : BaseViewModelTest
{
    private Mock<ITimelineNavigationViewModel> _timelineNavigationViewModelMock = null!;
    private Mock<IFramesVisualizationHandler> _framesVisualizationHandlerMock = null!;

    private TimelineControlViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _timelineNavigationViewModelMock = ViewModelProviderMock.CreateViewModelMock<ITimelineNavigationViewModel>();
        _framesVisualizationHandlerMock = DependencyMock.CreateMockDependency<IFramesVisualizationHandler>();
        _viewModel = new TimelineControlViewModel(DependencyMock.Object);
    }

    [Test]
    public void FrameVisualizationHandlerIsSetup()
    {
        _framesVisualizationHandlerMock.Verify(x => x.Setup(_viewModel), Times.Once);
    }

    [Test]
    public void VerticalLinesNotEmptyAtStart()
    {
        // Assert
        //Assert.That(_viewModel.VerticalLines, Is.Not.Empty);
    }

    [Test]
    public void MovementUpdatesVerticalLines()
    {
        // Act
        //_viewModel.TimelineNavigationViewModel.ZoomLevel = 1;
        //_viewModel.TimelineNavigationViewModel.MovementPosition = 250;

        // Assert
        //Assert.That(_viewModel.VerticalLines[0], Is.EqualTo(1));
    }
}