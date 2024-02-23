using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.ExtractionNames;

[TestFixture]
[TestOf(typeof(ExtractionNameService))]
public class ExtractionNameServiceTest : BaseDependencyTest
{
    private FileServiceMock _fileService = null!;
    private Mock<IProjectManager> _projectManager = null!;
    private ExtractionNameService _extractionNameService = null!;

    public override void Setup()
    {
        base.Setup();
        _fileService = new FileServiceMock();
        DependencyMock.AddMockDependency(_fileService);
        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _extractionNameService = new ExtractionNameService(DependencyMock.Object);
    }


    [Test]
    [TestCase("", @"C:\Images\Video.png")]
    [TestCase("Test", @"C:\Images\Test\Video.png")]
    [TestCase("test", @"C:\Images\test\Video.png")]
    [TestCase("a", @"C:\Images\a\Video.png")]
    public void GetImageNameReturnsCorrectName(string extractionName, string expectedPath)
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        imageExtraction.Name = extractionName;

        _projectManager.SetupGet(p => p.Project).Returns(ProjectExamples.GetExampleProject());
        _fileService.SetupFileExists(expectedPath, false);
        var result = _extractionNameService.GetImagePath(videoViewModel, imageExtraction);
        Assert.That(result, Is.EqualTo(expectedPath));
    }

    [Test]
    public void NameGetsIncremented()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();

        _projectManager.SetupGet(p => p.Project).Returns(ProjectExamples.GetExampleProject());
        _fileService.SetupFileExists(@"C:\Images\Video.png", true);
        _fileService.SetupFileExists(@"C:\Images\Video_1.png", true);

        var result = _extractionNameService.GetImagePath(videoViewModel, imageExtraction);
        Assert.That(result, Is.EqualTo(@"C:\Images\Video_2.png"));
    }

    [Test]
    public void NameGetsIncrementedWhenExtractionHasName()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        imageExtraction.Name = "Test";

        _projectManager.SetupGet(p => p.Project).Returns(ProjectExamples.GetExampleProject());
        _fileService.SetupFileExists(@"C:\Images\Test\Video.png", true);

        var result = _extractionNameService.GetImagePath(videoViewModel, imageExtraction);
        Assert.That(result, Is.EqualTo(@"C:\Images\Test\Video_1.png"));
    }

    [Test]
    public void TmpFolderWillBeUsedWhenNoProjectIsSpecified()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        _projectManager.SetupGet(p => p.Project).Returns((Project)null!);
        _fileService.SetupGetTmpFolder();

        int eventCount = 0;
        _extractionNameService.NoProjectSpecified += (_, _) => { eventCount++; };
        var result = _extractionNameService.GetImagePath(videoViewModel, imageExtraction);

        Assert.That(eventCount, Is.EqualTo(1));
        Assert.That(result, Is.EqualTo(@"C:\Tmp\Video.png"));
    }

    [Test]
    public void ExtractionFolderWillBeCreatedIfItDoesNotExist()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        _projectManager.SetupGet(p => p.Project).Returns(ProjectExamples.GetExampleProject());
        _fileService.SetupFileExists(@"C:\Images\Video.png", false);
        _fileService.SetupDirectoryExists(@"C:\Images", false);

        _extractionNameService.GetImagePath(videoViewModel, imageExtraction);
        _fileService.Verify(f => f.CreateDirectory(@"C:\Images"));
    }

    [Test]
    public void ExtractionNameFolderWillBeCreatedIfItDoesNotExist()
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        _projectManager.SetupGet(p => p.Project).Returns(ProjectExamples.GetExampleProject());
        _fileService.SetupFileExists(@"C:\Images\Video.png", false);
        _fileService.SetupDirectoryExists(@"C:\Images", true);
        _fileService.SetupDirectoryExists(@"C:\Images\Test", false);
        imageExtraction.Name = "Test";

        _extractionNameService.GetImagePath(videoViewModel, imageExtraction);
        _fileService.Verify(f => f.CreateDirectory(@"C:\Images\Test"));
    }

    [Test]
    [TestCase("", @"C:\Images\Video.mp4")]
    [TestCase("Test", @"C:\Images\Test\Video.mp4")]
    [TestCase("test", @"C:\Images\test\Video.mp4")]
    [TestCase("a", @"C:\Images\a\Video.mp4")]
    public void GetVideoNameReturnsCorrectName(string extractionName, string expectedPath)
    {
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        var videoExtraction = ExtractionExamples.GetVideoExtractionExample();
        videoExtraction.Name = extractionName;

        _projectManager.SetupGet(p => p.Project).Returns(ProjectExamples.GetExampleProject());
        _fileService.SetupFileExists(expectedPath, false);
        var result = _extractionNameService.GetVideoPath(videoViewModel, videoExtraction);
        Assert.That(result, Is.EqualTo(expectedPath));
    }
}