using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.ExtractionVerificationServiceTests;

[TestFixture]
[TestOf(typeof(ExtractionVerificationService))]
public class ExtractionVerificationServiceTest : BaseDependencyTest
{
    private FileServiceMock _fileServiceMock = null!;
    private ExtractionVerificationService _extractionVerificationService = null!;

    public override void Setup()
    {
        base.Setup();
        _fileServiceMock = new FileServiceMock();
        DependencyMock.AddMockDependency(_fileServiceMock);
        _extractionVerificationService = new ExtractionVerificationService(DependencyMock.Object);
    }

    [Test]
    public void CheckExtractionWithNotExistingReturnsFailedResults()
    {
        var extractionPath = @"C:\Output\Image.png";
        _fileServiceMock.SetupFileExists(extractionPath, false);

        var result = _extractionVerificationService.CheckExtraction(extractionPath);

        Assert.Multiple(() =>
        {
            Assert.That(result.Path, Is.EqualTo(extractionPath));
            Assert.That(result.Success, Is.False);
            Assert.That(result.Bytes, Is.EqualTo(0));
            Assert.That(result.Message, Is.EqualTo("The extraction was not found"));
        });
    }

    [Test]
    public void CheckExtractionWithZeroSizeReturnsFailedResults()
    {
        var extractionPath = @"C:\Output\Image.png";
        _fileServiceMock.SetupFileExists(extractionPath, true);
        _fileServiceMock.SetupGetFileSize(extractionPath, 0);

        var result = _extractionVerificationService.CheckExtraction(extractionPath);

        Assert.Multiple(() =>
        {
            Assert.That(result.Path, Is.EqualTo(extractionPath));
            Assert.That(result.Success, Is.False);
            Assert.That(result.Bytes, Is.EqualTo(0));
            Assert.That(result.Message, Is.Not.Empty);
        });
    }

    [Test]
    public void CheckExtractionReturnsSuccessResult()
    {
        var extractionPath = @"C:\Output\Image.png";
        _fileServiceMock.SetupFileExists(extractionPath, true);
        _fileServiceMock.SetupGetFileSize(extractionPath, 40);

        var result = _extractionVerificationService.CheckExtraction(extractionPath);

        Assert.Multiple(() =>
        {
            Assert.That(result.Path, Is.EqualTo(extractionPath));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Bytes, Is.EqualTo(40));
            Assert.That(result.Message, Is.Empty);
        });
    }
}