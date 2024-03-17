using BaseUI.Exceptions.Basics;
using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoCaching.CacheRunnerTests;

[TestFixture]
[TestOf(typeof(CacheRunner))]
public class CacheRunnerTest : BaseDependencyTest
{
    private Mock<IFileService> _fileService = null!;
    private Mock<IVideoRepository> _repo = null!;
    private CacheRunner _cacheRunner = null!;

    public override void Setup()
    {
        base.Setup();
        _fileService = DependencyMock.CreateMockDependency<IFileService>();
        _repo = new Mock<IVideoRepository>();
        _cacheRunner = new CacheRunner(DependencyMock.Object);
    }


    [Test]
    public void StoreVideoWithoutSetupThrowsSetupException()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        Assert.Throws<NotSetupException>(() => _cacheRunner.StoreVideo(sourceVideo));
    }

    [Test]
    [TestCase("Video.mp4")]
    [TestCase("Video.avi")]
    [TestCase("az_103854864684.mp4")]
    public void StoreVideoChecksFileExistsWithCorrectPath(string videoName)
    {
        var project = ProjectExamples.GetEmptyProject();
        _cacheRunner.Setup(project, _repo.Object);

        var sourceVideo = SourceVideoExamples.GetSourceVideoExampleByFullName(videoName);
        _cacheRunner.StoreVideo(sourceVideo);

        var expectedLocalPath = $@"{project.ImageDirectory}\{videoName}";
        _fileService.Verify(x => x.FileExists(expectedLocalPath), Times.Once);
    }

    [Test]
    [TestCase("Video.mp4")]
    [TestCase("Video.avi")]
    [TestCase("az_103854864684.mp4")]
    public void FileDeletedWhenExisting(string videoName)
    {
        var project = ProjectExamples.GetEmptyProject();
        _cacheRunner.Setup(project, _repo.Object);

        var expectedLocalPath = $@"{project.ImageDirectory}\{videoName}";
        _fileService.Setup(x => x.FileExists(expectedLocalPath)).Returns(true);

        var sourceVideo = SourceVideoExamples.GetSourceVideoExampleByFullName(videoName);
        _cacheRunner.StoreVideo(sourceVideo);

        _fileService.Verify(x => x.DeleteFile(expectedLocalPath), Times.Once);
    }

    [Test]
    [TestCase("Video.mp4")]
    [TestCase("Video.avi")]
    [TestCase("az_103854864684.mp4")]
    public void FileIsCopied(string videoName)
    {
        var project = ProjectExamples.GetEmptyProject();
        _cacheRunner.Setup(project, _repo.Object);

        var sourceVideo = SourceVideoExamples.GetSourceVideoExampleByFullName(videoName);
        _cacheRunner.StoreVideo(sourceVideo);

        var expectedLocalPath = $@"{project.ImageDirectory}\{videoName}";
        _repo.Verify(x => x.CopyFileByPath(sourceVideo.Path, expectedLocalPath), Times.Once);
    }
}