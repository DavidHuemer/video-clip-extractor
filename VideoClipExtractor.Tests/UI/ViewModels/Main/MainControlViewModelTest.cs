using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main;

[TestFixture]
[TestOf(typeof(MainControlViewModel))]
public class MainControlViewModelTest : BaseViewModelTest
{
    private ViewModelMock<IVideosExplorerViewModel> _explorerVm = null!;
    private Mock<IProjectManager> _projectManager = null!;
    private Mock<IVideoManager> _videoManager = null!;
    private Mock<IVideoPlayerViewModel> _videoPlayerVm = null!;
    private Mock<IControlPanelViewModel> _controlPanelVm = null!;
    private MainControlViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _explorerVm = ViewModelProviderMock.CreateViewModelMock<IVideosExplorerViewModel>();
        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _videoManager = DependencyMock.CreateMockDependency<IVideoManager>();
        _videoPlayerVm = ViewModelProviderMock.CreateViewModelMock<IVideoPlayerViewModel>();
        _controlPanelVm = ViewModelProviderMock.CreateViewModelMock<IControlPanelViewModel>();
        _viewModel = new MainControlViewModel(DependencyMock.Object);
    }

    [Test]
    public void ProjectIsSetToExplorer()
    {
        _explorerVm.Reset();
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);
        _viewModel = new MainControlViewModel(DependencyMock.Object);
        _explorerVm.VerifySet(x => x.Project = project);
    }

    [Test]
    public void ProjectChangeUpdatesExplorerProject()
    {
        var project = ProjectExamples.GetExampleProject();
        _explorerVm.Reset();
        _projectManager.Raise(x => x.ProjectChanged += null!, project);
        _explorerVm.VerifySet(x => x.Project = project);
    }

    [Test]
    public void VideoChangeUpdatesVideoManager()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _explorerVm.SetupGet(x => x.SelectedVideo).Returns(video);
        _explorerVm.RaisePropertyChanged(nameof(IVideosExplorerViewModel.SelectedVideo));
        _videoManager.VerifySet(x => x.Video = video);
    }

    [Test]
    public void VideoChangeUpdatesVideoPlayer()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _explorerVm.SetupGet(x => x.SelectedVideo).Returns(video);
        _explorerVm.RaisePropertyChanged(nameof(IVideosExplorerViewModel.SelectedVideo));
        _videoPlayerVm.VerifySet(x => x.Video = video);
    }

    [Test]
    public void VideoChangeUpdatesControlPanel()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _explorerVm.SetupGet(x => x.SelectedVideo).Returns(video);
        _explorerVm.RaisePropertyChanged(nameof(IVideosExplorerViewModel.SelectedVideo));
        _controlPanelVm.VerifySet(x => x.Video = video);
    }
}