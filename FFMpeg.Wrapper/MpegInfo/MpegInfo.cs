using System.Text.RegularExpressions;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.Data;
using FFMpeg.Wrapper.Engine;

namespace FFMpeg.Wrapper.MpegInfo;

[Transient]
public class MpegInfo(IDependencyProvider provider) : IMpegInfo
{
    private readonly IMpegEngine _mpegEngine = provider.GetDependency<IMpegEngine>();

    public async Task<VideoInfo> GetVideoInfoAsync(string inputPath)
    {
        var command = $"-i \"{inputPath}\"";
        var output = await _mpegEngine.RunCommandAsync(command);

        var duration = GetDurationFromOutput(output);
        var fps = GetFpsFromOutput(output);

        return new VideoInfo(duration, fps);
    }

    private TimeSpan GetDurationFromOutput(string output)
    {
        var pattern = @"Duration: (?<duration>\d\d:\d\d:\d\d\.\d\d)";
        var match = Regex.Match(output, pattern);

        if (!match.Success) throw new InvalidOperationException("Duration not found.");
        var duration = match.Groups["duration"].Value;
        return TimeSpan.Parse(duration);
    }

    private double GetFpsFromOutput(string output)
    {
        var pattern = @"(?<fps>\d+\.?\d*) fps";
        var match = Regex.Match(output, pattern);

        if (!match.Success) throw new InvalidOperationException("FPS not found.");
        var fps = match.Groups["fps"].Value;
        return double.Parse(fps.Replace(".", ","));
    }
}