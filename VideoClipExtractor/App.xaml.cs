using System.Windows;
using BaseUI.Services.Basics.Time;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.Dialogs;
using BaseUI.Services.FileServices;
using BaseUI.Services.FileServices.Implementations;
using BaseUI.Services.RecentlyOpened;
using BaseUI.Services.WindowService;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Core.Services.RecentlyOpened;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Provider;
using VideoClipExtractor.UI.Services.FileServices;
using VideoClipExtractor.UI.ViewModels.WindowViewModels;
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
        dependencyProvider.AddSingletonDependency<IWindowService, WindowService>();
        dependencyProvider.AddSingletonDependency<IDialogService, DialogService>();
        dependencyProvider.AddTransientDependency<IFileExplorer, FileExplorer>();
        dependencyProvider.AddTransientDependency<IProjectFileExplorer, ProjectFileExplorer>();
        dependencyProvider.AddTransientDependency<IVideoRepositoryProvider, VideoRepositoryProvider>();
        dependencyProvider.AddTransientDependency<IProjectSerializer, JsonProjectSerializer>();
        dependencyProvider.AddSingletonDependency<IVideoRepositoryManager, VideoRepositoryManager>();
        dependencyProvider.AddTransientDependency<IVideoRepositoryBuilder, VideoRepositoryBuilder>();
        dependencyProvider.AddTransientDependency<ITimeService, TimeService>();
        dependencyProvider.AddTransientDependency<IRecentlyOpenedFilesService, RecentlyOpenedFilesService>();
        SetupWindows(dependencyProvider);

        new MainWindowViewModel(dependencyProvider).Show(dependencyProvider.GetDependency<IWindowService>());
    }

    private void SetupWindows(IDependencyProvider provider)
    {
        var windowService = provider.GetDependency<IWindowService>();
        windowService.Register<MainWindowViewModel, MainWindow>();
        windowService.Register<WelcomeWindowViewModel, WelcomeWindow>();
        windowService.Register<VideoRepositoryExplorerWindowViewModel, VideoRepositoryExplorerWindow>();
        windowService.Register<VideosSetupWindowViewModel, VideosSetupWindow>();
    }
}