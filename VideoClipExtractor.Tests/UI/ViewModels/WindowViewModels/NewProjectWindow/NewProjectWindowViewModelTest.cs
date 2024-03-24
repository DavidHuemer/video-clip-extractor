using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.NewProjectWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels.NewProjectWindow;

[TestFixture]
[TestOf(typeof(NewProjectWindowViewModel))]
public class NewProjectWindowViewModelTest : BaseWindowViewModelTest
{
    private Mock<INewProjectViewModel> _newProjectViewModelMock = null!;
    private NewProjectWindowViewModel _newProjectWindowViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _newProjectViewModelMock = ViewModelProviderMock.CreateViewModelMock<INewProjectViewModel>();
        _newProjectWindowViewModel = new NewProjectWindowViewModel(DependencyMock.Object);
    }

    [Test]
    public void NewProjectViewModelIsSet()
    {
        Assert.That(_newProjectWindowViewModel.NewProjectViewModel, Is.EqualTo(_newProjectViewModelMock.Object));
    }

    [Test]
    public void OnProjectCreated_ClosesWindow()
    {
        _newProjectWindowViewModel.ShowDialog();
        var project = ProjectExamples.GetExampleProject();
        _newProjectViewModelMock.Raise(x => x.ProjectCreated += null, project);
        WindowMock.Verify(x => x.Close(), Times.Once);
    }
}