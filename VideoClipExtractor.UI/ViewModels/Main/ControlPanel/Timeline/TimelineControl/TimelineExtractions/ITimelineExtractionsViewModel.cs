using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;

public interface ITimelineExtractionsViewModel
{
    VideoViewModel? Video { get; set; }
}