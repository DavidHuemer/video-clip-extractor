using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.WorkspaceManager;

[Singleton]
public class WorkspaceManager : IWorkspaceManager
{
    public WorkspaceManager(IDependencyProvider provider)
    {
        _projectManager = provider.GetDependency<IProjectManager>();
        _videoRepositoryManager = provider.GetDependency<IVideoRepositoryManager>();
        _videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        _videoRepositoryManager.VideoRepositoryChanged += (_) => UpdateProvider();

        _videoProviderManager.VideoAdded += (video) => VideoAdded?.Invoke(video);
    }

    public event EventHandler? Clear;
    public event Action<VideoViewModel>? VideoAdded;

    public void SourceVideosChanged() => UpdateProvider();

    private void UpdateProvider()
    {
        Clear?.Invoke(this, EventArgs.Empty);
        _videoProviderManager.Setup(_projectManager.Project, _videoRepositoryManager.VideoRepository);
    }

    #region Dependencies

    private readonly IProjectManager _projectManager;

    private readonly IVideoRepositoryManager _videoRepositoryManager;

    private readonly IVideoProviderManager _videoProviderManager;

    #endregion
}