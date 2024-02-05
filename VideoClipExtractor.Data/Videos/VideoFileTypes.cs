namespace VideoClipExtractor.Data.Videos;

public class VideoFileTypes
{
    private static readonly string[] SupportedExtensions = ["mp4", "mov", "avi", "mkv", "wmv"];

    public static bool IsSupported(string fileExtension)
    {
        return SupportedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }
}