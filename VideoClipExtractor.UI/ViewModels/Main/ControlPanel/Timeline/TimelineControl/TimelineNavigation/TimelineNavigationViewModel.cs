using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

public class TimelineNavigationViewModel : BaseViewModel
{
    public double MovementPosition { get; set; }
    public int ZoomLevel { get; set; } = 27;
    public double TimelineControlWidth { get; set; } = 1000;

    public MovementState MovementState { get; set; }

    public VideoPosition VideoPosition { get; set; } = new(0);
}