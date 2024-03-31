using System.Diagnostics;
using BaseUI.Services.Provider.Attributes;
using FFMpeg.Wrapper.FFMpegValidator;

namespace FFMpeg.Wrapper.Engine;

[Singleton]
public class MpegEngine(string ffMpegPath) : IMpegEngine
{
    public MpegEngine() : this(@"C:\Development\tools\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe")
    {
    }

    public string FfMpegPath { get; } = ffMpegPath;

    public async Task<string> RunCommandAsync(string command)
    {
        command = $"{command}";

        var psi = CreateProcessStartInfo(command);

        using var process = new Process();
        process.StartInfo = psi;
        process.Start();
        var errors = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();

        FfMpegValidator.Validate(errors);
        return errors;
    }

    private ProcessStartInfo CreateProcessStartInfo(string command)
    {
        return new ProcessStartInfo
        {
            FileName = FfMpegPath,
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
    }
}