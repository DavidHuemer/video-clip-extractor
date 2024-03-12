using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using Moq;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Provider;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;

[TestFixture]
[TestOf(typeof(VideoRepositoryExplorerWindowViewModel))]
public class VideoRepositoryExplorerWindowViewModelTest : BaseViewModelTest
{
    private Mock<IWindowService> _windowServiceMock = null!;
    private Mock<IVideoRepositoryProvider> _videoRepositoryProviderMock = null!;
    private VideoRepositoryExplorerWindowViewModel _videoRepositoryExplorerWindowViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _windowServiceMock = DependencyMock.CreateMockDependency<IWindowService>();
        _videoRepositoryProviderMock = DependencyMock.CreateMockDependency<IVideoRepositoryProvider>();

        var drive = new ExampleVideoRepositoryDrive("C:\\");
        _videoRepositoryProviderMock.Setup(x => x.GetDrives()).Returns([
            drive,
        ]);

        _videoRepositoryExplorerWindowViewModel = new VideoRepositoryExplorerWindowViewModel(DependencyMock.Object);
    }

    [Test]
    public void RootIsNotNull()
    {
        Assert.That(_videoRepositoryExplorerWindowViewModel.Root, Is.Not.Null);
    }

    [Test]
    public void RootContainsDrives()
    {
        Assert.That(_videoRepositoryExplorerWindowViewModel.Root.Count, Is.EqualTo(1));
    }

    [Test]
    public void SelectedItemIsNullAtBeginning()
    {
        Assert.That(_videoRepositoryExplorerWindowViewModel.SelectedItem, Is.Null);
    }

    [Test]
    public void OkCommandIsDisabledWhenSelectedItemIsNull()
    {
        Assert.That(_videoRepositoryExplorerWindowViewModel.Ok.CanExecute(null), Is.False);
    }

    [Test]
    public void OkCommandIsEnabledWhenSelectedItemIsNotNull()
    {
        var repoItem = new ExampleVideoRepositoryItem();
        _videoRepositoryExplorerWindowViewModel.SelectedItem = repoItem;
        Assert.That(_videoRepositoryExplorerWindowViewModel.Ok.CanExecute(null), Is.True);
    }

    [Test]
    public void OkCommandInvokesVideoRepositoryBlueprintSelectedEvent()
    {
        var repoItem = new ExampleVideoRepositoryItem();
        _videoRepositoryExplorerWindowViewModel.SelectedItem = repoItem;
        var eventRaised = false;
        _videoRepositoryExplorerWindowViewModel.VideoRepositoryBlueprintSelected +=
            (sender, args) => eventRaised = true;
        _videoRepositoryExplorerWindowViewModel.Ok.Execute(null);
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void OkCommandWithNullSelectedItemDoesNothing()
    {
        var eventRaised = false;
        _videoRepositoryExplorerWindowViewModel.VideoRepositoryBlueprintSelected +=
            (sender, args) => eventRaised = true;
        _videoRepositoryExplorerWindowViewModel.Ok.Execute(null);
        Assert.That(eventRaised, Is.False);
    }

    [Test]
    public void OkCommandClosesWindow()
    {
        var windowMock = new Mock<IWindow>();
        _windowServiceMock.Setup(x => x.GetWindow(It.IsAny<WindowViewModel>()))
            .Returns(windowMock.Object);
        _videoRepositoryExplorerWindowViewModel.Show();

        var repoItem = new ExampleVideoRepositoryItem();
        _videoRepositoryExplorerWindowViewModel.SelectedItem = repoItem;

        _videoRepositoryExplorerWindowViewModel.Ok.Execute(null);
        windowMock.Verify(x => x.Close(), Times.Once);
    }
}