using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModelTests : BaseViewModelTest
{
    private Mock<IVideoPlayerControlPanelViewModel> _controlPanelviewModel = null!;
    private Mock<IVideoNavigationViewModel> _videoNavigationViewModel = null!;
    private Mock<IVideoPlayerNavigationViewModel> _videoPlayerNavigationMock = null!;
    private Mock<IVideosExplorerViewModel> _videosExplorerViewModelMock = null!;

    private VideoPlayerViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPlayerNavigationMock = ViewModelProviderMock.CreateViewModelMock<IVideoPlayerNavigationViewModel>();
        _videosExplorerViewModelMock = ViewModelProviderMock.CreateViewModelMock<IVideosExplorerViewModel>();
        _videoNavigationViewModel = ViewModelProviderMock.CreateViewModelMock<IVideoNavigationViewModel>();
        _controlPanelviewModel = ViewModelProviderMock.CreateViewModelMock<IVideoPlayerControlPanelViewModel>();
        _viewModel = new VideoPlayerViewModel(DependencyMock.Object);
    }

    [Test]
    public void ViewModelsAreSet()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.VideoPlayerNavigationVm, Is.EqualTo(_videoPlayerNavigationMock.Object));
            Assert.That(_viewModel.ExplorerViewModel, Is.EqualTo(_videosExplorerViewModelMock.Object));
            Assert.That(_viewModel.VideoNavigationViewModel, Is.EqualTo(_videoNavigationViewModel.Object));
        });
    }

    [Test]
    public void VideoSetsControlPanelVideo()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video = video;
        _controlPanelviewModel.VerifySet(x => x.Video = video);
    }
}