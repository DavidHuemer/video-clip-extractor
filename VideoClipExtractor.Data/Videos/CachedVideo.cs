namespace VideoClipExtractor.Data.Videos;

/// <summary>
///     Represents a video that has been cached locally. v
/// </summary>
/// <param name="SourceVideo">The source video (from which the video should be cached)</param>
/// <param name="LocalPath">The path where the video is cached/stored locally</param>
public record CachedVideo(SourceVideo SourceVideo, string LocalPath);