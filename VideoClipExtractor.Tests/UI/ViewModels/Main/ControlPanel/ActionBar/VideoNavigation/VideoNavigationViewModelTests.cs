using BaseUI.Basics.DelayWrapper;
using Moq;
using VideoClipExtractor.Core.Managers.PlayStatusManager;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

[TestFixture]
[TestOf(typeof(VideoNavigationViewModel))]
public class VideoNavigationViewModelTests : BaseViewModelTest
{
    private DelayTestWrapper _delayTestWrapper = null!;
    private Mock<IPlayStatusManager> _playStatusManager = null!;
    private Mock<IFrameNavigationViewModel> _frameNavigationViewModel = null!;
    private VideoNavigationViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _delayTestWrapper = new DelayTestWrapper();
        DependencyMock.Setup(x => x.GetDependency<IDelayWrapper>())
            .Returns(_delayTestWrapper);
        _playStatusManager = DependencyMock.CreateMockDependency<IPlayStatusManager>();
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
    public void VideoChangeCallsFrameNavigationViewModel()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video = video;
        _frameNavigationViewModel.VerifySet(x => x.Video = video, Times.Once);
    }

    [Test]
    public void VideoChangeCallsPlayStatusManager()
    {
        _viewModel.PlayStatus = PlayStatus.Paused;
        var video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video = video;

        _playStatusManager.Verify(x => x.SetMainPlayStatus(PlayStatus.Playing), Times.Once);
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

        _playStatusManager.Verify(x => x.SetMainPlayStatus(PlayStatus.Paused), Times.Once);
    }
}