using BaseUI.Services.Dialogs;
using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.WorkspaceManager;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupResultViewModels;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupSettingsViewModels;

namespace VideoClipExtractor.Tests.UI.ViewModels.SetupViewModels.VideosSetupViewModels;

[TestFixture]
[TestOf(typeof(VideosSetupViewModel))]
public class VideosSetupViewModelTest : BaseViewModelTest
{
    private Mock<IVideoSetupSettingsViewModel> _videoSetupSettingsViewModel = null!;
    private Mock<IVideoSetupResultViewModel> _videoSetupResultViewModel = null!;

    private Mock<IProjectManager> _projectManager = null!;
    private Mock<IDialogService> _dialogService = null!;
    private Mock<IWorkspaceManager> _workspaceManager = null!;

    private VideosSetupViewModel _videosSetupViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoSetupSettingsViewModel = ViewModelProviderMock.CreateViewModelMock<IVideoSetupSettingsViewModel>();
        _videoSetupResultViewModel = ViewModelProviderMock.CreateViewModelMock<IVideoSetupResultViewModel>();

        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _dialogService = DependencyMock.CreateMockDependency<IDialogService>();
        _workspaceManager = DependencyMock.CreateMockDependency<IWorkspaceManager>();

        _videosSetupViewModel = new VideosSetupViewModel(DependencyMock.Object);
    }

    [Test]
    public void ProjectIsSet()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        _videoSetupSettingsViewModel.Reset();
        _ = new VideosSetupViewModel(DependencyMock.Object);
        _videoSetupSettingsViewModel.VerifySet(x => x.Project = project, Times.Once);
    }

    [Test]
    public void ProjectChangeUpdatesSettings()
    {
        var project = ProjectExamples.GetExampleProject();
        _videoSetupSettingsViewModel.Reset();

        _projectManager.Raise(x => x.ProjectChanged += null!, project);
        _videoSetupSettingsViewModel.VerifySet(x => x.Project = project, Times.Once);
    }

    [Test]
    public void LoadRequestedLoadsVideos()
    {
        var settings = new VideoSetupSettings();
        _videoSetupSettingsViewModel.Raise(x => x.LoadVideosRequested += null!, settings);
        _videoSetupResultViewModel.Verify(x => x.LoadVideos(), Times.Once);
    }

    [Test]
    public void VideosAddedCallsSettingsViewModel()
    {
        var settings = new VideoSetupSettings();
        _videoSetupSettingsViewModel.Raise(x => x.LoadVideosRequested += null!, settings);
        _videoSetupSettingsViewModel.Verify(x => x.LoadingFinished(), Times.Once);
    }

    [Test]
    public void VideosAddedUpdatesProject()
    {
        var project = ProjectExamples.GetEmptyProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        var expectedProject = ProjectExamples.GetEmptyProject();
        expectedProject.Videos.AddRange(sourceVideos);

        _videoSetupResultViewModel.Raise(x => x.VideosAdded += null!, sourceVideos);
        Assert.That(project, Is.EqualTo(expectedProject));
    }

    [Test]
    public void VideoAddedStoresProject()
    {
        var project = ProjectExamples.GetEmptyProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        _videoSetupResultViewModel.Raise(x => x.VideosAdded += null!, new List<SourceVideo>());
        _projectManager.Verify(x => x.StoreProject(), Times.Once);
    }

    [Test]
    public void DialogServiceShowsExceptionWhenProjectNotSet()
    {
        _projectManager.SetupGet(x => x.Project).Returns(null as Project);
        _videoSetupResultViewModel.Raise(x => x.VideosAdded += null!, new List<SourceVideo>());
        _dialogService.Verify(x => x.Show(It.IsAny<Exception>()), Times.Once);
    }

    [Test]
    public void FinishEventInvokedAfterVideosAdded()
    {
        var project = ProjectExamples.GetEmptyProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);
        var finishInvoked = false;
        _videosSetupViewModel.Finish += (sender, args) => finishInvoked = true;

        _videoSetupResultViewModel.Raise(x => x.VideosAdded += null!, new List<SourceVideo>());
        Assert.That(finishInvoked, Is.True);
    }

    [Test]
    public void WorkspaceManagerNotifiedAfterVideosAdded()
    {
        var project = ProjectExamples.GetEmptyProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        _videoSetupResultViewModel.Raise(x => x.VideosAdded += null!, new List<SourceVideo>());
        _workspaceManager.Verify(x => x.SourceVideosChanged(), Times.Once);
    }
}