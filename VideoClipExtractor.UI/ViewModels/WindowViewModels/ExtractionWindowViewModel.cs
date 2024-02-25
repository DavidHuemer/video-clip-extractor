using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

[UsedImplicitly]
public class ExtractionWindowViewModel : WindowViewModel
{
    public ExtractionWindowViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExtractionViewModel = viewModelProvider.Get<IExtractionPanelViewModel>();
        
        var explorerViewModel = viewModelProvider.Get<IVideosExplorerViewModel>();
        SetupExtraction(explorerViewModel.Videos);
    }

    public IExtractionPanelViewModel ExtractionViewModel { get; set; }

    public void SetupExtraction(IEnumerable<VideoViewModel> videos)
    {
        ExtractionViewModel.SetupExtraction(videos);
    }
}