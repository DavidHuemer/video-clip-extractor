using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction;

/// <summary>
/// The view model for the extraction panel.
/// </summary>
public interface IExtractionPanelViewModel
{
    void SetupExtraction(IEnumerable<VideoViewModel> videos);
}