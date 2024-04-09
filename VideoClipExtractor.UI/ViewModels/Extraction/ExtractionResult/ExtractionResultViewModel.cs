using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionResult;

[UsedImplicitly]
[Transient]
public class ExtractionResultViewModel : BaseViewModel, IExtractionResultViewModel
{
    public ExtractionResultViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        NavigationViewModel = viewModelProvider.Get<IExtractionNavigationViewModel>();
    }

    public ExtractionProcessResult Result
    {
        set
        {
            Success = value.Success;
            Message = value.Message;

            var successfulExtractions = value.SuccessfulVideoExtractions.Count();
            var totalExtractions = value.VideoExtractionResults.Count();
            SuccessfulExtractions = $"{successfulExtractions}/{totalExtractions}";

            StoredBytes = value.StoredBytes;
            SavedBytes = value.SavedBytes;
            ByteDifference = value.StoredBytes - value.SavedBytes;
        }
    }

    public IExtractionNavigationViewModel NavigationViewModel { get; }

    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public bool ShowMessage => !string.IsNullOrEmpty(Message);
    public string SuccessfulExtractions { get; set; } = "0/0";
    public long StoredBytes { get; set; }
    public long SavedBytes { get; set; }

    public long ByteDifference { get; set; }
    public ICommand GoBack => new RelayCommand<string>(DoGoBack, _ => true);

    private void DoGoBack(string? obj) => NavigationViewModel.CurrentVideo = null;
}