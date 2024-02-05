using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Data;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

public class WelcomeViewModel(IDependencyProvider provider) : BaseViewModel
{
    private void OpenRecentlyOpenedFile(RecentlyOpenedFileInfo fileInfo)
    {
        OpenRecentProjectRequested?.Invoke(null, new OpenRecentlyOpenedEventArgs(fileInfo.Path));
    }

    #region Events

    public event EventHandler? NewProjectRequested;

    public event EventHandler? OpenProjectRequested;

    public event EventHandler<OpenRecentlyOpenedEventArgs>? OpenRecentProjectRequested;

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
            _selectedRecentlyOpenedFile = value;
            OnPropertyChanged();

            if (value is null)
                return;

            OpenRecentlyOpenedFile(value);
        }
    }

    #endregion

    #region Commands

    public ICommand NewProject => new RelayCommand<string>(DoNewProject, _ => true);

    private void DoNewProject(string? obj)
    {
        NewProjectRequested?.Invoke(null, EventArgs.Empty);
    }

    public ICommand OpenProject => new RelayCommand<string>(DoOpenProject, _ => true);

    private void DoOpenProject(string? obj)
    {
        OpenProjectRequested?.Invoke(null, EventArgs.Empty);
    }

    #endregion
}