using BaseUI.Services.Provider.Attributes;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;

[UsedImplicitly]
[Singleton]
public class TimelineExtractionsViewModel : BaseViewModel, ITimelineExtractionsViewModel
{
    public VideoViewModel? Video { get; set; }
}