using BaseUI.Services.Dialogs;
using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Managers.Project.OpenProjectManager;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;

namespace VideoClipExtractor.Tests.UI.Managers.ProjectManagerTests.OpenProjectManagerTests;

[TestFixture]
[TestOf(typeof(OpenProjectManager))]
public class OpenProjectManagerTest : BaseViewModelTest
{
    private Mock<IProjectFileExplorer> _projectFileExplorer = null!;
    private Mock<IProjectSerializer> _projectSerializer = null!;
    private Mock<IProjectManager> _projectManager = null!;
    private Mock<IVideosSetupWindowViewModel> _videosSetupWindowViewModel = null!;
    private Mock<IDialogService> _dialogService = null!;

    private OpenProjectManager _openProjectManager = null!;

    public override void Setup()
    {
        base.Setup();
        _projectFileExplorer = DependencyMock.CreateMockDependency<IProjectFileExplorer>();
        _projectSerializer = DependencyMock.CreateMockDependency<IProjectSerializer>();
        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _videosSetupWindowViewModel = ViewModelProviderMock.CreateViewModelMock<IVideosSetupWindowViewModel>();
        _dialogService = DependencyMock.CreateMockDependency<IDialogService>();
        _openProjectManager = new OpenProjectManager(DependencyMock.Object);
    }

    [Test]
    public async Task OpenProjectByExplorerOpensExplorer()
    {
        _projectFileExplorer.Setup(x => x.GetOpenProjectFilePath()).Returns("path");
        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .ReturnsAsync(ProjectExamples.GetExampleProject());

        await _openProjectManager.OpenProjectByExplorer();
        _projectFileExplorer.Verify(x => x.GetOpenProjectFilePath(), Times.Once);
    }

    [Test]
    public async Task OpenProjectByExplorerSetsOpenedProject()
    {
        _projectFileExplorer.Setup(x => x.GetOpenProjectFilePath()).Returns("path");
        var project = ProjectExamples.GetExampleProject();

        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .ReturnsAsync(project);

        await _openProjectManager.OpenProjectByExplorer();
        _projectManager.Verify(x => x.SetOpenedProject(project, "path"), Times.Once);
    }

    [Test]
    public async Task OpenProjectByPathWithEmptyPathDoesNothing()
    {
        await _openProjectManager.OpenProjectByPath(string.Empty);
        _projectSerializer.Verify(x => x.LoadProject(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task OpenProjectByPathWithEmptyVideosShowsSetupVideos()
    {
        var project = ProjectExamples.GetEmptyProject();

        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .ReturnsAsync(project);

        await _openProjectManager.OpenProjectByPath("path");
        _videosSetupWindowViewModel.Verify(x => x.ShowDialog(), Times.Once);
    }

    [Test]
    public async Task ExceptionIsShown()
    {
        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .Throws(new Exception("Test"));

        await _openProjectManager.OpenProjectByPath("path");

        _dialogService.Verify(x => x.Show(It.IsAny<Exception>()), Times.Once);
    }
}