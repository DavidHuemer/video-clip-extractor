namespace VideoClipExtractor.UI.Handler.Timeline;

public class TimelineFrameWidthHandler : ITimelineFrameWidthHandler
{
    /// <summary>
    ///     Returns the width of a single frame at a given zoom level
    /// </summary>
    /// <param name="zoomLevel"></param>
    /// <returns>Width of a frame at a given zoom level</returns>
    public double GetFrameWidth(int zoomLevel) =>
        100 / Math.Pow(2, (double)zoomLevel / 9);
}