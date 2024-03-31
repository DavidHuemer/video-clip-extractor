using Moq;
using VideoClipExtractor.Core.Services.Extraction.ExtractionRunnerService;
using VideoClipExtractor.Core.Services.Extraction.FileExtractionService;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.ExtractionRunnerServiceTest;

[TestFixture]
[TestOf(typeof(ExtractionRunnerService))]
public class ExtractionRunnerServiceTest : BaseDependencyTest
{
    private Mock<IFileExtractionService> _fileExtractionService = null!;
    private ExtractionRunnerService _extractionRunnerService = null!;

    public override void Setup()
    {
        base.Setup();
        _fileExtractionService = DependencyMock.CreateMockDependency<IFileExtractionService>();
        _extractionRunnerService = new ExtractionRunnerService(DependencyMock.Object);
    }


    [Test]
    public async Task ExtractIsCalledForAllExtractions()
    {
        var video = VideoExamples.GetVideoViewModelExample();

        var imageExtractions = ExtractionExamples.GetImageExtractionExamples(5).ToList();
        imageExtractions.ForEach(x => video.ImageExtractions.Add(x));

        var videoExtractions = ExtractionExamples.GetVideoExtractionExamples(5).ToList();
        videoExtractions.ForEach(x => video.VideoExtractions.Add(x));

        await _extractionRunnerService.ExtractVideo(video);
        var extractions = video.GetExtractions().ToList();

        extractions.ForEach(extraction =>
            _fileExtractionService.Verify(y => y.Extract(video, extraction), Times.Once));
    }

    [Test]
    public async Task ExtractionResultIsSetWhenResultReturned()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        video.ImageExtractions.Add(imageExtraction);

        var result = ExtractionResultExamples.GetSuccessResultExample();
        _fileExtractionService.Setup(x => x.Extract(video, imageExtraction))
            .ReturnsAsync(result);

        await _extractionRunnerService.ExtractVideo(video);
        Assert.That(imageExtraction.Result, Is.EqualTo(result));
    }

    [Test]
    public async Task ExtractionResultIsSetWhenExceptionThrown()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        video.ImageExtractions.Add(imageExtraction);

        _fileExtractionService.Setup(x => x.Extract(video, imageExtraction))
            .ThrowsAsync(new Exception("Test"));

        await _extractionRunnerService.ExtractVideo(video);
        Assert.That(imageExtraction.Result!.Success, Is.False);
    }

    [Test]
    public async Task ExtractionResultsAreReturnedCorrectly()
    {
        var video = VideoExamples.GetVideoViewModelExample();

        var imageExtractions = ExtractionExamples.GetImageExtractionExamples(5).ToList();
        imageExtractions.ForEach(x => video.ImageExtractions.Add(x));

        var videoExtractions = ExtractionExamples.GetVideoExtractionExamples(5).ToList();
        videoExtractions.ForEach(x => video.VideoExtractions.Add(x));

        var extractionResults = await _extractionRunnerService.ExtractVideo(video);

        Assert.That(extractionResults, Has.Count.EqualTo(video.GetExtractions().Count()));
    }
}