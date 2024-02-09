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
    public TimelineControlViewModel()
    {
        TimelineNavigationViewModel = new TimelineNavigationViewModel();

        ViewModelPropertyListener.AddPropertyListener(TimelineNavigationViewModel,
            new[]
            {
                nameof(TimelineNavigationViewModel.ZoomLevel), nameof(TimelineNavigationViewModel.MovementPosition)
            }, UpdateVerticalLines);

        UpdateVerticalLines();
    }

    public TimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    public ObservableCollection<int> VerticalLines { get; } = [];

    private void UpdateVerticalLines()
    {
        VerticalLines.Clear();
        var primitiveScalar = TimelineScaleHandler.GetPrimitiveScale(TimelineNavigationViewModel.ZoomLevel);
        var frameWidth = TimelineFrameWidthHandler.GetFrameWidth(TimelineNavigationViewModel.ZoomLevel);

        var firstVisibleFrame =
            TimelineFrameVisibilityHandler.GetFirstVisibleFrame(TimelineNavigationViewModel.MovementPosition,
                frameWidth);
        var firstVisibleFrameScalar = (int)Math.Ceiling((double)firstVisibleFrame / primitiveScalar) * primitiveScalar;

        for (var i = 0; i < 100; i++)
        {
            if (firstVisibleFrameScalar >= 0) VerticalLines.Add(firstVisibleFrameScalar);
            firstVisibleFrameScalar += primitiveScalar;
        }
    }
}