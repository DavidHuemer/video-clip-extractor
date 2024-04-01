using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerActionBar;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel;

[TestFixture]
[TestOf(typeof(VideoPlayerControlPanelViewModel))]
public class VideoPlayerControlPanelViewModelTest : BaseViewModelTest
{
    private Mock<IVideoPlayerNavigationViewModel> _videoPlayerNavigationViewModelMock = null!;
    private Mock<IVideoPlayerActionBarViewModel> _videoPlayerActionBarViewModel = null!;
    private VideoPlayerControlPanelViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPlayerNavigationViewModelMock =
            ViewModelProviderMock.CreateViewModelMock<IVideoPlayerNavigationViewModel>();

        _videoPlayerActionBarViewModel =
            ViewModelProviderMock.CreateViewModelMock<IVideoPlayerActionBarViewModel>();

        _viewModel = new VideoPlayerControlPanelViewModel(DependencyMock.Object);
    }

    [Test]
    public void ViewModelsAreSet()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.VideoPlayerNavigationViewModel,
                Is.EqualTo(_videoPlayerNavigationViewModelMock.Object));
            Assert.That(_viewModel.ActionBarViewModel, Is.EqualTo(_videoPlayerActionBarViewModel.Object));
        });
    }

    [Test]
    public void VideoSetsVideoPlayerNavigationVideo()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video = video;
        _videoPlayerNavigationViewModelMock.VerifySet(x => x.Video = video);
    }
}