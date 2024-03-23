using BaseUI.Exceptions.Basics;
using BaseUI.Handler;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;

[Transient]
public class CacheProcessor(IDependencyProvider provider)
    : AsyncQueueProcessor<SourceVideo, CachedVideo>, ICacheProcessor
{
    private readonly ICacheRunner _cacheRunner = provider.GetDependency<ICacheRunner>();

    public bool IsSetup => _cacheRunner.IsSetup;

    public void AddVideo(SourceVideo video)
    {
        if (!IsSetup)
            throw new NotSetupException(nameof(CacheProcessor), nameof(Setup));

        Enqueue(video);
    }

    public void Setup(Project project, IVideoRepository repository) =>
        _cacheRunner.Setup(project, repository);

    protected override CachedVideo Process(SourceVideo sourceVideo) =>
        _cacheRunner.StoreVideo(sourceVideo);
}