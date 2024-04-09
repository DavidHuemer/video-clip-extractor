using System.Text.Json.Serialization;
using System.Windows;
using VideoClipExtractor.Data.Handler.Video;

namespace VideoClipExtractor.Data.UI.Video;

/// <summary>
/// Represents the position of a video
/// </summary>
[method: JsonConstructor]
public class VideoPosition1(int frame)
{
    public VideoPosition1(Duration duration) : this(FrameDurationConversion.GetFrameByTimespan(duration.TimeSpan, 30))
    {
    }

    public override bool Equals(object? obj)
    {
        return obj is VideoPosition1 position &&
               Frame == position.Frame;
    }

    public override int GetHashCode() => HashCode.Combine(Frame, Duration);

    #region Properties

    public int Frame { get; set; } = frame;

    public Duration Duration => FrameDurationConversion.GetDurationByFrame(Frame, 30);

    #endregion
}