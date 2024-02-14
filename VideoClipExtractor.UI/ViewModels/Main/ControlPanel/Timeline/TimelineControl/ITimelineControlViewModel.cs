using System.Collections.ObjectModel;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

/// <summary>
/// View model for the actual timeline control
/// </summary>
public interface ITimelineControlViewModel
{
    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    ITimelineExtractionsViewModel TimelineExtractionsViewModel { get; set; }

    ObservableCollection<int> VerticalLines { get; }
    IDependencyProvider Provider { get; }

    VideoViewModel? VideoViewModel { set; }
}