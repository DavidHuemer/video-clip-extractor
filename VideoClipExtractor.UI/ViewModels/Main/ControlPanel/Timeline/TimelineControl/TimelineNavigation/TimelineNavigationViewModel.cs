using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

public class TimelineNavigationViewModel : BaseViewModel
{
    public double MovementPosition { get; set; }
    public int ZoomLevel { get; set; } = 27;
    public double TimelineControlWidth { get; set; } = 1000;
}