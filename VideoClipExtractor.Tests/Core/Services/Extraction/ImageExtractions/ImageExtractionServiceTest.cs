using FFMpeg.Wrapper.Engine;
using Moq;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Core.Services.Extraction.ImageExtractions;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.ImageExtractions;

[TestFixture]
[TestOf(typeof(ImageExtractionService))]
public class ImageExtractionServiceTest : BaseDependencyTest
{
    private Mock<IExtractionNameService> _extractionNameService = null!;
    private Mock<IMpegEngine> _mpegEngine = null!;
    private Mock<IExtractionVerificationService> _extractionVerificationService = null!;

    private ImageExtractionService _imageExtractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionNameService = DependencyMock.CreateMockDependency<IExtractionNameService>();
        _mpegEngine = DependencyMock.CreateMockDependency<IMpegEngine>();
        _extractionVerificationService = DependencyMock.CreateMockDependency<IExtractionVerificationService>();
        _imageExtractionService = new ImageExtractionService(DependencyMock.Object);
    }


    [Test]
    public async Task ImageIsExtracted()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        _extractionNameService
            .Setup(x => x.GetImagePath(videoViewModel, imageExtraction))
            .Returns(@"C\Output\Path\image.png");

        _extractionVerificationService
            .Setup(x => x.ExtractionSucceeded(@"C\Output\Path\image.png"))
            .Returns(true);

        await _imageExtractionService.Extract(videoViewModel, imageExtraction);

        _mpegEngine.Verify(
            x => x.ExtractImageAsync(videoViewModel.LocalPath, @"C\Output\Path\image.png",
                imageExtraction.Position.Duration.TimeSpan), Times.Once);
    }

    [Test]
    public void ExtractionFailedThrowsExtractionFailedException()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        _extractionNameService
            .Setup(x => x.GetImagePath(videoViewModel, imageExtraction))
            .Returns(@"C\Output\Path\image.png");

        _extractionVerificationService
            .Setup(x => x.ExtractionSucceeded(@"C\Output\Path\image.png"))
            .Returns(false);

        Assert.ThrowsAsync<ExtractionFailedException>(async () =>
            await _imageExtractionService.Extract(videoViewModel, imageExtraction));
    }
}