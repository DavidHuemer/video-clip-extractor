﻿using BaseUI.Services.Dialogs;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;

namespace VideoClipExtractor.UI.Managers.ProjectManagers.OpenProjectManager;

[Singleton]
public class OpenProjectManager(IDependencyProvider provider) : IOpenProjectManager
{
    private readonly IProjectManager _projectManager = provider.GetDependency<IProjectManager>();

    public async Task OpenProjectByExplorer()
    {
        var path = provider.GetDependency<IProjectFileExplorer>().GetOpenProjectFilePath();
        await OpenProjectByPath(path);
    }

    public async Task OpenProjectByPath(string projectPath)
    {
        if (string.IsNullOrEmpty(projectPath)) return;

        try
        {
            var projectSerializer = provider.GetDependency<IProjectSerializer>();
            var project = await projectSerializer.LoadProject(projectPath);
            _projectManager.SetOpenedProject(project, projectPath);
            HandleSetupVideos(project);
        }
        catch (Exception e)
        {
            provider.GetDependency<IDialogService>().Show(e);
        }
    }

    private void HandleSetupVideos(Project project)
    {
        var sourceVideosExist = project.Videos.Any(x => !x.Checked);
        if (sourceVideosExist) return;

        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        var viewModel = viewModelProvider.Get<IVideosSetupWindowViewModel>();
        viewModel.ShowDialog();
    }
}