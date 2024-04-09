using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Timeline;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

[Singleton]
public class TimelineNavigationViewModel : BaseViewModel, ITimelineNavigationViewModel
{
    public double MovementPosition { get; set; }
    public int ZoomLevel { get; set; } = 27;
    public double TimelineControlWidth { get; set; } = 1000;
    public MovementState MovementState { get; set; }

    public ICommand ZoomIn => new RelayCommand<string>(DoZoomIn, _ => true);

    public ICommand ZoomOut => new RelayCommand<string>(DoZoomOut, _ => true);

    private void DoZoomIn(string? obj)
    {
        ZoomLevel--;
    }

    private void DoZoomOut(string? obj)
    {
        ZoomLevel++;
    }
}