﻿using System.Windows;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.Services.RecentlyOpened;
using BaseUI.Services.WindowService;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Core.Services.RecentlyOpened;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Provider;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;
using VideoClipExtractor.UI.Services.FileServices;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;
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
        BaseUiDependencies.AddBaseUiDependencies(dependencyProvider);

        dependencyProvider.AddTransientDependency<IProjectFileExplorer, ProjectFileExplorer>();
        dependencyProvider.AddTransientDependency<IVideoRepositoryProvider, VideoRepositoryProvider>();
        dependencyProvider.AddTransientDependency<IProjectSerializer, JsonProjectSerializer>();

        dependencyProvider.AddTransientDependency<IVideoRepositoryBuilder, VideoRepositoryBuilder>();
        dependencyProvider.AddTransientDependency<IRecentlyOpenedFilesService, RecentlyOpenedFilesService>();
        dependencyProvider.AddTransientDependency<IVideoCrawler, VideoCrawler>();

        dependencyProvider.AddTransientDependency<IVideoCacheService, VideoCacheService>();
        dependencyProvider.AddTransientDependency<IVideoProvider, VideoProvider>();

        AddManagers(dependencyProvider);
        SetupViewModels(dependencyProvider);
        SetupWindows(dependencyProvider);

        new MainWindowViewModel(dependencyProvider).Show(dependencyProvider.GetDependency<IWindowService>());
    }

    private static void AddManagers(IDependencyProvider provider)
    {
        provider.AddSingletonDependency<IVideoRepositoryManager, VideoRepositoryManager>();
        provider.AddSingletonDependency<IProjectManager, ProjectManager>();
        provider.AddSingletonDependency<IVideoProviderManager, VideoProviderManager>();
        provider.AddSingletonDependency<IVideoManager, VideoManager>();
    }

    private void SetupWindows(IDependencyProvider provider)
    {
        var windowService = provider.GetDependency<IWindowService>();
        windowService.Register<MainWindowViewModel, MainWindow>();
        windowService.Register<WelcomeWindowViewModel, WelcomeWindow>();
        windowService.Register<VideoRepositoryExplorerWindowViewModel, VideoRepositoryExplorerWindow>();
        windowService.Register<VideosSetupWindowViewModel, VideosSetupWindow>();
    }

    private void SetupViewModels(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        viewModelProvider.AddSingletonViewModel<IVideosExplorerViewModel, VideosExplorerViewModel>();
        viewModelProvider.AddSingletonViewModel<IVideoPlayerNavigationViewModel, VideoPlayerNavigationViewModel>();
    }
}