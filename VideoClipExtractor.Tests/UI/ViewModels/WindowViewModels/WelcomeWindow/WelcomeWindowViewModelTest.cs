using Moq;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;
using VideoClipExtractor.UI.ViewModels.WelcomeViewModels;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.WelcomeWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels.WelcomeWindow;

[TestFixture]
[TestOf(typeof(WelcomeWindowViewModel))]
public class WelcomeWindowViewModelTest : BaseWindowViewModelTest
{
    private Mock<IWelcomeViewModel> _welcomeViewModelMock = null!;
    private Mock<INewProjectViewModel> _newProjectViewModelMock = null!;
    private Mock<IProjectManager> _projectManagerMock = null!;

    private WelcomeWindowViewModel _welcomeWindowViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _welcomeViewModelMock = ViewModelProviderMock.CreateViewModelMock<IWelcomeViewModel>();
        _newProjectViewModelMock = ViewModelProviderMock.CreateViewModelMock<INewProjectViewModel>();
        _projectManagerMock = DependencyMock.CreateMockDependency<IProjectManager>();
        _welcomeWindowViewModel = new WelcomeWindowViewModel(DependencyMock.Object);
    }


    [Test]
    public void CurrentControlIsWelcomeViewModelAtBeginning()
    {
        Assert.That(_welcomeWindowViewModel.CurrentControl, Is.EqualTo(_welcomeViewModelMock.Object));
    }

    [Test]
    public void ShowBackButtonIsFalseAtBeginning()
    {
        Assert.That(_welcomeWindowViewModel.ShowBackButton, Is.False);
    }

    [Test]
    public void CurrentControlIsNewProjectAfterNewProjectRequested()
    {
        _welcomeViewModelMock.Raise(vm => vm.NewProjectRequested += null, EventArgs.Empty);
        Assert.That(_welcomeWindowViewModel.CurrentControl, Is.EqualTo(_newProjectViewModelMock.Object));
    }

    [Test]
    public void ShowBackButtonIsTrueAfterNewProjectRequested()
    {
        _welcomeViewModelMock.Raise(vm => vm.NewProjectRequested += null, EventArgs.Empty);
        Assert.That(_welcomeWindowViewModel.ShowBackButton, Is.True);
    }

    [Test]
    public void GoBackIsAllowedAtBeginning()
    {
        Assert.That(_welcomeWindowViewModel.GoBack.CanExecute(null), Is.True);
    }

    [Test]
    public void GoBackSetsCurrentControlToWelcomeViewModel()
    {
        _welcomeViewModelMock.Raise(vm => vm.NewProjectRequested += null, EventArgs.Empty);
        _welcomeWindowViewModel.GoBack.Execute(null);
        Assert.That(_welcomeWindowViewModel.CurrentControl, Is.EqualTo(_welcomeViewModelMock.Object));
    }

    [Test]
    public void ProjectOpenedClosesWindow()
    {
        _welcomeWindowViewModel.Show();

        var project = ProjectExamples.GetExampleProject();
        _projectManagerMock.Raise(pm => pm.ProjectOpened += null, new ProjectOpenedEventArgs(project, ""));
        WindowMock.Verify(w => w.Close(), Times.Once);
    }
}