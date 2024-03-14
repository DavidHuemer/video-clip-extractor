using System.Text.Json.Serialization;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

/// <summary>
/// The part of a <see cref="VideoExtraction"/>
/// </summary>
/// <param name="position">The position of the video extraction part</param>
[method: JsonConstructor]
public sealed class VideoExtractionPart(VideoPosition position) : BaseExtractionViewModel
{
    public override VideoPosition Position { get; set; } = position;

    public override bool Equals(object? obj) =>
        obj is VideoExtractionPart part &&
        Position.Equals(part.Position);

    public override int GetHashCode() => 0;
}