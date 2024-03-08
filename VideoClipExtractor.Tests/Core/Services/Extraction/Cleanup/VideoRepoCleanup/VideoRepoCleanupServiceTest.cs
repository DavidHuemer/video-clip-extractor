using Moq;
using VideoClipExtractor.Core.Services.Extraction.Cleanup.VideoRepoCleanup;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Data.Exceptions.VideoRepositoryExceptions;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.Cleanup.VideoRepoCleanup;

[TestFixture]
[TestOf(typeof(VideoRepoCleanupService))]
public class VideoRepoCleanupServiceTest : BaseDependencyTest
{
    private Mock<IVideoRepositoryManager> _videoRepositoryManagerMock = null!;
    private VideoRepoCleanupService _videoCleanupService = null!;

    public override void Setup()
    {
        base.Setup();
        _videoRepositoryManagerMock = DependencyMock.CreateMockDependency<IVideoRepositoryManager>();
        _videoCleanupService = new VideoRepoCleanupService(DependencyMock.Object);
    }

    [Test]
    public void NoVideoRepositoryThrowsException()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        _videoRepositoryManagerMock.SetupGet(x => x.VideoRepository)
            .Returns(null as IVideoRepository);

        Assert.Throws<VideoRepositoryNotSetException>(() => _videoCleanupService.CleanupVideo(videoViewModel));
    }

    [Test]
    public void VideoRepositoryRemovesVideo()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var videoRepositoryMock = new Mock<IVideoRepository>();
        _videoRepositoryManagerMock.SetupGet(x => x.VideoRepository)
            .Returns(videoRepositoryMock.Object);

        _videoCleanupService.CleanupVideo(videoViewModel);

        videoRepositoryMock.Verify(x => x.RemoveVideoByPath(videoViewModel.SourcePath), Times.Once);
    }
}