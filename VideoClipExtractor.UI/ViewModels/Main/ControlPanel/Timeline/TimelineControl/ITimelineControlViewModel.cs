using System.Collections.ObjectModel;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

/// <summary>
/// View model for the actual timeline control
/// </summary>
public interface ITimelineControlViewModel : IBaseViewModel
{
    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    ITimelineExtractionsViewModel TimelineExtractionsViewModel { get; set; }

    ObservableCollection<int> VerticalLines { get; }
    IDependencyProvider Provider { get; }

    VideoViewModel? VideoViewModel { set; }
    ObservableCollection<VideoPosition> TimelineIndicators { get; set; }
    ObservableCollection<VideoPosition> TimelineSupporters { get; set; }
}