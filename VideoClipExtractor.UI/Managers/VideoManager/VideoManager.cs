using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.Managers.VideoManager;

[Singleton]
public class VideoManager : IVideoManager
{
    private readonly IVideosExplorerViewModel _videosExplorer;

    public VideoManager(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _videosExplorer = viewModelProvider.Get<IVideosExplorerViewModel>();

        _videosExplorer.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(IVideosExplorerViewModel.SelectedVideo))
                VideoChanged?.Invoke(_videosExplorer.SelectedVideo);
        };
    }

    public event Action<VideoViewModel?>? VideoChanged;

    #region Properties

    public VideoViewModel? Video => _videosExplorer.SelectedVideo;

    #endregion
}