using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicatorsFactory;

public interface ITimelineIndicatorsFactory
{
    List<VideoPosition> GetTimelineIndicators(double movement, int zoomLevel, double width);
    List<VideoPosition> GetTimelineSupporters(double movementPosition, int zoomLevel, double timelineControlWidth);
}