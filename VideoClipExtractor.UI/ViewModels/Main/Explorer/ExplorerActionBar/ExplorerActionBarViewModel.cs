using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.Managers.Extraction;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer.ExplorerActionBar;

[Transient]
public class ExplorerActionBarViewModel : BaseViewModelContainer, IExplorerActionBarViewModel
{
    public ExplorerActionBarViewModel(IDependencyProvider provider) : base(provider)
    {
    }

    public Project? Project { private get; set; }

    #region Commands

    public ICommand RefreshVideos => new RelayCommand<string>(DoRefreshVideos, _ => Project != null);

    private void DoRefreshVideos(string? obj)
    {
        var vm = ViewModelProvider.Get<IVideosSetupWindowViewModel>();
        vm.ShowDialog();
    }

    public ICommand ExportVideos => new RelayCommand<string>(DoExportVideos, _ => Project != null);

    private void DoExportVideos(string? obj)
    {
        var extractionManager = DependencyProvider.GetDependency<IExtractionManager>();
        extractionManager.ExtractVideos(Project!.WorkingVideos);
    }

    #endregion
}