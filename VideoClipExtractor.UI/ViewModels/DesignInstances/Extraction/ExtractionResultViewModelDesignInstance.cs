using System.Windows.Input;
using BaseUI.Commands;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionResult;

namespace VideoClipExtractor.UI.ViewModels.DesignInstances.Extraction
{
    public class ExtractionResultViewModelDesignInstance : IExtractionResultViewModel
    {
        public bool ShowDetails => true;

        public ExtractionProcessResult Result
        {
            set { throw new NotImplementedException(); }
        }

        public IExtractionNavigationViewModel NavigationViewModel =>
            ExtractionNavigationViewModelDesignInstance.Instance;

        public bool Success { get; set; } = false;
        public string Message { get; set; } = "Extraction failed";
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        public string SuccessfulExtractions { get; set; } = "0/0";
        public long StoredBytes { get; set; } = 75;
        public long SavedBytes { get; set; } = 750;

        public long ByteDifference => StoredBytes - SavedBytes;

        public ICommand GoBack => new RelayCommand<string>(DoGoBack, _ => true);

        private void DoGoBack(string? obj)
        {
            NavigationViewModel.CurrentVideo = null;
        }
    }
}