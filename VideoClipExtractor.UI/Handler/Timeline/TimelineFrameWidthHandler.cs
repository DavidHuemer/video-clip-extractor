using BaseUI.Services.Provider.Attributes;

namespace VideoClipExtractor.UI.Handler.Timeline;

[Transient]
public class TimelineFrameWidthHandler : ITimelineFrameWidthHandler
{
    /// <summary>
    ///     Returns the width of a single frame at a given zoom level
    /// </summary>
    /// <param name="zoomLevel"></param>
    /// <returns>Width of a frame at a given zoom level</returns>
    public double GetFrameWidth(int zoomLevel) => 40.545 * Math.Exp(-0.064 * zoomLevel);
}