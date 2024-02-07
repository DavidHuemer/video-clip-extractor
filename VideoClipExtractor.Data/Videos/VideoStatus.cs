namespace VideoClipExtractor.Data.Videos;

/// <summary>
/// Represents the status of a video
/// </summary>
public enum VideoStatus
{
    Unset,
    Skipped,
    ReadyForExport,
    Exported,
}