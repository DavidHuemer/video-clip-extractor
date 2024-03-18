using BaseUI.Exceptions.Basics;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoCaching;

[Transient]
public class VideoCacheService(IDependencyProvider provider) : IVideoCacheService
{
    private readonly ICacheProcessor _cacheProcessor = provider.GetDependency<ICacheProcessor>();
    public event EventHandler<VideoCachedEventArgs>? VideoCached;

    public void Setup(Project project, IVideoRepository repository)
    {
        _cacheProcessor.Setup(project, repository);
    }

    public void CacheVideo(SourceVideo video)
    {
        if (!_cacheProcessor.IsSetup)
            throw new NotSetupException(nameof(VideoCacheService), nameof(Setup));

        _cacheProcessor.AddVideo(video);
    }
}