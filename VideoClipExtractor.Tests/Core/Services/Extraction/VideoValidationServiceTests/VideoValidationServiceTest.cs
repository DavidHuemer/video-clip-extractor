using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Services.Extraction.VideoValidationService;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.VideoValidationServiceTests;

[TestFixture]
[TestOf(typeof(VideoValidationService))]
public class VideoValidationServiceTest : BaseDependencyTest
{
    private Mock<IFileService> _fileService = null!;
    private VideoValidationService _videoValidationService = null!;

    public override void Setup()
    {
        base.Setup();
        _fileService = DependencyMock.CreateMockDependency<IFileService>();
        _videoValidationService = new VideoValidationService(DependencyMock.Object);
    }

    [Test]
    public void VideoNotReadyExceptionIsThrownWhenVideoIsSkipped()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoStatus = VideoStatus.Skipped;
        Assert.Throws<VideoNotReadyForExportException>(() => _videoValidationService.ValidateVideoForExtraction(video));
    }

    [Test]
    public void FileNotFoundExceptionIsThrownWhenFileDoesNotExist()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _fileService.Setup(x => x.FileExists(video.LocalPath)).Returns(false);
        Assert.Throws<FileNotFoundException>(() => _videoValidationService.ValidateVideoForExtraction(video));
    }

    [Test]
    public void VideoIsReadyForExtraction()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _fileService.Setup(x => x.FileExists(video.LocalPath)).Returns(true);
        _videoValidationService.ValidateVideoForExtraction(video);
    }
}