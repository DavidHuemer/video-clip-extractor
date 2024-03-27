using BaseUI.Exceptions.Basics;
using BaseUI.Services.Dialogs;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupResultViewModels;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupSettingsViewModels;

namespace VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels;

[Transient]
public class VideosSetupViewModel : BaseViewModelContainer, IVideosSetupViewModel
{
    public VideosSetupViewModel(IDependencyProvider provider) : base(provider)
    {
        SettingsViewModel = ViewModelProvider.Get<IVideoSetupSettingsViewModel>();
        ResultViewModel = ViewModelProvider.Get<IVideoSetupResultViewModel>();

        SettingsViewModel.LoadVideosRequested += OnLoadVideosRequested;
        ResultViewModel.VideosAdded += OnVideosAdded;

        SetupProject();
    }

    private IProjectManager ProjectManager => DependencyProvider.GetDependency<IProjectManager>();
    public event EventHandler Finish;

    private void SetupProject()
    {
        ProjectManager.ProjectChanged += (project) => { SettingsViewModel.Project = project; };

        SettingsViewModel.Project = ProjectManager.Project;
    }

    private async void OnLoadVideosRequested(VideoSetupSettings settings)
    {
        await ResultViewModel.LoadVideos();
    }

    private void OnVideosAdded(List<SourceVideo> videos)
    {
        try
        {
            var project = ProjectManager.Project;
            if (project == null)
                throw new ProjectNotSetException();

            project.Videos.Clear();
            project.Videos.AddRange(videos);
            ProjectManager.StoreProject();
            Finish?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            DependencyProvider.GetDependency<IDialogService>().Show(e);
        }
    }

    #region Properties

    public IVideoSetupSettingsViewModel SettingsViewModel { get; }

    public IVideoSetupResultViewModel ResultViewModel { get; }

    #endregion
}