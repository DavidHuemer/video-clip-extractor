using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Managers.Project.OpenProjectManager;
using VideoClipExtractor.UI.ViewModels.Menu;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.NewProjectWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.Menu;

[TestFixture]
[TestOf(typeof(MenuViewModel))]
public class MenuViewModelTest : BaseViewModelTest
{
    private Mock<IProjectManager> _projectManager = null!;
    private Mock<INewProjectWindowViewModel> _newProjectWindowViewModel = null!;
    private Mock<IOpenProjectManager> _openProjectManager = null!;

    private MenuViewModel _menuViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _newProjectWindowViewModel = ViewModelProviderMock.CreateViewModelMock<INewProjectWindowViewModel>();
        _openProjectManager = DependencyMock.CreateMockDependency<IOpenProjectManager>();
        _menuViewModel = new MenuViewModel(DependencyMock.Object);
    }

    [Test]
    public void NewProjectIsAllowed()
    {
        Assert.IsTrue(_menuViewModel.NewProject.CanExecute(null));
    }

    [Test]
    public void NewProjectShowsWindow()
    {
        _menuViewModel.NewProject.Execute(null);
        _newProjectWindowViewModel.Verify(x => x.ShowDialog(), Times.Once);
    }

    [Test]
    public void OpenProjectIsAllowed()
    {
        Assert.IsTrue(_menuViewModel.OpenProject.CanExecute(null));
    }

    [Test]
    public void OpenProjectOpensExplorer()
    {
        _menuViewModel.OpenProject.Execute(null);
        _openProjectManager.Verify(x => x.OpenProjectByExplorer(), Times.Once);
    }

    [Test]
    public void SaveProjectNotAllowedAtBeginning()
    {
        Assert.IsFalse(_menuViewModel.SaveProject.CanExecute(null));
    }

    [Test]
    public void SaveProjectAllowedWhenProjectChanged()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.Raise(x => x.ProjectChanged += null, project);
        Assert.IsTrue(_menuViewModel.SaveProject.CanExecute(null));
    }

    [Test]
    public void SaveProjectStoresProject()
    {
        var project = ProjectExamples.GetExampleProject();
        _projectManager.Raise(x => x.ProjectChanged += null, project);
        _menuViewModel.SaveProject.Execute(null);
        _projectManager.Verify(x => x.StoreProject(), Times.Once);
    }
}