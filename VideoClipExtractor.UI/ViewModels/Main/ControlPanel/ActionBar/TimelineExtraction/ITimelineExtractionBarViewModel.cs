using System.Windows.Input;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

public interface ITimelineExtractionBarViewModel
{
    VideoViewModel? Video { get; set; }

    ICommand AddImageExtraction { get; }
}