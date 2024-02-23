using Moq;
using VideoClipExtractor.Core.Services.Extraction.FileExtractionService;
using VideoClipExtractor.Core.Services.Extraction.ImageExtractions;
using VideoClipExtractor.Core.Services.Extraction.VideoExtractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

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
    public async Task ExceptionThrown()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();

        var extractionMock = new Mock<IExtraction>();

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _fileExtractionService.Extract(videoViewModel, extractionMock.Object));
    }
}