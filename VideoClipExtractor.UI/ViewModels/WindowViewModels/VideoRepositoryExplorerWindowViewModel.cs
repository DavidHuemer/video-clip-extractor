using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using BaseUI.ViewModels.Tree;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Provider;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.VideoRepos.Explorer;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

/// <summary>
/// Responsible for browsing a video repository
/// </summary>
public class VideoRepositoryExplorerWindowViewModel : WindowViewModel
{
    public VideoRepositoryExplorerWindowViewModel(IDependencyProvider provider)
    {
        var driveProvider = provider.GetDependency<IVideoRepositoryProvider>();
        Root = new ObservableCollection<VideoRepositoryDrive>(driveProvider.GetDrives());
    }

    #region Events

    public event EventHandler<VideoRepositoryBlueprintEventArgs>? VideoRepositoryBlueprintSelected;

    #endregion

    #region Properties

    public ObservableCollection<VideoRepositoryDrive> Root { get; }

    public BaseTreeViewItem? SelectedItem { get; set; }

    private VideoRepositoryItem? SelectedVideoRepoItem =>
        SelectedItem as VideoRepositoryItem;

    #endregion

    #region Commands

    public ICommand Ok => new RelayCommand<string>(DoOk, _ => SelectedVideoRepoItem != null);

    private void DoOk(string? obj)
    {
        if (SelectedVideoRepoItem == null)
            return;

        var blueprint = SelectedVideoRepoItem.GetBlueprint();
        VideoRepositoryBlueprintSelected?.Invoke(this, new VideoRepositoryBlueprintEventArgs(blueprint));
        CloseWindow();
    }

    #endregion
}