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
    [TestCase(true, 1381, true)]
    [TestCase(false, 1381, false)]
    [TestCase(true, 0, false)]
    [TestCase(true, -1, false)]
    [TestCase(true, 1, true)]
    public void ExtractionSucceededReturnsCorrectValue(bool fileExists, int fileLength, bool expected)
    {
        var extractionPath = @"C:\Output\Image.png";
        _fileServiceMock.SetupFileExists(extractionPath, fileExists);
        _fileServiceMock.SetupGetFileSize(extractionPath, fileLength);

        var result = _extractionVerificationService.ExtractionSucceeded(@"C:\Output\Image.png");
        Assert.That(result, Is.EqualTo(expected));
    }
}