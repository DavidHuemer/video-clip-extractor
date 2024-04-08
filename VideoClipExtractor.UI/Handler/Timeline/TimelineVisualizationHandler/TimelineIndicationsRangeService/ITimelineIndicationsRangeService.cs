namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicationsRangeService;

/// <summary>
/// Responsible for calculating the range of the timeline indications. (The first and last indication)
/// </summary>
public interface ITimelineIndicationsRangeService
{
    int GetFirstFrame(double movement, double frameWidth, int stepSize);

    int GetLastFrame(int indicationStepSize, double frameWidth, double width);
}