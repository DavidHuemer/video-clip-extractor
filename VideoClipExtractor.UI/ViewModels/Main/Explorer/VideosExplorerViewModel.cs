using System.Collections.ObjectModel;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.WorkspaceManager;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.Explorer.ExplorerActionBar;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

/// <summary>
///     The view model for the videos explorer
/// </summary>
[Singleton]
public class VideosExplorerViewModel : BaseViewModelContainer, IVideosExplorerViewModel
{
    private readonly IWorkspaceManager _workspaceManager;

    public VideosExplorerViewModel(IDependencyProvider provider) : base(provider)
    {
        ActionBar = ViewModelProvider.Get<IExplorerActionBarViewModel>();
        _workspaceManager = provider.GetDependency<IWorkspaceManager>();
        _workspaceManager.VideoAdded += OnVideoAdded;
        _workspaceManager.Clear += OnWorkspaceCleared;
    }

    private void OnWorkspaceCleared(object? sender, EventArgs e)
    {
        Videos.Clear();
        SelectedVideo = null;
    }

    private void OnVideoAdded(VideoViewModel video)
    {
        Videos.Add(video);
        SelectedVideo = video;
    }

    #region Properties

    public IExplorerActionBarViewModel ActionBar { get; }

    public Project Project
    {
        set => ActionBar.Project = value;
    }

    /// <summary>
    /// The currently opened videos
    /// </summary>
    public ObservableCollection<VideoViewModel> Videos { get; } = [];

    public VideoViewModel? SelectedVideo { get; set; }

    public int SelectedIndex { get; set; }

    #endregion
}