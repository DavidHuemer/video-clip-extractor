using System.Windows.Input;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionResult;

public interface IExtractionResultViewModel : IBaseViewModel
{
    public ExtractionProcessResult Result { set; }

    public IExtractionNavigationViewModel NavigationViewModel { get; }

    public bool Success { get; set; }

    string Message { get; set; }

    bool ShowMessage { get; }

    string SuccessfulExtractions { get; set; }

    long StoredBytes { get; set; }

    long ByteDifference { get; }

    long SavedBytes { get; set; }

    ICommand GoBack { get; }
}