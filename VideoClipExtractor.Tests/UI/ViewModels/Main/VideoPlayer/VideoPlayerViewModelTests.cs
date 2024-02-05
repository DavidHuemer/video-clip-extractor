using Moq;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModelTests : BaseViewModelTest
{
    private Mock<IVideoManager> _videoManagerMock = null!;

    private Mock<IVideosExplorerViewModel> _videosExplorerViewModelMock = null!;

    private VideoPlayerViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoManagerMock = new Mock<IVideoManager>();
        AddMockDependency(_videoManagerMock);

        _videosExplorerViewModelMock = new Mock<IVideosExplorerViewModel>();
        AddViewModel(_videosExplorerViewModelMock);
        _viewModel = new VideoPlayerViewModel(DependencyMock.Object);
    }

    [Test]
    public void ExplorerViewModelIsNotNull()
    {
        Assert.That(_viewModel.ExplorerViewModel, Is.Not.Null);
    }
}