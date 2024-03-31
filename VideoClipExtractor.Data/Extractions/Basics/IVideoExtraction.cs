using System.Text.Json.Serialization;

namespace VideoClipExtractor.Data.Extractions.Basics;

[JsonDerivedType(typeof(VideoExtraction), "videoExtraction")]
public interface IVideoExtraction : IExtraction
{
    VideoExtractionPart Begin { get; }

    VideoExtractionPart End { get; }
}