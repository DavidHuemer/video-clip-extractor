using FFMpeg.Wrapper.Engine;
using VideoClipExtractor.Tests.Basics.Extensions;

namespace VideoClipExtractor.Tests.FFMpeg.Engine;

[TestFixture]
[NonParallelizable]
[TestOf(typeof(MpegEngine))]
public class MpegEngineTest
{
    [SetUp]
    public void Setup()
    {
        _tempFolder = new TestFolder("MpegEngineTest");
        _sourceVideoPath = _tempFolder.GetFilePath("source.mp4");
        File.Copy(OriginalSourceVideoPath, _sourceVideoPath);
        _outputImagePath = _tempFolder.GetFilePath("output.jpg");
        _outputVideoPath = _tempFolder.GetFilePath("output.mp4");
        var mpegPath = @"C:\Development\tools\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe";
        _mpegEngine = new MpegEngine(mpegPath);
    }

    [TearDown]
    public void TearDown()
    {
        _tempFolder.RemoveFolder();
    }

    private const string OriginalSourceVideoPath = @"C:\tmp\az_recorder_20230611_170334.mp4";

    private MpegEngine _mpegEngine = null!;
    private TestFolder _tempFolder = null!;

    private string _sourceVideoPath = null!;
    private string _outputImagePath = null!;
    private string _outputVideoPath = null!;

    [Test]
    public async Task ExtractImageAsync()
    {
        var timeSpan = TimeSpan.FromSeconds(10);
        await _mpegEngine.ExtractImageAsync(_sourceVideoPath, _outputImagePath, timeSpan);

        Assert.IsTrue(File.Exists(_outputImagePath));
    }

    [Test]
    public async Task ExtractAlreadyExistingImageThrowsIoException()
    {
        var timeSpan = TimeSpan.FromSeconds(10);
        await _mpegEngine.ExtractImageAsync(_sourceVideoPath, _outputImagePath, timeSpan);
        Assert.IsTrue(File.Exists(_outputImagePath));
        Assert.ThrowsAsync<IOException>(() =>
            _mpegEngine.ExtractImageAsync(_sourceVideoPath, _outputImagePath, timeSpan));
    }

    [Test]
    public void ExtractImageFromNonExistingVideoThrowsFileNotFoundException()
    {
        var timeSpan = TimeSpan.FromSeconds(10);
        var nonExistingVideoPath = Path.Combine(_tempFolder.FolderPath, "non-existing.mp4");
        Assert.ThrowsAsync<FileNotFoundException>(() =>
            _mpegEngine.ExtractImageAsync(nonExistingVideoPath, _outputImagePath, timeSpan));
    }

    [Test]
    public void ExtractImageWithNegativeTimeSpanThrowsArgumentOutOfRangeException()
    {
        var timeSpan = TimeSpan.FromSeconds(-10);
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _mpegEngine.ExtractImageAsync(_sourceVideoPath, _outputImagePath, timeSpan));
    }

    [Test]
    public async Task ExtractVideoAsync()
    {
        var startTime = TimeSpan.FromSeconds(10);
        var duration = TimeSpan.FromSeconds(5);
        await _mpegEngine.ExtractVideoAsync(_sourceVideoPath, _outputVideoPath, startTime, duration);

        Assert.IsTrue(File.Exists(_outputVideoPath));
    }

    [Test]
    public async Task ExtractAlreadyExistingVideoThrowsIoException()
    {
        var startTime = TimeSpan.FromSeconds(10);
        var duration = TimeSpan.FromSeconds(5);
        await _mpegEngine.ExtractVideoAsync(_sourceVideoPath, _outputVideoPath, startTime, duration);
        Assert.IsTrue(File.Exists(_outputVideoPath));
        Assert.ThrowsAsync<IOException>(() =>
            _mpegEngine.ExtractVideoAsync(_sourceVideoPath, _outputVideoPath, startTime, duration));
    }

    [Test]
    public void ExtractVideoFromNonExistingVideoThrowsFileNotFoundException()
    {
        var startTime = TimeSpan.FromSeconds(10);
        var duration = TimeSpan.FromSeconds(5);
        var nonExistingVideoPath = _tempFolder.GetFilePath("non-existing.mp4");
        Assert.ThrowsAsync<FileNotFoundException>(() =>
            _mpegEngine.ExtractVideoAsync(nonExistingVideoPath, _outputVideoPath, startTime, duration));
    }

    [Test]
    public void ExtractVideoWithNegativeStartTimeThrowsArgumentOutOfRangeException()
    {
        var startTime = TimeSpan.FromSeconds(-10);
        var duration = TimeSpan.FromSeconds(5);
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _mpegEngine.ExtractVideoAsync(_sourceVideoPath, _outputVideoPath, startTime, duration));
    }

    [Test]
    public void ExtractVideoWithNegativeDurationThrowsArgumentOutOfRangeException()
    {
        var startTime = TimeSpan.FromSeconds(10);
        var duration = TimeSpan.FromSeconds(-5);
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _mpegEngine.ExtractVideoAsync(_sourceVideoPath, _outputVideoPath, startTime, duration));
    }
}