using System.Text.Json.Serialization;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class VideoExtraction : BaseExtractionViewModel, IVideoExtraction
{
    public VideoExtraction(VideoPosition begin, VideoPosition end)
    {
        Begin = new VideoExtractionPart(begin);
        End = new VideoExtractionPart(end);
    }

    [JsonConstructor]
    public VideoExtraction(VideoExtractionPart begin, VideoExtractionPart end)
    {
        Begin = begin;
        End = end;
    }

    public override VideoPosition Position
    {
        get => Begin.Position;
        set
        {
            var length = End.Position.Frame - Begin.Position.Frame;
            Begin.Position = value;
            End.Position = new VideoPosition(value.Frame + length, Begin.Position.FrameRate);
        }
    }

    public override void SetupSelection(Action<IExtractionViewModel> selectionCallback)
    {
        base.SetupSelection(selectionCallback);

        Begin.SetupSelection(selectionCallback);
        End.SetupSelection(selectionCallback);
    }

    public override bool Equals(object? obj)
    {
        return obj is VideoExtraction extraction &&
               Name == extraction.Name &&
               Begin.Equals(extraction.Begin) &&
               End.Equals(extraction.End);
    }

    public override int GetHashCode() => HashCode.Combine(Begin, End);

    #region Properties

    public string Name { get; set; } = "";

    public ExtractionResult? Result { get; set; }

    public VideoExtractionPart Begin { get; }

    public VideoExtractionPart End { get; }

    [JsonIgnore] public int FrameCount => End.Position.Frame - Begin.Position.Frame;

    #endregion
}