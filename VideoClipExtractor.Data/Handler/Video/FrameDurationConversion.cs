namespace VideoClipExtractor.Data.Handler.Video;

/// <summary>
/// Converts frame to duration and vice versa
/// </summary>
public static class FrameDurationConversion
{
    /// <summary>
    /// Converts frame to duration
    /// </summary>
    /// <param name="frame">The frame that should be converted to the duration</param>
    /// <param name="frameRate">The framerate that should be taken into account when converting</param>
    /// <returns>The duration converted from a frame</returns>
    public static TimeSpan GetDurationByFrame(int frame, int frameRate)
    {
        var seconds = (double)frame / frameRate;
        return TimeSpan.FromSeconds(seconds);
    }

    /// <summary>
    /// Converts duration to frame
    /// </summary>
    /// <param name="time">The duration that should be converted to the frame</param>
    /// <param name="frameRate">The framerate that should be taken into account when converting</param>
    /// <returns>The frame converted from a duration</returns>
    public static int GetFrameByTimespan(TimeSpan time, int frameRate)
    {
        var seconds = time.TotalSeconds;
        return (int)Math.Round(seconds * frameRate);
    }
}