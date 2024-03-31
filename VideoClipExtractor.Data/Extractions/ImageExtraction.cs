using System.Text.Json.Serialization;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

[method: JsonConstructor]
public sealed class ImageExtraction(VideoPosition position) : BaseExtractionViewModel, IImageExtraction
{
    public override VideoPosition Position { get; set; } = position;
    public string Name { get; set; } = "";

    [JsonIgnore] public ExtractionResult? Result { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is ImageExtraction extraction &&
               Name == extraction.Name &&
               Position.Equals(extraction.Position);
    }

    public override int GetHashCode() => 0;
}