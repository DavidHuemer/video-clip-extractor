using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Data;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using BaseUI.ViewModels;
using VideoClipExtractor.UI.Managers.ProjectManagers.OpenProjectManager;

namespace VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

public class WelcomeViewModel(IDependencyProvider provider) : BaseViewModel, IWelcomeViewModel
{
    private readonly IOpenProjectManager _openProjectManager = provider.GetDependency<IOpenProjectManager>();

    #region Events

    public event EventHandler? NewProjectRequested;

    #endregion

    #region Properties

    public ObservableCollection<RecentlyOpenedFileInfo> RecentlyOpenedFiles { get; } =
        new(provider.GetDependency<IRecentlyOpenedFilesService>().GetRecentlyOpenedFiles());

    private RecentlyOpenedFileInfo? _selectedRecentlyOpenedFile;

    public RecentlyOpenedFileInfo? SelectedRecentlyOpenedFile
    {
        get => _selectedRecentlyOpenedFile;
        set
        {
            SetProperty(ref _selectedRecentlyOpenedFile, value);

            if (value is not null)
                _openProjectManager.OpenProjectByPath(value.Path);
        }
    }

    #endregion

    #region Commands

    public ICommand NewProject => new RelayCommand<string>(DoNewProject, _ => true);

    private void DoNewProject(string? obj) => NewProjectRequested?.Invoke(null, EventArgs.Empty);

    public ICommand OpenProject => new RelayCommand<string>(DoOpenProject, _ => true);

    private void DoOpenProject(string? obj) => _openProjectManager.OpenProjectByExplorer();

    #endregion
}