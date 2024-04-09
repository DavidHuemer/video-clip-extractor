using System.Collections.ObjectModel;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.DesignData;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.DesignInstances.Extraction
{
    public class ExtractionNavigationViewModelDesignInstance : BaseViewModel, IExtractionNavigationViewModel
    {
        public static ExtractionNavigationViewModelDesignInstance Instance =>
            new ExtractionNavigationViewModelDesignInstance();

        public VideoViewModel? CurrentVideo { get; set; } = VideoViewModelDesignDataExamples.GetExampleVideoViewModel(
            VideoStatus.ReadyForExport, VideoViewModelDesignDataExamples.GetVideoExtractionResultExample(true));

        public ObservableCollection<IExtraction> Extractions { get; } =
        [
            ExtractionDesignDataExamples.GetExampleImageExtraction(),
        ];

        public bool ShowDetails { get; set; }
    }
}