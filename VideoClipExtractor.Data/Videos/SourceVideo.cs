using System.IO;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
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
        Path = file.FullName;
        Size = (int)file.Length;
    }

    [SupportedOSPlatform("windows")]
    public SourceVideo(MediaFileInfo file)
    {
        Path = file.FullName;
        Size = (int)file.Length;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path">The path to the video</param>
    /// <param name="size">The size in bytes of the video</param>
    public SourceVideo(string path, int size)
    {
        Path = path;
        Size = size;
    }

    public SourceVideo()
    {
        Path = string.Empty;
        Size = 0;
    }

    /// <summary>
    /// The name of the video file together with its extension.
    /// </summary>
    public string FullName => System.IO.Path.GetFileName(Path);

    /// <summary>
    /// The name of the video file without its extension.
    /// </summary>
    [JsonIgnore]
    public string Name => FullName.Split('.').First();

    /// <summary>
    /// The path to the video file.
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    /// The extension of the video file.
    /// </summary>
    [JsonIgnore]
    public string Extension => Path.Split('.').Last();

    /// <summary>
    /// The size in bytes of the video file.
    /// </summary>
    public long Size { get; init; }

    public bool Checked { get; set; }

    public override string ToString() => $"{FullName} - {Size} bytes";

    public override bool Equals(object? obj) => obj is SourceVideo video && video.Path == Path && video.Size == Size;

    public override int GetHashCode() => Path.GetHashCode() + Size.GetHashCode();
}