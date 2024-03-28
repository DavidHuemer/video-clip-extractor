using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Managers.ProjectManagers.OpenProjectManager;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.NewProjectViewModels;

[TestFixture]
[TestOf(typeof(NewProjectViewModel))]
public class NewProjectViewModelTest : BaseViewModelTest
{
    private Mock<IProjectFileExplorer> _projectFileExplorer = null!;
    private Mock<IVideoRepositoryExplorerWindowViewModel> _videoRepositoryExplorerWindowViewModel = null!;
    private Mock<IFileExplorer> _fileExplorer = null!;
    private Mock<IProjectSerializer> _projectSerializer = null!;
    private Mock<IOpenProjectManager> _openProjectManager = null!;

    private NewProjectViewModel _newProjectViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _projectFileExplorer = DependencyMock.CreateMockDependency<IProjectFileExplorer>();
        _videoRepositoryExplorerWindowViewModel =
            ViewModelProviderMock.CreateViewModelMock<IVideoRepositoryExplorerWindowViewModel>();

        _fileExplorer = DependencyMock.CreateMockDependency<IFileExplorer>();
        _projectSerializer = DependencyMock.CreateMockDependency<IProjectSerializer>();
        _openProjectManager = DependencyMock.CreateMockDependency<IOpenProjectManager>();
        _newProjectViewModel = new NewProjectViewModel(DependencyMock.Object);
    }

    [Test]
    public void NameIsNotEmptyAtBeginning()
    {
        Assert.That(_newProjectViewModel.Name, Is.Not.Empty);
    }

    [Test]
    public void CreateProjectNotAllowedAtBeginning()
    {
        Assert.That(_newProjectViewModel.CreateProject.CanExecute(null), Is.False);
    }

    [Test]
    public void BrowseProjectPathIsAllowed()
    {
        Assert.That(_newProjectViewModel.BrowseProjectPath.CanExecute(null), Is.True);
    }

    [Test]
    public void BrowseProjectPathSetsProjectPath()
    {
        _projectFileExplorer.Setup(x => x.GetSaveProjectFilePath()).Returns("C:\\test");
        _newProjectViewModel.BrowseProjectPath.Execute(null);
        Assert.That(_newProjectViewModel.ProjectPath, Is.EqualTo("C:\\test"));
    }

    [Test]
    public void BrowseVideoRepositoryIsAllowed()
    {
        Assert.That(_newProjectViewModel.BrowseVideoRepository.CanExecute(null), Is.True);
    }

    [Test]
    public void BrowseVideoRepositoryShowsVideoRepositoryExplorer()
    {
        _newProjectViewModel.BrowseVideoRepository.Execute(null);
        _videoRepositoryExplorerWindowViewModel.Verify(x => x.ShowDialog(), Times.Once);
    }

    [Test]
    public void VideoRepositoryBlueprintSelectedSetsVideoRepositoryBlueprint()
    {
        var videoRepositoryBlueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "path");
        _newProjectViewModel.BrowseVideoRepository.Execute(null);
        _videoRepositoryExplorerWindowViewModel.Raise(x => x.VideoRepositoryBlueprintSelected += null,
            _videoRepositoryExplorerWindowViewModel.Object,
            new VideoRepositoryBlueprintEventArgs(videoRepositoryBlueprint));
        Assert.That(_newProjectViewModel.VideoRepositoryBlueprint, Is.EqualTo(videoRepositoryBlueprint));
    }

    [Test]
    public void BrowseImageDirectoryIsAllowed()
    {
        Assert.That(_newProjectViewModel.BrowseImageDirectory.CanExecute(null), Is.True);
    }

    [Test]
    public void BrowseImageDirectorySetsImageDirectoryPath()
    {
        _fileExplorer.Setup(x => x.GetBrowseDirectoryPath()).Returns("C:\\test");
        _newProjectViewModel.BrowseImageDirectory.Execute(null);
        Assert.That(_newProjectViewModel.ImageDirectoryPath, Is.EqualTo("C:\\test"));
    }

    [Test]
    public void CreateProjectIsAllowedWhenAllFieldsAreFilled()
    {
        _newProjectViewModel.Name = "name";
        _newProjectViewModel.ProjectPath = "path";
        _newProjectViewModel.ImageDirectoryPath = "path";
        _newProjectViewModel.VideoRepositoryBlueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "path");
        Assert.That(_newProjectViewModel.CreateProject.CanExecute(null), Is.True);
    }

    [Test]
    public void CreateDoesNothingWhenBlueprintIsNull()
    {
        _newProjectViewModel.CreateProject.Execute(null);
        _openProjectManager.Verify(x => x.OpenProjectByPath(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void SerializerIsCalled()
    {
        _newProjectViewModel.Name = "name";
        _newProjectViewModel.ProjectPath = "path";
        _newProjectViewModel.ImageDirectoryPath = "path";
        _newProjectViewModel.VideoRepositoryBlueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "path");

        _newProjectViewModel.CreateProject.Execute(null);
        _projectSerializer.Verify(x => x.StoreProject(It.IsAny<Project>(), It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void OpenProjectManagerIsCalled()
    {
        _newProjectViewModel.Name = "name";
        _newProjectViewModel.ProjectPath = "path";
        _newProjectViewModel.ImageDirectoryPath = "path";
        _newProjectViewModel.VideoRepositoryBlueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "path");

        _newProjectViewModel.CreateProject.Execute(null);
        _openProjectManager.Verify(x => x.OpenProjectByPath(It.IsAny<string>()), Times.Once);
    }
}