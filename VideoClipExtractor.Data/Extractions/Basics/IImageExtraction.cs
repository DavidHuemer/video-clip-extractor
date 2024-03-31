using System.Text.Json.Serialization;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions.Basics;

[JsonDerivedType(typeof(ImageExtraction), "imageExtraction")]
public interface IImageExtraction : IExtraction
{
    VideoPosition Position { get; set; }
}