using Moq;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.Core.Managers.VideoRepositoryManagerTests;

[TestFixture]
[TestOf(typeof(VideoRepositoryManager))]
public class VideoRepositoryManagerTest : BaseDependencyTest
{
    private Mock<IVideoRepositoryBuilder> _videoRepositoryBuilder = null!;
    private VideoRepositoryManager _videoRepositoryManager = null!;

    public override void Setup()
    {
        base.Setup();
        _videoRepositoryBuilder = DependencyMock.CreateMockDependency<IVideoRepositoryBuilder>();
        _videoRepositoryManager = new VideoRepositoryManager(DependencyMock.Object);
    }

    [Test]
    public void VideoRepositoryIsNullAtBeginning()
    {
        Assert.That(_videoRepositoryManager.VideoRepository, Is.Null);
    }

    [Test]
    public void SetupRepositoryByBlueprintSetsVideoRepository()
    {
        var videoRepo = new Mock<IVideoRepository>();
        _videoRepositoryBuilder.Setup(x => x.Build(It.IsAny<VideoRepositoryBlueprint>())).Returns(videoRepo.Object);

        var blueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "");
        _videoRepositoryManager.SetupRepositoryByBlueprint(blueprint);

        Assert.That(_videoRepositoryManager.VideoRepository, Is.EqualTo(videoRepo.Object));
    }

    [Test]
    public void SetupRepositoryByBlueprintConnectsVideoRepository()
    {
        var videoRepo = new Mock<IVideoRepository>();
        _videoRepositoryBuilder.Setup(x => x.Build(It.IsAny<VideoRepositoryBlueprint>())).Returns(videoRepo.Object);

        var blueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "");
        _videoRepositoryManager.SetupRepositoryByBlueprint(blueprint);

        videoRepo.Verify(x => x.Connect(), Times.Once);
    }

    [Test]
    public void SetupRepositoryByBlueprintInvokesVideoRepositoryChanged()
    {
        var videoRepo = new Mock<IVideoRepository>();
        _videoRepositoryBuilder.Setup(x => x.Build(It.IsAny<VideoRepositoryBlueprint>())).Returns(videoRepo.Object);

        var blueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "");

        _videoRepositoryManager.VideoRepositoryChanged += (repo) =>
        {
            Assert.That(repo, Is.EqualTo(videoRepo.Object));
        };

        _videoRepositoryManager.SetupRepositoryByBlueprint(blueprint);
    }
}