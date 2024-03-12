using BaseUI.ViewModels;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;

public interface IVideoRepositoryExplorerWindowViewModel : IWindowViewModel
{
    event EventHandler<VideoRepositoryBlueprintEventArgs>? VideoRepositoryBlueprintSelected;
}