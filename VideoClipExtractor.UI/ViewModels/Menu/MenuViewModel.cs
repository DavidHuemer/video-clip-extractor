using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.UI.Managers.Project.OpenProjectManager;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.NewProjectWindow;

namespace VideoClipExtractor.UI.ViewModels.Menu;

[Singleton]
public class MenuViewModel : BaseViewModelContainer, IMenuViewModel
{
    private readonly IProjectManager _projectManager;

    public MenuViewModel(IDependencyProvider provider) : base(provider)
    {
        _projectManager = provider.GetDependency<IProjectManager>();
        _projectManager.ProjectChanged += project => CanSave = project != null;
    }

    #region Properties

    [DoNotNotify] private bool CanSave { get; set; }

    #endregion

    #region Commands

    public ICommand NewProject => new RelayCommand<string>(DoNewProject, _ => true);

    private void DoNewProject(string? obj)
    {
        var newProjectWindow = ViewModelProvider.Get<INewProjectWindowViewModel>();
        newProjectWindow.ShowDialog();
    }

    public ICommand OpenProject => new RelayCommand<string>(DoOpenProject, _ => true);

    private void DoOpenProject(string? obj)
    {
        DependencyProvider.GetDependency<IOpenProjectManager>().OpenProjectByExplorer();
    }

    public ICommand SaveProject => new RelayCommand<string>(DoSaveProject, _ => CanSave);

    private void DoSaveProject(string? obj)
    {
        _projectManager.StoreProject();
    }

    #endregion
}