using FFMpeg.Wrapper.Engine;
using Moq;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Core.Services.Extraction.ImageExtractions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.ImageExtractions;

[TestFixture]
[TestOf(typeof(ImageExtractionService))]
public class ImageExtractionServiceTest : BaseExtractionServiceTest
{
    private Mock<IMpegEngine> _mpegEngine = null!;
    private ImageExtraction _imageExtraction = null!;

    private ImageExtractionService _imageExtractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _mpegEngine = DependencyMock.CreateMockDependency<IMpegEngine>();
        ExtractionVerificationService = DependencyMock.CreateMockDependency<IExtractionVerificationService>();

        _imageExtraction = ExtractionExamples.GetImageExtractionExample();
        _imageExtractionService = new ImageExtractionService(DependencyMock.Object);
        ExtractionPath = @"C\Output\Path\image.png";
    }

    [Test]
    public async Task ImageIsExtracted()
    {
        SetupImagePath();
        SetupVerificationService();

        await _imageExtractionService.Extract(VideoViewModel, _imageExtraction);

        _mpegEngine.Verify(
            x => x.ExtractImageAsync(VideoViewModel.LocalPath, ExtractionPath,
                _imageExtraction.Position.Duration.TimeSpan), Times.Once);
    }

    [Test]
    public async Task VerificationServiceIsCalled()
    {
        SetupImagePath();
        SetupVerificationService();

        await _imageExtractionService.Extract(VideoViewModel, _imageExtraction);
        ExtractionVerificationService.Verify(x => x.CheckExtraction(ExtractionPath), Times.Once);
    }

    [Test]
    public async Task ExtractionPathErrorReturnsFailedResult()
    {
        ExtractionNameService
            .Setup(x => x.GetImagePath(VideoViewModel, _imageExtraction))
            .Throws(new Exception());

        var result = await _imageExtractionService.Extract(VideoViewModel, _imageExtraction);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Path, Is.Empty);
            Assert.That(result.Bytes, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task ExtractionErrorReturnsFailedResult()
    {
        SetupImagePath();
        _mpegEngine
            .Setup(x => x.ExtractImageAsync(VideoViewModel.LocalPath, ExtractionPath,
                _imageExtraction.Position.Duration.TimeSpan))
            .Throws(new Exception());

        var result = await _imageExtractionService.Extract(VideoViewModel, _imageExtraction);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Path, Is.EqualTo(ExtractionPath));
            Assert.That(result.Bytes, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task VerificationErrorReturnsFailedResult()
    {
        SetupImagePath();
        ExtractionVerificationService
            .Setup(x => x.CheckExtraction(ExtractionPath))
            .Throws(new Exception());

        var result = await _imageExtractionService.Extract(VideoViewModel, _imageExtraction);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Path, Is.EqualTo(ExtractionPath));
            Assert.That(result.Bytes, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task VerificationServiceResultReturned()
    {
        var expectedResult = ExtractionResultExamples.GetSuccessResultExample();

        SetupImagePath();
        ExtractionVerificationService
            .Setup(x => x.CheckExtraction(ExtractionPath))
            .Returns(expectedResult);

        var result = await _imageExtractionService.Extract(VideoViewModel, _imageExtraction);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private void SetupImagePath() =>
        ExtractionNameService
            .Setup(x => x.GetImagePath(VideoViewModel, _imageExtraction))
            .Returns(ExtractionPath);
}