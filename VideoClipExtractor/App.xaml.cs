﻿using System.Windows;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.FileServices;
using BaseUI.Services.FileServices.Implementations;
using BaseUI.Services.WindowService;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Core.Services.VideoRepositoryServices;
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
        dependencyProvider.AddTransientDependency<IFileExplorer, FileExplorer>();
        dependencyProvider.AddTransientDependency<IProjectFileExplorer, ProjectFileExplorer>();
        dependencyProvider.AddTransientDependency<IVideoRepositoryProvider, VideoRepositoryProvider>();
        dependencyProvider.AddTransientDependency<IProjectSerializer, JsonProjectSerializer>();
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