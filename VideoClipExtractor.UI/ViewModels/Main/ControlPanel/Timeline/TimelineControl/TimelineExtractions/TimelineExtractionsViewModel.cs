using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;

[UsedImplicitly]
public class TimelineExtractionsViewModel : BaseViewModel, ITimelineExtractionsViewModel
{
    public VideoViewModel? Video { get; set; }
}