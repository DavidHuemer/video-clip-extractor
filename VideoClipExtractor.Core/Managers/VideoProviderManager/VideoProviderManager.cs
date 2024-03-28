using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.VideoProviderManager;

[Singleton]
public class VideoProviderManager(IDependencyProvider provider) : IVideoProviderManager
{
    public IVideoProvider? VideoProvider { get; private set; }
    public event Action<VideoViewModel>? VideoAdded;

    public void Setup(Project? project, IVideoRepository? repository)
    {
        ClearProvider();
        if (project == null || repository == null) return;

        VideoProvider = provider.GetDependency<IVideoProvider>();
        VideoProvider.Setup(project, repository);
        VideoProvider.VideoAdded += OnVideoAdded;
    }

    public void Next() => VideoProvider?.Next();

    private void ClearProvider()
    {
        if (VideoProvider == null) return;
        VideoProvider.VideoAdded -= OnVideoAdded;
        VideoProvider = null;
    }

    private void OnVideoAdded(VideoViewModel video) => VideoAdded?.Invoke(video);
}