using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Managers.VideoProviderManagerTests;

[TestFixture]
[TestOf(typeof(VideoProviderManager))]
public class VideoProviderManagerTest : BaseDependencyTest
{
    private Mock<IVideoProvider> _videoProviderMock = null!;
    private VideoProviderManager _videoProviderManager = null!;

    public override void Setup()
    {
        base.Setup();
        _videoProviderMock = DependencyMock.CreateMockDependency<IVideoProvider>();
        _videoProviderManager = new VideoProviderManager(DependencyMock.Object);
    }

    [Test]
    public void VideoProviderNullAtBeginning()
    {
        Assert.That(_videoProviderManager.VideoProvider, Is.Null);
    }

    [Test]
    public void SetupWithNullProjectAndRepositoryDoesNotSetProvider()
    {
        _videoProviderManager.Setup(null, null);
        Assert.That(_videoProviderManager.VideoProvider, Is.Null);
    }

    [Test]
    public void NextWithoutProviderDoesNothing()
    {
        _videoProviderManager.Next();
        _videoProviderMock.VerifyNoOtherCalls();
    }

    [Test]
    public void SetupSetsUpProvider()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>();
        _videoProviderManager.Setup(project, repository.Object);

        Assert.That(_videoProviderManager.VideoProvider, Is.Not.Null);
    }

    [Test]
    public void SetupCallsProviderSetup()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>();
        _videoProviderManager.Setup(project, repository.Object);

        _videoProviderMock.Verify(p => p.Setup(project, repository.Object), Times.Once);
    }

    [Test]
    public void VideoAddedIsInvoked()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>();
        _videoProviderManager.Setup(project, repository.Object);

        var video = VideoExamples.GetVideoViewModelExample();
        _videoProviderManager.VideoAdded += v => Assert.That(v, Is.EqualTo(video));
        _videoProviderMock.Raise(p => p.VideoAdded += null, video);
    }

    [Test]
    public void SetupClearsProvider()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>();
        _videoProviderManager.Setup(project, repository.Object);

        _videoProviderManager.Setup(null, null);
        Assert.That(_videoProviderManager.VideoProvider, Is.Null);
    }
}