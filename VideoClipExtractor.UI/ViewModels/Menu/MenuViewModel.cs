using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.UI.ViewModels.Menu;

public class MenuViewModel(IDependencyProvider provider) : BaseViewModel
{
    #region Properties

    [DoNotNotify] public Project? Project { get; set; }

    #endregion

    #region Commands

    public ICommand NewProject => new RelayCommand<string>(DoNewProject, _ => true);

    private void DoNewProject(string? obj)
    {
        Console.WriteLine("New Project");
    }

    public ICommand SaveProject => new RelayCommand<string>(DoSaveProject, _ => Project != null);

    private void DoSaveProject(string? obj) =>
        provider.GetDependency<IProjectManager>().StoreProject();

    #endregion
}