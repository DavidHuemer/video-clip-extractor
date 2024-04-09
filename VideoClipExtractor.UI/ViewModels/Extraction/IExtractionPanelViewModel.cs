using System.Collections.ObjectModel;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.Extraction;

/// <summary>
/// The view model for the extraction panel.
/// </summary>
public interface IExtractionPanelViewModel : IBaseViewModel
{
    ObservableCollection<VideoViewModel> Videos { get; }

    IBaseViewModel ActiveViewModel { get; set; }

    IExtractionNavigationViewModel ExtractionNavigation { get; }

    void SetupExtraction(IEnumerable<VideoViewModel> videos);
}