using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.ExtractionWindow;

namespace VideoClipExtractor.UI.Managers.Extraction;

public class ExtractionManager(IDependencyProvider provider) : IExtractionManager
{
    public void ExtractVideos(IEnumerable<VideoViewModel> videos)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();

        var extractionWindowViewModel = viewModelProvider.Get<IExtractionWindowViewModel>();
        extractionWindowViewModel.SetupExtraction(videos);
        extractionWindowViewModel.ShowDialog();
    }
}