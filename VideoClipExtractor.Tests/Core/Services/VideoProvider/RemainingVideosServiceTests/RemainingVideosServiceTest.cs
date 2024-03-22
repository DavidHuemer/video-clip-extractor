using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Core.Services.VideoProvider.RemainingVideosService;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoProvider.RemainingVideosServiceTests;

[TestFixture]
[TestOf(typeof(RemainingVideosService))]
public class RemainingVideosServiceTest
{
    [SetUp]
    public void Setup()
    {
        _remainingVideosService = new RemainingVideosService();
    }

    private RemainingVideosService _remainingVideosService = null!;

    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(2, 0, 2)]
    [TestCase(10, 0, 10)]
    [TestCase(20, 0, 20)]
    [TestCase(20, 2, 18)]
    public void RemainingSourceVideosAreSetBySetup(int nrSourceVideos, int nrCachedVideos, int expected)
    {
        var project = ProjectExamples.GetEmptyProject();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(nrSourceVideos);
        project.Videos = sourceVideos;

        for (var i = 0; i < nrCachedVideos; i++)
        {
            var video = VideoExamples.GetVideoViewModelBySourceVideo(sourceVideos[i]);
            project.WorkingVideos.Add(video);
        }

        _remainingVideosService.Setup(project);
        Assert.That(_remainingVideosService.RemainingVideosCount, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextVideoReturnsNextVideo()
    {
        var project = ProjectExamples.GetEmptyProject();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(3);
        project.Videos = sourceVideos;

        _remainingVideosService.Setup(project);
        var nextVideo = _remainingVideosService.GetNextVideo();
        Assert.That(nextVideo, Is.EqualTo(sourceVideos[0]));
    }

    [Test]
    public void GetNextVideoRemovesVideoFromRemainingVideos()
    {
        var project = ProjectExamples.GetEmptyProject();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(3);
        project.Videos = sourceVideos;

        _remainingVideosService.Setup(project);
        _remainingVideosService.GetNextVideo();
        Assert.That(_remainingVideosService.RemainingVideosCount, Is.EqualTo(2));
    }

    [Test]
    public void GetNextVideoThrowsExceptionWhenNoRemainingVideos()
    {
        var project = ProjectExamples.GetEmptyProject();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(0);
        project.Videos = sourceVideos;

        _remainingVideosService.Setup(project);
        Assert.Throws<RemainingVideosEmptyException>(() => _remainingVideosService.GetNextVideo());
    }
}