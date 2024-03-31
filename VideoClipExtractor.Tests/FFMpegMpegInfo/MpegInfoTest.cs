using FFMpeg.Wrapper.Engine;
using FFMpeg.Wrapper.MpegInfo;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Extensions;

namespace VideoClipExtractor.Tests.FFMpegMpegInfo;

[TestFixture]
[TestOf(typeof(MpegInfo))]
public class MpegInfoTest : BaseDependencyTest
{
    [TearDown]
    public void TearDown()
    {
        _tempFolder.RemoveFolder();
    }

    private TestFolder _tempFolder = null!;
    private const string OriginalSourceVideoPath = @"C:\tmp\az_recorder_20230611_170334.mp4";
    private string _sourceVideoPath = null!;

    private MpegEngine _mpegEngine = null!;
    private MpegInfo _mpegInfo = null!;

    public override void Setup()
    {
        base.Setup();
        _tempFolder = new TestFolder(nameof(MpegInfoTest));
        _sourceVideoPath = _tempFolder.GetFilePath("source.mp4");
        File.Copy(OriginalSourceVideoPath, _sourceVideoPath);

        var mpegPath = @"C:\Development\tools\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe";
        _mpegEngine = new MpegEngine(mpegPath);
        DependencyMock.AddDependency<IMpegEngine>(_mpegEngine);
        _mpegInfo = new MpegInfo(DependencyMock.Object);
    }

    [Test]
    public async Task GetDurationReturnsCorrectDuration()
    {
        var duration = await _mpegInfo.GetDurationAsync(_sourceVideoPath);
        Assert.That(duration, Is.GreaterThan(TimeSpan.Zero));
    }

    [Test]
    public void GetDurationFromNonExistingFileThrowsFileNotFoundException()
    {
        var nonExistingPath = _tempFolder.GetFilePath("non-existing.mp4");
        Assert.ThrowsAsync<FileNotFoundException>(() => _mpegInfo.GetDurationAsync(nonExistingPath));
    }

    [Test]
    public void GetDurationFromImageThrowsInvalidOperationException()
    {
        var imagePath = _tempFolder.GetFilePath("image.jpg");
        File.Create(imagePath).Close();
        Assert.ThrowsAsync<InvalidOperationException>(() => _mpegInfo.GetDurationAsync(imagePath));
    }
}