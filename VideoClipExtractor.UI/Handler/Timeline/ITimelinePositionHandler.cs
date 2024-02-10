namespace VideoClipExtractor.UI.Handler.Timeline;

/// <summary>
/// Provides methods for handling timeline positions.
/// </summary>
public interface ITimelinePositionHandler
{
    /// <summary>
    /// Returns the frame at the given position.
    /// </summary>
    /// <param name="position">The given position in the timeline</param>
    /// <param name="zoomLevel">The current zoomLevel</param>
    /// <returns></returns>
    double GetFrameAtPosition(double position, int zoomLevel);

    /// <summary>
    /// Returns the position at the given frame.
    /// </summary>
    /// <param name="frame">The given frame</param>
    /// <param name="zoomLevel">The current zoomLevel</param>
    /// <returns></returns>
    double GetPositionAtFrame(double frame, int zoomLevel);

    /// <summary>
    /// Returns the center position of the given timeline position and width.
    /// </summary>
    /// <param name="timelinePosition">The movement of the timeline</param>
    /// <param name="timelineWidth">The actual width of the timeline</param>
    /// <returns></returns>
    double GetCenterPosition(double timelinePosition, double timelineWidth);
}