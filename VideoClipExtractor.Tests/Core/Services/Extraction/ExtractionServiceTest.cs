using Moq;
using VideoClipExtractor.Core.Services.Extraction;
using VideoClipExtractor.Core.Services.Extraction.Cleanup;
using VideoClipExtractor.Core.Services.Extraction.ExtractionRunnerService;
using VideoClipExtractor.Core.Services.Extraction.VideoValidationService;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction;

[TestFixture]
[TestOf(typeof(ExtractionService))]
public class ExtractionServiceTest : BaseDependencyTest
{
    private Mock<IVideoValidationService> _videoValidationService = null!;
    private Mock<IExtractionRunnerService> _extractionRunnerService = null!;
    private Mock<IVideoCleanupService> _videoCleanupService = null!;

    private ExtractionService _extractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _videoValidationService = DependencyMock.CreateMockDependency<IVideoValidationService>();
        _extractionRunnerService = DependencyMock.CreateMockDependency<IExtractionRunnerService>();
        _videoCleanupService = DependencyMock.CreateMockDependency<IVideoCleanupService>();
        _extractionService = new ExtractionService(DependencyMock.Object);
    }

    [Test]
    public async Task VideoValidationServiceIsCalledWithVideo()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        await _extractionService.Extract(video);
        _videoValidationService.Verify(x => x.ValidateVideoForExtraction(video), Times.Once);
    }

    [Test]
    public async Task VideoValidationErrorReturnsFailingResult()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _videoValidationService.Setup(x => x.ValidateVideoForExtraction(video)).Throws<Exception>();

        var result = await _extractionService.Extract(video);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.ExtractionResults, Is.Empty);
            Assert.That(result.CreatedBytes, Is.EqualTo(0));
            Assert.That(result.SavedBytes, Is.EqualTo(0));
            Assert.That(result.ByteDifference, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task ExtractionRunnerServiceIsCalledWithVideo()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        await _extractionService.Extract(video);
        _extractionRunnerService.Verify(x => x.ExtractVideo(video), Times.Once);
    }

    [Test]
    public void ExtractionRunnerErrorReturnsFailingResult()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _extractionRunnerService.Setup(x => x.ExtractVideo(video)).Throws<Exception>();

        var result = _extractionService.Extract(video).Result;
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.ExtractionResults, Is.Empty);
            Assert.That(result.CreatedBytes, Is.EqualTo(0));
            Assert.That(result.SavedBytes, Is.EqualTo(0));
            Assert.That(result.ByteDifference, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task VideoCleanupServiceIsCalledWithVideoAndExtractionResults()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var results = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();

        _extractionRunnerService.Setup(x => x.ExtractVideo(video)).ReturnsAsync(results);
        await _extractionService.Extract(video);
        _videoCleanupService.Verify(x => x.CleanupVideo(video, results), Times.Once);
    }

    [Test]
    public async Task VideoCleanupErrorReturnsFailingResult()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var results = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();

        _extractionRunnerService.Setup(x => x.ExtractVideo(video)).ReturnsAsync(results);
        _videoCleanupService.Setup(x => x.CleanupVideo(video, results)).Throws<Exception>();

        var result = await _extractionService.Extract(video);

        Assert.Multiple(() =>
        {
            Assert.That(result.ExtractionResults.Count(), Is.EqualTo(results.Count));
            Assert.That(result.ExtractionResults, Is.EqualTo(results));
            Assert.IsFalse(result.Success);
            Assert.That(result.CreatedBytes, Is.GreaterThan(0));
        });
    }
}