using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Core.Managers.VideoRepositoryManager;

[Singleton]
public class VideoRepositoryManager(IDependencyProvider provider) : IVideoRepositoryManager
{
    public event Action<IVideoRepository?>? VideoRepositoryChanged;
    public IVideoRepository? VideoRepository { get; set; }

    public void SetupRepositoryByBlueprint(VideoRepositoryBlueprint blueprint)
    {
        var videoRepoBuilder = provider.GetDependency<IVideoRepositoryBuilder>();
        VideoRepository = videoRepoBuilder.Build(blueprint);
        VideoRepository.Connect();
        VideoRepositoryChanged?.Invoke(VideoRepository);
    }
}