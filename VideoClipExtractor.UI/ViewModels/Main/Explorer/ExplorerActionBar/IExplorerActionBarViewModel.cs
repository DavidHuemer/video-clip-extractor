using System.Windows.Input;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer.ExplorerActionBar;

public interface IExplorerActionBarViewModel : IBaseViewModel
{
    public Project? Project { set; }

    ICommand RefreshVideos { get; }

    ICommand ExportVideos { get; }
}