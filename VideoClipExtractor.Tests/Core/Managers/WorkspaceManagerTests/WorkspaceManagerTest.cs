using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Core.Managers.WorkspaceManager;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Managers.WorkspaceManagerTests;

[TestFixture]
[TestOf(typeof(WorkspaceManager))]
public class WorkspaceManagerTest : BaseDependencyTest
{
    private Mock<IProjectManager> _projectManagerMock = null!;
    private Mock<IVideoRepositoryManager> _videoRepositoryManagerMock = null!;
    private Mock<IVideoProviderManager> _videoProviderManagerMock = null!;
    private WorkspaceManager _workspaceManager = null!;

    public override void Setup()
    {
        base.Setup();
        _projectManagerMock = DependencyMock.CreateMockDependency<IProjectManager>();
        _videoRepositoryManagerMock = DependencyMock.CreateMockDependency<IVideoRepositoryManager>();
        _videoProviderManagerMock = DependencyMock.CreateMockDependency<IVideoProviderManager>();
        _workspaceManager = new WorkspaceManager(DependencyMock.Object);
    }

    [Test]
    public void RepositoryChangeInvokesClear()
    {
        var clearInvoked = false;
        _workspaceManager.Clear += (_, _) => clearInvoked = true;

        _projectManagerMock.SetupGet(x => x.Project).Returns(null as Project);
        _videoRepositoryManagerMock.SetupGet(x => x.VideoRepository).Returns(null as IVideoRepository);

        var repositoryMock = new Mock<IVideoRepository>();
        _videoRepositoryManagerMock.Raise(m => m.VideoRepositoryChanged += null!, repositoryMock.Object);
        Assert.IsTrue(clearInvoked);
    }

    [Test]
    public void VideoProviderIsSetupWhenRepoChanges()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>().Object;

        _projectManagerMock.SetupGet(x => x.Project).Returns(project);
        _videoRepositoryManagerMock.SetupGet(x => x.VideoRepository).Returns(repository);

        _videoRepositoryManagerMock.Raise(m => m.VideoRepositoryChanged += null!, repository);
        _videoProviderManagerMock.Verify(m => m.Setup(project, repository), Times.Once);
    }

    [Test]
    public void SourceVideosChangedSetsUpProvider()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>().Object;

        _projectManagerMock.SetupGet(x => x.Project).Returns(project);
        _videoRepositoryManagerMock.SetupGet(x => x.VideoRepository).Returns(repository);

        _workspaceManager.SourceVideosChanged();
        _videoProviderManagerMock.Verify(m => m.Setup(project, repository), Times.Once);
    }

    [Test]
    public void VideoAddedEventIsInvoked()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var videoAddedInvoked = false;
        _workspaceManager.VideoAdded += (v) =>
        {
            videoAddedInvoked = true;
            Assert.That(v, Is.EqualTo(video));
        };

        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, video);
        Assert.IsTrue(videoAddedInvoked);
    }
}