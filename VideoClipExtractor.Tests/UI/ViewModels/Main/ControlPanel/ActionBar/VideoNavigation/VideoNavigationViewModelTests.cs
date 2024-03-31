using BaseUI.Basics.DelayWrapper;
using Moq;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

public class VideoNavigationViewModelTests : BaseViewModelTest
{
    private DelayTestWrapper _delayTestWrapper;
    private Mock<IFrameNavigationViewModel> _frameNavigationViewModel = null!;
    private VideoNavigationViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _delayTestWrapper = new DelayTestWrapper();
        _frameNavigationViewModel = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        DependencyMock.Setup(x => x.GetDependency<IDelayWrapper>()).Returns(_delayTestWrapper);
        _viewModel = new VideoNavigationViewModel(DependencyMock.Object);
    }

    [Test]
    public void PlayStatusPausedAtBeginning()
    {
        Assert.That(_viewModel.PlayStatus, Is.EqualTo(PlayStatus.Paused));
    }

    [Test]
    public void VideoChangeSetsPlayStatusToPlaying()
    {
        _viewModel.PlayStatus = PlayStatus.Paused;
        var video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video = video;
        Assert.That(_viewModel.PlayStatus, Is.EqualTo(PlayStatus.Playing));
    }

    [Test]
    public void PlayPauseCommandNotAllowedWhenVideoNull()
    {
        // Arrange
        _viewModel.Video = null;

        // Act
        var result = _viewModel.PlayPause.CanExecute(null);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void PlayPauseCommandAllowedWhenVideoNotNull()
    {
        // Arrange
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();

        // Act
        var result = _viewModel.PlayPause.CanExecute(null);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase(PlayStatus.Paused, PlayStatus.Playing)]
    [TestCase(PlayStatus.Playing, PlayStatus.Paused)]
    public void PlayPauseCommandSetsPlayStatus(PlayStatus current, PlayStatus expected)
    {
        _viewModel.PlayStatus = current;

        _viewModel.PlayPause.Execute(null);

        Assert.That(_viewModel.PlayStatus, Is.EqualTo(expected));
    }
}