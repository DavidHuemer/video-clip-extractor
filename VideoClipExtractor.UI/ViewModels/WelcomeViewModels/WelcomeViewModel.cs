using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Data;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

public class WelcomeViewModel(IDependencyProvider provider) : BaseViewModel
{
    #region Properties

    public ObservableCollection<RecentlyOpenedFileInfo> RecentlyOpenedFiles { get; } =
        new(provider.GetDependency<IRecentlyOpenedFilesService>().GetRecentlyOpenedFiles());

    #endregion

    #region Events

    public event EventHandler? NewProjectRequested;

    public event EventHandler? OpenProjectRequested;

    #endregion

    #region Commands

    public ICommand NewProject => new RelayCommand<string>(DoNewProject, _ => true);

    private void DoNewProject(string? obj) => NewProjectRequested?.Invoke(null, EventArgs.Empty);

    public ICommand OpenProject => new RelayCommand<string>(DoOpenProject, _ => true);

    private void DoOpenProject(string? obj) => OpenProjectRequested?.Invoke(null, EventArgs.Empty);

    #endregion
}