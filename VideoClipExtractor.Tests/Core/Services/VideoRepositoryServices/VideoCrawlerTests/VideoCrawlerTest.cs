using BaseUI.Basics.CurrentApplicationWrapper;
using BaseUI.Exceptions.Basics;
using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;
using VideoClipExtractor.Data.Exceptions.VideoRepositoryExceptions;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Core.Services.VideoRepositoryServices.VideoCrawlerTests;

[TestFixture]
[TestOf(typeof(VideoCrawler))]
public class VideoCrawlerTest : BaseDependencyTest
{
    private Mock<IProjectManager> _projectManager = null!;
    private Mock<IVideoRepositoryManager> _videoRepoManager = null!;
    private CurrentApplicationTestWrapper _currentApplicationTestWrapper = null!;
    private VideoCrawler _videoCrawler = null!;

    public override void Setup()
    {
        base.Setup();
        _currentApplicationTestWrapper = new CurrentApplicationTestWrapper();
        DependencyMock.Setup(x => x.GetDependency<ICurrentApplicationWrapper>())
            .Returns(_currentApplicationTestWrapper);

        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _videoRepoManager = DependencyMock.CreateMockDependency<IVideoRepositoryManager>();
        _videoCrawler = new VideoCrawler(DependencyMock.Object);
    }

    [Test]
    public void RunCrawlerWithoutProjectThrowsException()
    {
        _projectManager.SetupGet(x => x.Project).Returns(null as Project);
        Assert.ThrowsAsync<ProjectNotSetException>(() => _videoCrawler.CrawlVideos());
    }

    [Test]
    public void RunCrawlerWithoutRepoThrowsException()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        _videoRepoManager.SetupGet(x => x.VideoRepository).Returns(null as IVideoRepository);
        Assert.ThrowsAsync<VideoRepositoryNotSetException>(() => _videoCrawler.CrawlVideos());
    }

    [Test]
    public async Task RunCrawlerGetsFiles()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        var repo = new Mock<IVideoRepository>();
        _videoRepoManager.SetupGet(x => x.VideoRepository).Returns(repo.Object);

        await _videoCrawler.CrawlVideos();
        repo.Verify(x => x.GetFiles(), Times.Once);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(VideoCrawler.BufferSize - 1)]
    [TestCase(VideoCrawler.BufferSize)]
    [TestCase(VideoCrawler.BufferSize + 1)]
    [TestCase(VideoCrawler.BufferSize * 2)]
    [TestCase(VideoCrawler.BufferSize * 3)]
    public async Task RunCrawlerInvokesVideosAdded(int nrSourceVideos)
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);

        var repo = new Mock<IVideoRepository>();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(nrSourceVideos);
        repo.Setup(x => x.GetFiles()).Returns(sourceVideos);

        _videoRepoManager.SetupGet(x => x.VideoRepository).Returns(repo.Object);

        var videosAddedCounter = 0;
        _videoCrawler.VideosAdded += (_) => { videosAddedCounter++; };

        await _videoCrawler.CrawlVideos();
        Assert.That(videosAddedCounter, Is.EqualTo(videosAddedCounter % VideoCrawler.BufferSize));
    }
}