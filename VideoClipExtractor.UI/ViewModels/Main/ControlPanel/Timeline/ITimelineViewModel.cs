using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

/// <summary>
/// The ViewModel for the entire timeline.
/// </summary>
public interface ITimelineViewModel
{
    public VideoViewModel? Video { set; }
}