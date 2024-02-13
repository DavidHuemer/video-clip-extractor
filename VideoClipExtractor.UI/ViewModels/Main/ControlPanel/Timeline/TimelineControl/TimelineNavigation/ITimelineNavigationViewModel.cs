using System.ComponentModel;
using VideoClipExtractor.Data.UI.Timeline;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

public interface ITimelineNavigationViewModel : INotifyPropertyChanged
{
    MovementState MovementState { get; set; }

    double MovementPosition { get; set; }

    int ZoomLevel { get; set; }

    double TimelineControlWidth { get; set; }
}