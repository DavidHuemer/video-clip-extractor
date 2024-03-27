using BaseUI.Basics.CurrentApplicationWrapper;
using BaseUI.Data;
using BaseUI.Exceptions.Basics;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Data.Exceptions.VideoRepositoryExceptions;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;

[Transient]
public class VideoCrawler(IDependencyProvider provider) : IVideoCrawler
{
    public const int BufferSize = 25;

    #region Private Fields

    private readonly ICurrentApplicationWrapper _currentApplicationWrapper =
        provider.GetDependency<ICurrentApplicationWrapper>();

    #endregion

    public async Task CrawlVideos() =>
        await Task.Run(RunCrawler);

    #region Events

    public event Action<List<SourceVideo>>? VideosAdded;

    #endregion

    private void RunCrawler()
    {
        var project = provider.GetDependency<IProjectManager>().Project;
        if (project == null)
            throw new ProjectNotSetException();

        var repo = provider.GetDependency<IVideoRepositoryManager>().VideoRepository;
        if (repo == null)
            throw new VideoRepositoryNotSetException();

        RunCrawlerWithProjectAndRepo(project, repo);
    }

    private void RunCrawlerWithProjectAndRepo(Project project, IVideoRepository repo)
    {
        var buffer = new ElementBuffer<SourceVideo>(BufferSize, Report);
        var sourceVideos = project.Videos.ToList();

        var files = repo
            .GetFiles()
            .Where(sourceVideo => SourceVideoCrawlingHandler.ShouldCrawl(sourceVideo, sourceVideos));

        foreach (var file in files)
        {
            buffer.Add(file);
        }

        buffer.Flush();
    }

    private void Report(List<SourceVideo> videos)
    {
        _currentApplicationWrapper.Run(() => { VideosAdded?.Invoke(videos); });
    }
}