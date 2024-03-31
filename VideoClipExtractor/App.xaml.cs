using System.Windows;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using BaseUI.Services.WindowService;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Core.Services.RecentlyOpened;
using VideoClipExtractor.UI.Services.FileServices;
using VideoClipExtractor.UI.ViewModels.WindowViewModels;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.ExtractionWindow;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.NewProjectWindow;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.WelcomeWindow;
using VideoClipExtractor.UI.Windows;

namespace VideoClipExtractor;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        Console.WriteLine("Application Startup");
        var dependencyProvider = new DependencyProvider();
        BaseUiDependencies.AddBaseUiDependencies(dependencyProvider);

        dependencyProvider.AddTransientDependency<IProjectFileExplorer, ProjectFileExplorer>();
        dependencyProvider.AddTransientDependency<IProjectSerializer, JsonProjectSerializer>();
        dependencyProvider.AddTransientDependency<IRecentlyOpenedFilesService, RecentlyOpenedFilesService>();
        SetupWindows(dependencyProvider);
        new MainWindowViewModel(dependencyProvider).Show();
    }

    private void SetupWindows(IDependencyProvider provider)
    {
        var windowService = provider.GetDependency<IWindowService>();
        windowService.Register<MainWindowViewModel, MainWindow>();
        windowService.Register<WelcomeWindowViewModel, WelcomeWindow>();
        windowService.Register<VideoRepositoryExplorerWindowViewModel, VideoRepositoryExplorerWindow>();
        windowService.Register<VideosSetupWindowViewModel, VideosSetupWindow>();
        windowService.Register<ExtractionWindowViewModel, ExtractionWindow>();
        windowService.Register<NewProjectWindowViewModel, NewProjectWindow>();
    }
}