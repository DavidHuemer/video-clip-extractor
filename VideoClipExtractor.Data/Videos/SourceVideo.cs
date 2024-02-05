using System.IO;
using System.Runtime.Versioning;
using MediaDevices;

namespace VideoClipExtractor.Data.Videos;

/// <summary>
///     Represents a video file inside the video repository.
/// </summary>
[Serializable]
public class SourceVideo
{
    public SourceVideo(FileInfo file)
    {
        Name = file.Name;
        Path = file.FullName;
        Extension = file.Extension;
        Size = (int)file.Length;
    }

    [SupportedOSPlatform("windows")]
    public SourceVideo(MediaFileInfo file)
    {
        Name = file.Name;
        Path = file.FullName;
        Extension = file.Name.Split('.').Last();
        Size = (int)file.Length;
    }

    public SourceVideo()
    {
    }

    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
    public string Extension { get; set; } = "";
    public int Size { get; set; }
    public bool Checked { get; set; } = false;

    public override string ToString()
    {
        return $"{Name} - {Size} bytes";
    }
}