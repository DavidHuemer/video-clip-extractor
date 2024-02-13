using System.Collections.ObjectModel;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

/// <summary>
/// View model for the actual timeline control
/// </summary>
public interface ITimelineControlViewModel
{
    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    ObservableCollection<int> VerticalLines { get; }
    IDependencyProvider Provider { get; }
}