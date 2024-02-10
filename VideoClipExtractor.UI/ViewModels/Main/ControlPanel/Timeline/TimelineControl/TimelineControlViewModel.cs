using System.Collections.ObjectModel;
using BaseUI.Handler.ViewModelHandler;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.UI.Handler.Timeline;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

[UsedImplicitly]
public class TimelineControlViewModel : BaseViewModel, ITimelineControlViewModel
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler;

    public TimelineControlViewModel(ITimelineFrameWidthHandler? timelineFrameWidthHandler = null)
    {
        _timelineFrameWidthHandler = timelineFrameWidthHandler ?? new TimelineFrameWidthHandler();

        TimelineNavigationViewModel = new TimelineNavigationViewModel();

        ViewModelPropertyListener.AddPropertyListener(TimelineNavigationViewModel,
            new[]
            {
                nameof(TimelineNavigationViewModel.ZoomLevel),
                nameof(TimelineNavigationViewModel.MovementPosition),
                nameof(TimelineNavigationViewModel.TimelineControlWidth),
            }, UpdateVerticalLines);

        UpdateVerticalLines();
    }

    public TimelineControlViewModel() : this(null)
    {
    }

    public TimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    public ObservableCollection<int> VerticalLines { get; } = [];

    private void UpdateVerticalLines()
    {
        VerticalLines.Clear();
        var primitiveScalar = TimelineScaleHandler.GetPrimitiveScale(TimelineNavigationViewModel.ZoomLevel);
        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(TimelineNavigationViewModel.ZoomLevel);

        var firstVisibleFrame =
            TimelineFrameVisibilityHandler.GetFirstVisibleFrame(TimelineNavigationViewModel.MovementPosition,
                frameWidth);
        var visibleFrameScalar = (int)Math.Ceiling((double)firstVisibleFrame / primitiveScalar) * primitiveScalar;

        for (var i = 0; i < 100; i++)
        {
            if (visibleFrameScalar >= 0)
            {
                var isVisible = TimelineFrameVisibilityHandler.IsBeforeEnd(visibleFrameScalar,
                    TimelineNavigationViewModel.MovementPosition, frameWidth,
                    TimelineNavigationViewModel.TimelineControlWidth);

                if (isVisible)
                {
                    VerticalLines.Add(visibleFrameScalar);
                }
                else
                {
                    break;
                }
            }


            visibleFrameScalar += primitiveScalar;
        }

        Console.WriteLine("Hello");
    }
}