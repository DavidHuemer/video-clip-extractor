using BaseUI.Data;
using BaseUI.Services.RecentlyOpened;
using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Managers.ProjectManagers.OpenProjectManager;
using VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

namespace VideoClipExtractor.Tests.UI.ViewModels.WelcomeViewModels;

[TestFixture]
[TestOf(typeof(WelcomeViewModel))]
public class WelcomeViewModelTest : BaseViewModelTest
{
    private Mock<IRecentlyOpenedFilesService> _recentlyOpenedFilesServiceMock = null!;
    private Mock<IOpenProjectManager> _openProjectManagerMock = null!;
    private WelcomeViewModel _welcomeViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _recentlyOpenedFilesServiceMock = DependencyMock.CreateMockDependency<IRecentlyOpenedFilesService>();
        _recentlyOpenedFilesServiceMock.Setup(x => x.GetRecentlyOpenedFiles())
            .Returns([]);

        _openProjectManagerMock = DependencyMock.CreateMockDependency<IOpenProjectManager>();

        _welcomeViewModel = new WelcomeViewModel(DependencyMock.Object);
    }

    [Test]
    public void RecentlyOpenedFilesIsNotNullAtBeginning()
    {
        Assert.That(_welcomeViewModel.RecentlyOpenedFiles, Is.Not.Null);
    }

    [Test]
    public void SelectedRecentlyOpenedFileIsNullAtBeginning()
    {
        Assert.That(_welcomeViewModel.SelectedRecentlyOpenedFile, Is.Null);
    }

    [Test]
    public void NewProjectCanExecute()
    {
        Assert.That(_welcomeViewModel.NewProject.CanExecute(null), Is.True);
    }

    [Test]
    public void NewProjectRequestedIsRaised()
    {
        var newProjectRequestedRaised = false;
        _welcomeViewModel.NewProjectRequested += (sender, args) => newProjectRequestedRaised = true;

        _welcomeViewModel.NewProject.Execute(null);

        Assert.That(newProjectRequestedRaised, Is.True);
    }

    [Test]
    public void OpenProjectCanExecute()
    {
        Assert.That(_welcomeViewModel.OpenProject.CanExecute(null), Is.True);
    }

    [Test]
    public void OpenProjectByExplorerIsCalled()
    {
        _welcomeViewModel.OpenProject.Execute(null);
        _openProjectManagerMock.Verify(x => x.OpenProjectByExplorer(), Times.Once);
    }

    [Test]
    public void SelectedRecentlyOpenedFileIsSet()
    {
        var fileInfo = new RecentlyOpenedFileInfo()
        {
            Path = "path",
            LastOpened = DateTime.Now,
        };
        _welcomeViewModel.SelectedRecentlyOpenedFile = fileInfo;
        Assert.That(_welcomeViewModel.SelectedRecentlyOpenedFile, Is.EqualTo(fileInfo));
    }

    [Test]
    public void OpenProjectByPathIsCalled()
    {
        var fileInfo = new RecentlyOpenedFileInfo()
        {
            Path = "path",
            LastOpened = DateTime.Now,
        };
        _welcomeViewModel.SelectedRecentlyOpenedFile = fileInfo;
        _openProjectManagerMock.Verify(x => x.OpenProjectByPath(fileInfo.Path), Times.Once);
    }

    [Test]
    public void OpenProjectByPathNotCalledWhenRecentlyOpenedFileIsNull()
    {
        _welcomeViewModel.SelectedRecentlyOpenedFile = null;
        _openProjectManagerMock.Verify(x => x.OpenProjectByPath(It.IsAny<string>()), Times.Never);
    }
}