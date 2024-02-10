namespace VideoClipExtractor.UI.Handler.Timeline;

public class TimelinePositionHandler(ITimelineFrameWidthHandler? widthHandler = null) : ITimelinePositionHandler
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler =
        widthHandler ?? new TimelineFrameWidthHandler();

    public double GetFrameAtPosition(double position, int zoomLevel)
    {
        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);
        var frame = (position - 200) / frameWidth;

        return frame < 0 ? 0 : frame;
    }

    public double GetPositionAtFrame(double frame, int zoomLevel)
    {
        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);
        return frame * frameWidth + 200;
    }

    public double GetCenterPosition(double timelinePosition, double timelineWidth) =>
        timelineWidth / 2 + timelinePosition;
}