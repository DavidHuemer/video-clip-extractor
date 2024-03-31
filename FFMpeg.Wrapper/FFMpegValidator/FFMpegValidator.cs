using System.IO;
using System.Text.RegularExpressions;

namespace FFMpeg.Wrapper.FFMpegValidator;

public static class FfMpegValidator
{
    public static void Validate(string output)
    {
        CheckNotExisting(output);
        CheckAlreadyExisting(output);
    }

    private static void CheckNotExisting(string output)
    {
        var pattern = @"\[fatal\] Error opening input files: No such file or directory";
        var match = Regex.Match(output, pattern);
        if (match.Success)
            throw new FileNotFoundException("Error opening input files: No such file or directory");
    }

    private static void CheckAlreadyExisting(string output)
    {
        var pattern = @"\[fatal\] File '(?<filename>.*)' already exists.";
        var match = Regex.Match(output, pattern);
        if (match.Success)
            throw new IOException($"File '{match.Groups["filename"].Value}' already exists.");
    }
}