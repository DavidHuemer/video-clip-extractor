using Moq;
using VideoClipExtractor.Core.Services.Extraction.FileExtractionService;
using VideoClipExtractor.Core.Services.Extraction.ImageExtractions;
using VideoClipExtractor.Core.Services.Extraction.VideoExtractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.FileExtractionServiceTests;

[TestFixture]
[TestOf(typeof(FileExtractionService))]
public class FileExtractionServiceTest : BaseDependencyTest
{
    private Mock<IImageExtractionService> _imageExtractionService = null!;
    private Mock<IVideoExtractionService> _videoExtractionService = null!;

    private FileExtractionService _fileExtractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _imageExtractionService = DependencyMock.CreateMockDependency<IImageExtractionService>();
        _videoExtractionService = DependencyMock.CreateMockDependency<IVideoExtractionService>();
        _fileExtractionService = new FileExtractionService(DependencyMock.Object);
    }

    [Test]
    public async Task ImageExtractionIsCalled()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        await _fileExtractionService.Extract(videoViewModel, imageExtraction);
        _imageExtractionService.Verify(x =>
                x.Extract(videoViewModel, imageExtraction),
            Times.Once);
    }

    [Test]
    public async Task VideoExtractionIsCalled()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var videoExtraction = ExtractionExamples.GetVideoExtractionExample();

        await _fileExtractionService.Extract(videoViewModel, videoExtraction);
        _videoExtractionService.Verify(x =>
                x.Extract(videoViewModel, videoExtraction),
            Times.Once);
    }

    [Test]
    public async Task ImageExtractionErrorReturnsFailedResult()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        _imageExtractionService.Setup(x => x.Extract(videoViewModel, imageExtraction))
            .ThrowsAsync(new Exception("Test"));

        var result = await _fileExtractionService.Extract(videoViewModel, imageExtraction);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Path, Is.Empty);
            Assert.That(result.Bytes, Is.EqualTo(0));
            Assert.That(result.Message, Is.EqualTo("Test"));
        });
    }

    [Test]
    public async Task VideoExtractionErrorReturnsFailedResult()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var videoExtraction = ExtractionExamples.GetVideoExtractionExample();

        _videoExtractionService.Setup(x => x.Extract(videoViewModel, videoExtraction))
            .ThrowsAsync(new Exception("Test"));

        var result = await _fileExtractionService.Extract(videoViewModel, videoExtraction);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Path, Is.Empty);
            Assert.That(result.Bytes, Is.EqualTo(0));
            Assert.That(result.Message, Is.EqualTo("Test"));
        });
    }

    [Test]
    public async Task UnknownExtractionReturnsFailedResult()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();

        var videoExtractionMock = new Mock<IExtraction>();

        var result = await _fileExtractionService.Extract(videoViewModel, videoExtractionMock.Object);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Path, Is.Empty);
            Assert.That(result.Bytes, Is.EqualTo(0));
        });
    }
}