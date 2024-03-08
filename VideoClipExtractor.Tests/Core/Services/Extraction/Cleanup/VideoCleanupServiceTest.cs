using Moq;
using VideoClipExtractor.Core.Services.Extraction.Cleanup;
using VideoClipExtractor.Core.Services.Extraction.Cleanup.CacheCleanup;
using VideoClipExtractor.Core.Services.Extraction.Cleanup.VideoRepoCleanup;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.Cleanup;

[TestFixture]
[TestOf(typeof(VideoCleanupService))]
public class VideoCleanupServiceTest : BaseDependencyTest
{
    private Mock<IExtractionVerificationService> _extractionVerificationService = null!;
    private Mock<ICacheCleanupService> _cacheCleanupService = null!;
    private Mock<IVideoRepoCleanupService> _videoRepoCleanupService = null!;

    private VideoCleanupService _videoCleanupService = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionVerificationService = DependencyMock.CreateMockDependency<IExtractionVerificationService>();
        _cacheCleanupService = DependencyMock.CreateMockDependency<ICacheCleanupService>();
        _videoRepoCleanupService = DependencyMock.CreateMockDependency<IVideoRepoCleanupService>();

        _videoCleanupService = new VideoCleanupService(DependencyMock.Object);
    }

    [Test]
    public void ValidateExtractionResultsCalled()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();

        _videoCleanupService.CleanupVideo(videoViewModel, extractions);
        _extractionVerificationService.Verify(x => x.ValidateExtractionResults(extractions), Times.Once);
    }

    [Test]
    public void ValidateExtractionResultsErrorReturnsFailedResult()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();
        _extractionVerificationService.Setup(x => x.ValidateExtractionResults(extractions))
            .Throws(new Exception("Test"));

        var result = _videoCleanupService.CleanupVideo(videoViewModel, extractions);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.ExtractionResults.Count(), Is.EqualTo(4));
            Assert.That(result.CreatedBytes, Is.GreaterThan(0));
            Assert.That(result.SavedBytes, Is.EqualTo(0));
        });
    }

    [Test]
    public void CleanupCachedVideoCalled()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();

        _videoCleanupService.CleanupVideo(videoViewModel, extractions);
        _cacheCleanupService.Verify(x => x.CleanUpCachedVideo(videoViewModel), Times.Once);
    }

    [Test]
    public void CleanupCachedVideoErrorReturnsFailedResult()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();
        _cacheCleanupService.Setup(x => x.CleanUpCachedVideo(videoViewModel))
            .Throws(new Exception("Test"));

        var result = _videoCleanupService.CleanupVideo(videoViewModel, extractions);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.ExtractionResults.Count(), Is.EqualTo(4));
            Assert.That(result.CreatedBytes, Is.GreaterThan(0));
            Assert.That(result.SavedBytes, Is.EqualTo(0));
        });
    }

    [Test]
    [TestCase(VideoStatus.Unset)]
    [TestCase(VideoStatus.Skipped)]
    public void CleanupVideoNotCalledWhenNotReadyForExport(VideoStatus status)
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        videoViewModel.VideoStatus = status;
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();

        _videoCleanupService.CleanupVideo(videoViewModel, extractions);
        _videoRepoCleanupService.Verify(x => x.CleanupVideo(videoViewModel), Times.Never);
    }

    [Test]
    [TestCase(VideoStatus.Unset)]
    [TestCase(VideoStatus.Skipped)]
    public void NoSavedBytesWhenNotReadyForExport(VideoStatus status)
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        videoViewModel.VideoStatus = status;
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();
        var result = _videoCleanupService.CleanupVideo(videoViewModel, extractions);

        Assert.That(result.SavedBytes, Is.EqualTo(0));
    }

    [Test]
    public void CleanupVideoCalledWhenReadyForExport()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        videoViewModel.VideoStatus = VideoStatus.ReadyForExport;
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();

        _videoCleanupService.CleanupVideo(videoViewModel, extractions);
        _videoRepoCleanupService.Verify(x => x.CleanupVideo(videoViewModel), Times.Once);
    }

    [Test]
    public void CleanupVideoReturnsSavedBytes()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        videoViewModel.VideoStatus = VideoStatus.ReadyForExport;
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();
        var result = _videoCleanupService.CleanupVideo(videoViewModel, extractions);

        Assert.That(result.SavedBytes, Is.EqualTo(videoViewModel.Bytes));
    }
}