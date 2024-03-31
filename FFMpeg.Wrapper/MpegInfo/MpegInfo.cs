using System.Text.RegularExpressions;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.Engine;

namespace FFMpeg.Wrapper.MpegInfo;

[Transient]
public class MpegInfo(IDependencyProvider provider) : IMpegInfo
{
    private readonly IMpegEngine _mpegEngine = provider.GetDependency<IMpegEngine>();

    public async Task<TimeSpan> GetDurationAsync(string inputPath)
    {
        var command = $"-i \"{inputPath}\" -f null -";
        var output = await _mpegEngine.RunCommandAsync(command);

        var pattern = @"Duration: (?<duration>\d\d:\d\d:\d\d\.\d\d)";
        var match = Regex.Match(output, pattern);

        if (!match.Success) throw new InvalidOperationException("Duration not found.");
        var duration = match.Groups["duration"].Value;
        return TimeSpan.Parse(duration);
    }
}