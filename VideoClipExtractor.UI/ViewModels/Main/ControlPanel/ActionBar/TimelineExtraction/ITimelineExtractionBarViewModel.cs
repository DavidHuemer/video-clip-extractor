using System.Windows.Input;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

/// <summary>
/// The extraction bar view model that has the ability to add image and video extractions
/// </summary>
public interface ITimelineExtractionBarViewModel
{
    VideoViewModel? Video { get; set; }

    ICommand AddImageExtraction { get; }
}