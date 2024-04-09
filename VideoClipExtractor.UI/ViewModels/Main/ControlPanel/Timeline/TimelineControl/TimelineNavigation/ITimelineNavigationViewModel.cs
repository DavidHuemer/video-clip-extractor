using System.Windows.Input;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Timeline;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

public interface ITimelineNavigationViewModel : IBaseViewModel
{
    MovementState MovementState { get; set; }

    double MovementPosition { get; set; }

    int ZoomLevel { get; set; }

    double TimelineControlWidth { get; set; }

    ICommand ZoomIn { get; }

    ICommand ZoomOut { get; }
}