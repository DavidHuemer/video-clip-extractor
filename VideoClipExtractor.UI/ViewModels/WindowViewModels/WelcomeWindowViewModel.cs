using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;
using VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

public class WelcomeWindowViewModel : WindowViewModel
{
    public WelcomeWindowViewModel(IDependencyProvider provider)
    {
        _provider = provider;

        _welcomeViewModel.NewProjectRequested += NewProjectRequested;
        _welcomeViewModel.OpenProjectRequested += OpenProjectRequested;

        CurrentControl = _welcomeViewModel;
    }

    #region Events

    public event EventHandler<ProjectEventArgs>? ProjectChosen;

    #endregion

    private void NewProjectRequested(object? sender, EventArgs e)
    {
        var newProjectViewModel = new NewProjectViewModel(_provider);
        newProjectViewModel.ProjectCreated += ProjectCreated;
        CurrentControl = newProjectViewModel;
        ShowBackButton = true;
    }

    private void ProjectCreated(object? sender, ProjectEventArgs e)
    {
        CloseWindow();
        ProjectChosen?.Invoke(this, e);
    }

    private void OpenProjectRequested(object? sender, EventArgs e)
    {
        Console.WriteLine("Open project requested");
    }

    #region Private Fields

    private readonly IDependencyProvider _provider;

    private readonly WelcomeViewModel _welcomeViewModel = new();

    #endregion

    #region Properties

    public bool ShowBackButton { get; set; }

    public BaseViewModel CurrentControl { get; private set; }

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