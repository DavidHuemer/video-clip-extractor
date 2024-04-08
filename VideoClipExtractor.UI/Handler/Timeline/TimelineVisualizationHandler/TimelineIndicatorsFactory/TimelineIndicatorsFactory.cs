using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicationsVisibility;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicationsRangeService;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicatorsFactory;

[Transient]
public class TimelineIndicatorsFactory(IDependencyProvider provider) : ITimelineIndicatorsFactory
{
    private readonly ITimeIndicationsVisibility _timeIndicationsVisibility =
        provider.GetDependency<ITimeIndicationsVisibility>();

    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler =
        provider.GetDependency<ITimelineFrameWidthHandler>();

    private readonly ITimelineIndicationsRangeService _timelineIndicationsRangeService =
        provider.GetDependency<ITimelineIndicationsRangeService>();

    private readonly IVideoManager _videoManager = provider.GetDependency<IVideoManager>();

    public List<VideoPosition> GetTimelineIndicators(double movement, int zoomLevel, double width)
    {
        var indicationStep = _timeIndicationsVisibility.GetIndicationStep(zoomLevel);

        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);

        var frame = _timelineIndicationsRangeService.GetFirstFrame(movement, frameWidth, indicationStep);
        var max = _timelineIndicationsRangeService.GetLastFrame(indicationStep, frameWidth, width);

        var frameRate = _videoManager.Video?.VideoInfo.FrameRate ?? 50;

        return Enumerable.Range(0, max)
            .Select(i => new VideoPosition((i * indicationStep) + frame, frameRate))
            .ToList();
    }

    public List<VideoPosition> GetTimelineSupporters(double movementPosition, int zoomLevel,
        double timelineControlWidth)
    {
        var indicationStep = _timeIndicationsVisibility.GetIndicationStep(zoomLevel);
        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);
        var max = _timelineIndicationsRangeService.GetLastFrame(indicationStep, frameWidth, timelineControlWidth);

        var supporterFrameWidth = indicationStep / 5;

        var frame = _timelineIndicationsRangeService.GetFirstFrame(movementPosition, frameWidth, indicationStep);

        var supporters = new List<VideoPosition>();

        var frameRate = _videoManager.Video?.VideoInfo.FrameRate ?? 50;

        for (var i = 0; i < max; i++)
        {
            var position = (i * indicationStep) + frame;
            supporters.Add(new VideoPosition(position + supporterFrameWidth, frameRate));
            supporters.Add(new VideoPosition(position + (supporterFrameWidth * 2), frameRate));
            supporters.Add(new VideoPosition(position + (supporterFrameWidth * 3), frameRate));
            supporters.Add(new VideoPosition(position + (supporterFrameWidth * 4), frameRate));
        }

        return supporters;
    }
}