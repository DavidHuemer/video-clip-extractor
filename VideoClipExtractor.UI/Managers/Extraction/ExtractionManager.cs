using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.WindowService;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.WindowViewModels;

namespace VideoClipExtractor.UI.Managers.Extraction;

public class ExtractionManager(IDependencyProvider provider) : IExtractionManager
{
    public void ExtractVideos(IEnumerable<VideoViewModel> videos)
    {
        
        
        var windowService = provider.GetDependency<IWindowService>();
        var videoRepositoryExplorerVm = new ExtractionWindowViewModel(provider);
        videoRepositoryExplorerVm.ShowDialog(windowService);
    }
}