using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels.NewProjectWindow;

[Transient]
public class NewProjectWindowViewModel : WindowViewModel, INewProjectWindowViewModel
{
    public NewProjectWindowViewModel(IDependencyProvider provider) : base(provider)
    {
        NewProjectViewModel = ViewModelProvider.Get<INewProjectViewModel>();
        NewProjectViewModel.ProjectCreated += OnProjectCreated;
    }

    public INewProjectViewModel NewProjectViewModel { get; }

    private void OnProjectCreated(Project obj)
    {
        CloseWindow();
    }
}