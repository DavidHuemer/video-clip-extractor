using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

public class TimelineControlViewModelTests : BaseViewModelTest
{
    private TimelineControlViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();


        _viewModel = new TimelineControlViewModel(DependencyMock.Object);
    }

    [Test]
    [Ignore("Not ready!")]
    public void VerticalLinesNotEmptyAtStart()
    {
        // Assert
        Assert.That(_viewModel.VerticalLines, Is.Not.Empty);
    }

    [Test]
    [Ignore("Not ready!")]
    public void MovementUpdatesVerticalLines()
    {
        // Act
        _viewModel.TimelineNavigationViewModel.ZoomLevel = 1;
        _viewModel.TimelineNavigationViewModel.MovementPosition = 250;

        // Assert
        Assert.That(_viewModel.VerticalLines[0], Is.EqualTo(1));
    }
}