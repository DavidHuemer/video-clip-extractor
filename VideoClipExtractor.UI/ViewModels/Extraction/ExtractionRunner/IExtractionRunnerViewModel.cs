using System.Collections.ObjectModel;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

public interface IExtractionRunnerViewModel : IBaseViewModel
{
    public IExtractionNavigationViewModel ExtractionNavigation { get; set; }

    public ObservableCollection<IExtraction> Extractions { get; set; }
    Task<ExtractionProcessResult> ExtractVideos(IEnumerable<VideoViewModel> videos);
}