using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
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

    private void NewProjectRequested(object? sender, EventArgs e)
    {
        Console.WriteLine("New project requested");
        CurrentControl = new NewProjectViewModel(_provider);
        ShowBackButton = true;
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

    public bool ShowBackButton { get; set; } = false;

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