using System.Collections.ObjectModel;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

public interface IExtractionNavigationViewModel
{
    public VideoViewModel? CurrentVideo { get; set; }

    public ObservableCollection<IExtraction> Extractions { get; }

    public bool ShowDetails { get; set; }
}