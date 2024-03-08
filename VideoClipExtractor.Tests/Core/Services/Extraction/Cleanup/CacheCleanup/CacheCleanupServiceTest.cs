using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Services.Extraction.Cleanup.CacheCleanup;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.Cleanup.CacheCleanup;

[TestFixture]
[TestOf(typeof(CacheCleanupService))]
public class CacheCleanupServiceTest : BaseDependencyTest
{
    private Mock<IFileService> _fileServiceMock = null!;

    private CacheCleanupService _cacheCleanupService = null!;

    public override void Setup()
    {
        base.Setup();
        _fileServiceMock = DependencyMock.CreateMockDependency<IFileService>();
        _cacheCleanupService = new CacheCleanupService(DependencyMock.Object);
    }


    [Test]
    public void FileExistsCalled()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _cacheCleanupService.CleanUpCachedVideo(video);
        _fileServiceMock.Verify(f => f.FileExists(video.LocalPath), Times.Once);
    }

    [Test]
    public void DeleteFileNotCalledWhenFileDoesNotExist()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _fileServiceMock.Setup(f => f.FileExists(video.LocalPath)).Returns(false);
        _cacheCleanupService.CleanUpCachedVideo(video);
        _fileServiceMock.Verify(f => f.DeleteFile(video.LocalPath), Times.Never);
    }

    [Test]
    public void DeleteFileCalledWhenFileExists()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _fileServiceMock.Setup(f => f.FileExists(video.LocalPath)).Returns(true);
        _cacheCleanupService.CleanUpCachedVideo(video);
        _fileServiceMock.Verify(f => f.DeleteFile(video.LocalPath), Times.Once);
    }
}