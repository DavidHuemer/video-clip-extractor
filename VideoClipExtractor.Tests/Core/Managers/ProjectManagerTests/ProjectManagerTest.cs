using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Managers.ProjectManagerTests;

[TestFixture]
[TestOf(typeof(ProjectManager))]
public class ProjectManagerTest : BaseDependencyTest
{
    private Mock<IProjectSerializer> _projectSerializer = null!;
    private Mock<IVideoRepositoryManager> _videoRepositoryManager = null!;
    private ProjectManager _projectManager = null!;

    public override void Setup()
    {
        base.Setup();
        _projectSerializer = DependencyMock.CreateMockDependency<IProjectSerializer>();
        _videoRepositoryManager = DependencyMock.CreateMockDependency<IVideoRepositoryManager>();
        _projectManager = new ProjectManager(DependencyMock.Object);
    }

    [Test]
    public void ProjectIsNullAtBeginning()
    {
        Assert.That(_projectManager.Project, Is.Null);
    }

    [Test]
    public void StoreProjectDoesNothingAtBeginning()
    {
        _projectManager.StoreProject();
        _projectSerializer.Verify(x => x.StoreProject(It.IsAny<Project>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void SetOpenedProjectSetsProject()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetOpenedProject(project, "path");
        Assert.That(_projectManager.Project, Is.EqualTo(project));
    }

    [Test]
    public void SetOpenedProjectInvokesEvent()
    {
        var project = ProjectExamples.GetExampleProject();
        var eventInvoked = false;
        _projectManager.ProjectOpened += (_, _) => eventInvoked = true;
        _projectManager.SetOpenedProject(project, "path");
        Assert.That(eventInvoked, Is.True);
    }

    [Test]
    public void SetOpenedProjectsSetsUpVideoRepositoryManager()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetOpenedProject(project, "path");
        _videoRepositoryManager.Verify(x => x.SetupRepositoryByBlueprint(project.VideoRepositoryBlueprint), Times.Once);
    }

    [Test]
    public void StoreProjectStoresProject()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetOpenedProject(project, "path");
        _projectManager.StoreProject();
        _projectSerializer.Verify(x => x.StoreProject(project, "path"), Times.Once);
    }
}