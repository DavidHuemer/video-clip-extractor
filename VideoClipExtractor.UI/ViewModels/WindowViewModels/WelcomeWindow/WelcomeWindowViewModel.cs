using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;
using VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels.WelcomeWindow;

[Singleton]
public class WelcomeWindowViewModel : WindowViewModel, IWelcomeWindowViewModel
{
    #region Private Fields

    private readonly IWelcomeViewModel _welcomeViewModel;

    #endregion

    public WelcomeWindowViewModel(IDependencyProvider provider) : base(provider)
    {
        _welcomeViewModel = ViewModelProvider.Get<IWelcomeViewModel>();
        _welcomeViewModel.NewProjectRequested += NewProjectRequested;
        CurrentControl = _welcomeViewModel;

        provider.GetDependency<IProjectManager>().ProjectOpened += ProjectOpened;
    }

    private void ProjectOpened(object? sender, ProjectOpenedEventArgs e) =>
        CloseWindow();

    #region Welcome Requests

    private void NewProjectRequested(object? sender, EventArgs e)
    {
        var newProjectViewModel = ViewModelProvider.Get<INewProjectViewModel>();
        CurrentControl = newProjectViewModel;
        ShowBackButton = true;
    }

    #endregion

    #region Properties

    public bool ShowBackButton { get; set; }

    public IBaseViewModel CurrentControl { get; private set; }

    #endregion

    #region Commands

    public ICommand GoBack => new RelayCommand<string>(DoGoBack, _ => true);

    private void DoGoBack(string? obj)
    {
        CurrentControl = _welcomeViewModel;
        ShowBackButton = false;
    }

    #endregion
}