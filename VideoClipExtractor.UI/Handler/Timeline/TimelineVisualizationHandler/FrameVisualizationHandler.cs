using BaseUI.Handler.ViewModelHandler;
using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;

[UsedImplicitly]
public class FrameVisualizationHandler(IDependencyProvider provider)
    : IFramesVisualizationHandler
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler =
        provider.GetDependency<ITimelineFrameWidthHandler>();

    private ITimelineControlViewModel? _timelineControlViewModel;


    public void Setup(ITimelineControlViewModel timelineControlViewModel)
    {
        _timelineControlViewModel = timelineControlViewModel;

        ViewModelPropertyListener.AddPropertyListener(timelineControlViewModel.TimelineNavigationViewModel,
            new[]
            {
                nameof(TimelineNavigationViewModel.ZoomLevel),
                nameof(TimelineNavigationViewModel.MovementPosition),
                nameof(TimelineNavigationViewModel.TimelineControlWidth),
            }, UpdateVerticalLines);

        UpdateVerticalLines();
    }

    private void UpdateVerticalLines()
    {
        if (_timelineControlViewModel == null) return;

        var timelineNavigationVm = _timelineControlViewModel.TimelineNavigationViewModel;

        _timelineControlViewModel.VerticalLines.Clear();
        var primitiveScalar = TimelineScaleHandler.GetPrimitiveScale(timelineNavigationVm.ZoomLevel);
        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(timelineNavigationVm.ZoomLevel);

        var firstVisibleFrame =
            TimelineFrameVisibilityHandler.GetFirstVisibleFrame(timelineNavigationVm.MovementPosition,
                frameWidth);
        var visibleFrameScalar = (int)Math.Ceiling((double)firstVisibleFrame / primitiveScalar) * primitiveScalar;

        for (var i = 0; i < 100; i++)
        {
            if (visibleFrameScalar >= 0)
            {
                var isVisible = TimelineFrameVisibilityHandler.IsBeforeEnd(visibleFrameScalar,
                    timelineNavigationVm.MovementPosition, frameWidth,
                    timelineNavigationVm.TimelineControlWidth);

                if (isVisible)
                {
                    _timelineControlViewModel.VerticalLines.Add(visibleFrameScalar);
                }
                else
                {
                    break;
                }
            }

            visibleFrameScalar += primitiveScalar;
        }
    }
}