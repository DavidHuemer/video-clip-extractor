using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels.ExtractionWindow;

[UsedImplicitly]
[Transient]
public class ExtractionWindowViewModel : WindowViewModel, IExtractionWindowViewModel
{
    public ExtractionWindowViewModel(IDependencyProvider provider) : base(provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExtractionPanelViewModel = viewModelProvider.Get<IExtractionPanelViewModel>();
    }

    public IExtractionPanelViewModel ExtractionPanelViewModel { get; }

    public void SetupExtraction(IEnumerable<VideoViewModel> videos)
    {
        ExtractionPanelViewModel.SetupExtraction(videos);
    }
}