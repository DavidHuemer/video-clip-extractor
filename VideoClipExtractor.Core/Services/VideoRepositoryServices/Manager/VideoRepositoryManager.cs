using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;

[UsedImplicitly]
public class VideoRepositoryManager(IDependencyProvider provider) : IVideoRepositoryManager
{
    #region Properties

    public IVideoRepository? VideoRepository { get; set; }

    #endregion

    public void SetupRepository(VideoRepositoryBlueprint blueprint)
    {
        var videoRepoBuilder = provider.GetDependency<IVideoRepositoryBuilder>();
        VideoRepository = videoRepoBuilder.Build(blueprint);
        VideoRepository.Connect();
    }
}