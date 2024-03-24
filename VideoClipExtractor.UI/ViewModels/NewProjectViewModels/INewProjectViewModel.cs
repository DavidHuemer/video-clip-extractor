using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.UI.ViewModels.NewProjectViewModels;

public interface INewProjectViewModel : IBaseViewModel
{
    event Action<Project>? ProjectCreated;
}