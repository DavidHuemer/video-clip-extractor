using FFMpeg.Wrapper.Engine;
using Moq;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Core.Services.Extraction.VideoExtractions;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.VideoExtractions;

[TestFixture]
[TestOf(typeof(VideoExtractionService))]
public class VideoExtractionServiceTest : BaseDependencyTest
{
    private Mock<IExtractionNameService> _extractionNameService = null!;
    private Mock<IMpegEngine> _mpegEngine = null!;
    private Mock<IExtractionVerificationService> _extractionVerificationService = null!;
    private VideoExtractionService _videoExtractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionNameService = DependencyMock.CreateMockDependency<IExtractionNameService>();
        _mpegEngine = DependencyMock.CreateMockDependency<IMpegEngine>();
        _extractionVerificationService = DependencyMock.CreateMockDependency<IExtractionVerificationService>();
        _videoExtractionService = new VideoExtractionService(DependencyMock.Object);
    }

    [Test]
    public async Task VideoIsExtracted()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var videoExtraction = ExtractionExamples.GetVideoExtractionExample();

        _extractionNameService
            .Setup(x => x.GetVideoPath(videoViewModel, videoExtraction))
            .Returns(@"C\Output\Path\video.mp4");

        _extractionVerificationService
            .Setup(x => x.ExtractionSucceeded(@"C\Output\Path\video.mp4"))
            .Returns(true);

        await _videoExtractionService.Extract(videoViewModel, videoExtraction);

        _mpegEngine.Verify(
            x => x.ExtractVideoAsync(videoViewModel.LocalPath, @"C\Output\Path\video.mp4",
                videoExtraction.Begin.Position.Duration.TimeSpan, videoExtraction.Position.Duration.TimeSpan),
            Times.Once);
    }

    [Test]
    public void ExtractionFailedThrowsExtractionFailedException()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var videoExtraction = ExtractionExamples.GetVideoExtractionExample();

        _extractionNameService
            .Setup(x => x.GetVideoPath(videoViewModel, videoExtraction))
            .Returns(@"C\Output\Path\video.mp4");

        _extractionVerificationService
            .Setup(x => x.ExtractionSucceeded(@"C\Output\Path\video.mp4"))
            .Returns(false);

        Assert.ThrowsAsync<ExtractionFailedException>(() =>
            _videoExtractionService.Extract(videoViewModel, videoExtraction));
    }
}