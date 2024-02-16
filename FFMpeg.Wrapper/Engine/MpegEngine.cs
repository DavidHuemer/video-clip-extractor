using System.Diagnostics;
using System.IO;

namespace FFMpeg.Wrapper.Engine;

public class MpegEngine(string ffMpegPath) : IMpegEngine
{
    public async Task ExtractImageAsync(string inputVideoPath, string outputImagePath, TimeSpan timeSpan)
    {
        if (timeSpan < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(timeSpan), "Time span cannot be negative.");

        var command = $"-i {inputVideoPath} -ss {timeSpan} -frames:v 1 -n -q:v 2 {outputImagePath}";

        var psi = CreateProcessStartInfo(command);

        using var process = new Process();
        process.StartInfo = psi;
        process.Start();
        var errors = await process.StandardError.ReadToEndAsync();
        CheckExceptions(errors, inputVideoPath, outputImagePath);
        await process.WaitForExitAsync();
    }

    public async Task ExtractVideoAsync(string inputVideoPath, string outputVideoPath, TimeSpan startTime,
        TimeSpan duration)
    {
        if (startTime < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(startTime), "Start time cannot be negative.");

        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration cannot be negative.");

        var command = $"-i {inputVideoPath} -ss {startTime} -t {duration} -n -c copy {outputVideoPath}";
        var psi = CreateProcessStartInfo(command);

        using var process = new Process();
        process.StartInfo = psi;
        process.Start();
        var errors = await process.StandardError.ReadToEndAsync();
        CheckExceptions(errors, inputVideoPath, outputVideoPath);
        await process.WaitForExitAsync();
    }

    private ProcessStartInfo CreateProcessStartInfo(string command)
    {
        return new ProcessStartInfo
        {
            FileName = ffMpegPath,
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
    }

    private void CheckExceptions(string errors, string input, string output)
    {
        var alreadyExistErrorLine = $"File '{output}' already exists. Exiting.";
        if (errors.Contains(alreadyExistErrorLine))
            throw new IOException($"File '{output}' already exists.");

        var notExistingErrorLine = "Error opening input files: No such file or directory";
        if (errors.Contains(notExistingErrorLine))
            throw new FileNotFoundException($"File '{input}' does not exist.");
    }
}