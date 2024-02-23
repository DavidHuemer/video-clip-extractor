using Moq;
using VideoClipExtractor.Core.Services.Extraction;
using VideoClipExtractor.Core.Services.Extraction.FileExtractionService;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Core.Services.Extraction;

[TestFixture]
[TestOf(typeof(ExtractionService))]
public class ExtractionServiceTest : BaseDependencyTest
{
    private FileServiceMock _fileService = null!;
    private Mock<IFileExtractionService> _fileExtractionService = null!;

    private ExtractionService _extractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _fileService = new FileServiceMock();
        DependencyMock.AddMockDependency(_fileService);
        _fileExtractionService = DependencyMock.CreateMockDependency<IFileExtractionService>();
        _extractionService = new ExtractionService(DependencyMock.Object);
    }


    [Test]
    [TestCase(VideoStatus.Unset)]
    [TestCase(VideoStatus.Skipped)]
    [TestCase(VideoStatus.Exported)]
    public void ExtractVideoWithoutReadyThrowsNotReadyException(VideoStatus videoStatus)
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoStatus = videoStatus;

        Assert.ThrowsAsync<VideoNotReadyForExportException>(() => _extractionService.Extract(video));
    }

    [Test]
    public void ExtractNotExistingVideoThrowsFileNotFoundException()
    {
        _fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(false);
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoStatus = VideoStatus.ReadyForExport;
        Assert.ThrowsAsync<FileNotFoundException>(() => _extractionService.Extract(video));
    }

    [Test]
    public async Task ExtractIsCalledForEachExtraction()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoStatus = VideoStatus.ReadyForExport;

        _fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);

        var imageExtractions = ExtractionExamples.GetImageExtractionExamples(5).ToList();
        imageExtractions.ForEach(x => video.ImageExtractions.Add(x));

        var videoExtractions = ExtractionExamples.GetVideoExtractionExamples(5).ToList();
        videoExtractions.ForEach(x => video.VideoExtractions.Add(x));

        await _extractionService.Extract(video);

        imageExtractions.ForEach(imageExtraction =>
            _fileExtractionService.Verify(y => y.Extract(video, imageExtraction), Times.Once));

        videoExtractions.ForEach(videoExtraction =>
            _fileExtractionService.Verify(y => y.Extract(video, videoExtraction), Times.Once));
    }

    // [Test]
    // public void StartImageExtractionsIsInvoked()
    // {
    //     _fileService.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
    //     var video = VideoExamples.GetVideoViewModelExample();
    //     video.VideoStatus = VideoStatus.ReadyForExport;
    //
    //     var called = false;
    //     _extractionService.StartImageExtractions += (_, _) => called = true;
    //     _extractionService.Extract(video);
    //
    //     Assert.IsTrue(called);
    // }
    [Test]
    public void METHOD()
    {
    }
}