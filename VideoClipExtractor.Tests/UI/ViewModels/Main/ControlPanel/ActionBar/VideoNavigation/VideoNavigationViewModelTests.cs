using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

public class VideoNavigationViewModelTests : BaseViewModelTest
{
    private VideoNavigationViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _viewModel = new VideoNavigationViewModel();
    }

    [Test]
    public void PlayPauseCommand_WhenVideoIsNull_ShouldReturnFalse()
    {
        // Arrange
        _viewModel.Video = null;

        // Act
        var result = _viewModel.PlayPause.CanExecute(null);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void PlayPauseCommand_WhenVideoIsNotNull_ShouldReturnTrue()
    {
        // Arrange
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();

        // Act
        var result = _viewModel.PlayPause.CanExecute(null);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void PlayPauseCommand_WhenPlayStatusIsPaused_ShouldChangePlayStatusToPlaying()
    {
        // Arrange
        _viewModel.PlayStatus = PlayStatus.Paused;

        // Act
        _viewModel.PlayPause.Execute(null);

        // Assert
        Assert.That(_viewModel.PlayStatus, Is.EqualTo(PlayStatus.Playing));
    }

    [Test]
    public void PlayPauseCommand_WhenPlayStatusIsPlaying_ShouldChangePlayStatusToPaused()
    {
        // Arrange
        _viewModel.PlayStatus = PlayStatus.Playing;

        // Act
        _viewModel.PlayPause.Execute(null);

        // Assert
        Assert.That(_viewModel.PlayStatus, Is.EqualTo(PlayStatus.Paused));
    }
}