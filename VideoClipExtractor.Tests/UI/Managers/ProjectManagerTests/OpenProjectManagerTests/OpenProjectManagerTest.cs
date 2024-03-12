using BaseUI.Services.Dialogs;
using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Managers.Project.OpenProjectManager;

namespace VideoClipExtractor.Tests.UI.Managers.ProjectManagerTests.OpenProjectManagerTests;

[TestFixture]
[TestOf(typeof(OpenProjectManager))]
public class OpenProjectManagerTest : BaseDependencyTest
{
    private Mock<IProjectFileExplorer> _projectFileExplorer = null!;
    private Mock<IProjectSerializer> _projectSerializer = null!;
    private Mock<IProjectManager> _projectManager = null!;
    private Mock<IDialogService> _dialogService = null!;

    private OpenProjectManager _openProjectManager = null!;

    public override void Setup()
    {
        base.Setup();
        _projectFileExplorer = DependencyMock.CreateMockDependency<IProjectFileExplorer>();
        _projectSerializer = DependencyMock.CreateMockDependency<IProjectSerializer>();
        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _dialogService = DependencyMock.CreateMockDependency<IDialogService>();
        _openProjectManager = new OpenProjectManager(DependencyMock.Object);
    }

    [Test]
    public void OpenProjectByExplorerOpensExplorer()
    {
        _projectFileExplorer.Setup(x => x.GetOpenProjectFilePath()).Returns("path");
        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .Returns(ProjectExamples.GetExampleProject);

        _openProjectManager.OpenProjectByExplorer();
        _projectFileExplorer.Verify(x => x.GetOpenProjectFilePath(), Times.Once);
    }

    [Test]
    public void OpenProjectByExplorerSetsOpenedProject()
    {
        _projectFileExplorer.Setup(x => x.GetOpenProjectFilePath()).Returns("path");
        var project = ProjectExamples.GetExampleProject();

        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .Returns(project);

        _openProjectManager.OpenProjectByExplorer();
        _projectManager.Verify(x => x.SetOpenedProject(project, "path"), Times.Once);
    }

    [Test]
    public void OpenProjectByPathWithEmptyPathDoesNothing()
    {
        _openProjectManager.OpenProjectByPath(string.Empty);
        _projectSerializer.Verify(x => x.LoadProject(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void ExceptionIsShown()
    {
        _projectSerializer.Setup(x => x.LoadProject(It.IsAny<string>()))
            .Throws(new Exception("Test"));

        _openProjectManager.OpenProjectByPath("path");

        _dialogService.Verify(x => x.Show(It.IsAny<Exception>()), Times.Once);
    }
}