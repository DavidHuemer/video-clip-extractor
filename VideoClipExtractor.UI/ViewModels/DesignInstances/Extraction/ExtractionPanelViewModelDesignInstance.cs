using System.Collections.ObjectModel;
using BaseUI.Services.Provider.Attributes;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.DesignData;
using VideoClipExtractor.UI.ViewModels.Extraction;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.DesignInstances.Extraction
{
    [DesignData]
    public class ExtractionPanelViewModelDesignInstance : BaseViewModel, IExtractionPanelViewModel
    {
        public ExtractionPanelViewModelDesignInstance()
        {
            Videos = new ObservableCollection<VideoViewModel>(GetVideos());
            ActiveViewModel = this;
        }

        public ObservableCollection<VideoViewModel> Videos { get; }

        public void SetupExtraction(IEnumerable<VideoViewModel> videos)
        {
            throw new NotImplementedException();
        }

        public IBaseViewModel ActiveViewModel { get; set; }

        public IExtractionNavigationViewModel ExtractionNavigation =>
            ExtractionNavigationViewModelDesignInstance.Instance;

        private IEnumerable<VideoViewModel> GetVideos() =>
            VideoViewModelDesignDataExamples.GetExampleVideoViewModelsForExtractionPanel();
    }
}