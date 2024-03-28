using Moq;
using VideoClipExtractor.Core.Managers.WorkspaceManager;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.Explorer.ExplorerActionBar;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.Explorer;

public class VideoExplorerViewModelTests : BaseViewModelTest
{
    private Mock<IExplorerActionBarViewModel> _actionBarMock = null!;

    private VideosExplorerViewModel _viewModel = null!;
    private Mock<IWorkspaceManager> _workspaceManagerMock = null!;

    public override void Setup()
    {
        base.Setup();
        _workspaceManagerMock = DependencyMock.CreateMockDependency<IWorkspaceManager>();
        _actionBarMock = ViewModelProviderMock.CreateViewModelMock<IExplorerActionBarViewModel>();
        _viewModel = new VideosExplorerViewModel(DependencyMock.Object);
    }

    [Test]
    public void VideosAreEmptyOnCreation()
    {
        Assert.That(_viewModel.Videos, Is.Empty);
    }

    [Test]
    public void SelectedVideoIsNullOnCreation()
    {
        Assert.That(_viewModel.SelectedVideo, Is.Null);
    }

    [Test]
    public void ProjectSetsActionBarProject()
    {
        var project = ProjectExamples.GetExampleProject();
        _viewModel.Project = project;
        _actionBarMock.VerifySet(m => m.Project = project);
    }

    [Test]
    public void VideosAreAddedWhenVideoAddedEventIsRaised()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _workspaceManagerMock.Raise(m => m.VideoAdded += null!, video);
        Assert.That(_viewModel.Videos, Has.Count.EqualTo(1));
    }

    [Test]
    public void SelectedVideoIsSetWhenVideoAddedEventIsRaised()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _workspaceManagerMock.Raise(m => m.VideoAdded += null!, video);
        Assert.That(_viewModel.SelectedVideo, Is.Not.Null);
    }

    [Test]
    public void VideosClearedWhenWorkspaceClearedEventIsRaised()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _workspaceManagerMock.Raise(m => m.VideoAdded += null!, video);
        _workspaceManagerMock.Raise(m => m.Clear += null!, EventArgs.Empty);
        Assert.That(_viewModel.Videos, Is.Empty);
    }
}