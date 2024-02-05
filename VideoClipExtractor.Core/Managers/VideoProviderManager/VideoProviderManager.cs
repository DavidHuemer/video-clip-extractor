using BaseUI.Services.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Managers.VideoProviderManager;

[UsedImplicitly]
public class VideoProviderManager(IDependencyProvider provider) : IVideoProviderManager
{
    #region Private Fields

    private IVideoProvider? _videoProvider;

    #endregion

    #region Events

    public event EventHandler<VideoEventArgs>? VideoAdded;

    #endregion

    public void Setup(Project project, IVideoRepository repository)
    {
        _videoProvider = provider.GetDependency<IVideoProvider>();
        _videoProvider.Setup(project, repository);
        _videoProvider.VideoAdded += OnVideoAdded;
    }

    private void OnVideoAdded(object? sender, VideoEventArgs e)
    {
        VideoAdded?.Invoke(sender, e);
    }
}