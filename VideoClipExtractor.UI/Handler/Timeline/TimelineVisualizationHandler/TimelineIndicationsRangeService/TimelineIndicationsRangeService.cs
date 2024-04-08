using BaseUI.Services.Provider.Attributes;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicationsRangeService;

[Transient]
public class TimelineIndicationsRangeService : ITimelineIndicationsRangeService
{
    public int GetFirstFrame(double movement, double frameWidth, int stepSize)
    {
        var actualMovement = movement - 200;
        var startFrame = actualMovement / frameWidth;

        var timesStep = (Math.Floor(startFrame / stepSize) * stepSize) - stepSize;

        return timesStep < 0 ? 0 : (int)timesStep;
    }

    public int GetLastFrame(int indicationStepSize, double frameWidth, double width) =>
        (int)Math.Ceiling(width / (indicationStepSize * frameWidth)) + 3;
}