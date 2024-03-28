using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.Main;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main;

[TestFixture]
[TestOf(typeof(MainControlViewModel))]
public class MainControlViewModelTest : BaseViewModelTest
{
    private Mock<IVideosExplorerViewModel> _explorerVm = null!;
    private Mock<IProjectManager> _projectManager = null!;
    private MainControlViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _explorerVm = ViewModelProviderMock.CreateViewModelMock<IVideosExplorerViewModel>();
        _projectManager = DependencyMock.CreateMockDependency<IProjectManager>();
        _viewModel = new MainControlViewModel(DependencyMock.Object);
    }

    [Test]
    public void ProjectIsSetToExplorer()
    {
        _explorerVm.Reset();
        var project = ProjectExamples.GetExampleProject();
        _projectManager.SetupGet(x => x.Project).Returns(project);
        _viewModel = new MainControlViewModel(DependencyMock.Object);
        _explorerVm.VerifySet(x => x.Project = project);
    }

    [Test]
    public void ProjectChangeUpdatesExplorerProject()
    {
        var project = ProjectExamples.GetExampleProject();
        _explorerVm.Reset();
        _projectManager.Raise(x => x.ProjectChanged += null!, project);
        _explorerVm.VerifySet(x => x.Project = project);
    }
}