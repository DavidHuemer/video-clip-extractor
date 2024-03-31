using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.Engine;

namespace FFMpeg.Wrapper.MpegExtraction;

[Transient]
public class MpegExtractionRunner(IDependencyProvider provider) : IMpegExtractionRunner
{
    private readonly IMpegEngine _mpegEngine = provider.GetDependency<IMpegEngine>();

    public async Task ExtractImageAsync(string inputVideoPath, string outputImagePath, TimeSpan position)
    {
        if (position < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(position), "Time span cannot be negative.");

        var command = $"-i {inputVideoPath} -ss {position} -frames:v 1 -n -q:v 2 {outputImagePath}";

        var x = await _mpegEngine.RunCommandAsync(command);
        Console.WriteLine(x);
    }

    public async Task ExtractVideoAsync(string inputPath, string outputPath, TimeSpan begin, TimeSpan duration)
    {
        if (begin < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(begin), "Start time cannot be negative.");

        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration cannot be negative.");

        var command = $"-i {inputPath} -ss {begin} -t {duration} -n -c copy {outputPath}";
        await _mpegEngine.RunCommandAsync(command);
    }
}