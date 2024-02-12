using System.Windows;
using VideoClipExtractor.Data.Handler.Video;

namespace VideoClipExtractor.Data.UI.Video;

/// <summary>
/// Represents the position of a video
/// </summary>
public class VideoPosition
{
    public VideoPosition(int frame)
    {
        Frame = frame;
        Duration = FrameDurationConversion.GetDurationByFrame(frame, 30);
    }

    public VideoPosition(Duration duration)
    {
        Duration = duration;
        Frame = FrameDurationConversion.GetFrameByTimespan(duration.TimeSpan, 30);
    }

    public override bool Equals(object? obj)
    {
        return obj is VideoPosition position &&
               Frame == position.Frame &&
               Duration.Equals(position.Duration);
    }

    public override int GetHashCode() => HashCode.Combine(Frame, Duration);

    #region Properties

    public int Frame { get; init; }

    public Duration Duration { get; init; }

    #endregion
}