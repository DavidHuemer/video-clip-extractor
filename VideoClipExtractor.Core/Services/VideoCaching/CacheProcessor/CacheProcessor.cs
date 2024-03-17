using BaseUI.Exceptions.Basics;
using BaseUI.Handler;
using VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;

public class CacheProcessor(ICacheRunner cacheRunner) : AsyncQueueProcessor<SourceVideo, CachedVideo>, ICacheProcessor
{
    private bool IsSetup => cacheRunner.IsSetup;

    public void AddVideo(SourceVideo video)
    {
        if (!IsSetup)
            throw new NotSetupException(nameof(CacheProcessor), nameof(Setup));

        Enqueue(video);
    }

    public void Setup(Project project, IVideoRepository repository) =>
        cacheRunner.Setup(project, repository);

    protected override CachedVideo Process(SourceVideo sourceVideo)
    {
        return cacheRunner.StoreVideo(sourceVideo);
    }
}