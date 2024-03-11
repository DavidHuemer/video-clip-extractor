using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.InstanceBuilderService;
using BaseUI.Services.WindowService;
using BaseUI.Services.WindowService.ActiveWindow;
using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.BaseUI.Services.WindowServiceTests;

[TestFixture]
[TestOf(typeof(WindowService))]
public class WindowServiceTest : BaseDependencyTest
{
    private Mock<IInstanceBuilderService> _instanceBuilderService = null!;
    private Mock<IActiveWindowManager> _activeWindowManager = null!;
    private WindowService _windowService = null!;

    public override void Setup()
    {
        base.Setup();
        _instanceBuilderService = DependencyMock.CreateMockDependency<IInstanceBuilderService>();
        _activeWindowManager = DependencyMock.CreateMockDependency<IActiveWindowManager>();
        _windowService = new WindowService(DependencyMock.Object);
    }

    [Test]
    public void WindowNotRegisteredExceptionIsThrownWhenWindowNotRegistered()
    {
        var exampleViewModel = new ExampleWindowViewModel(DependencyMock.Object);
        Assert.Throws<WindowNotRegisteredException>(() => _windowService.GetWindow(exampleViewModel));
    }

    [Test]
    public void DataContextIsSet()
    {
        _instanceBuilderService.Setup(x => x.InstantiateType<IWindow>(It.IsAny<Type>()))
            .Returns(new ExampleWindow());

        _windowService.Register<ExampleWindowViewModel, ExampleWindow>();
        var exampleViewModel = new ExampleWindowViewModel(DependencyMock.Object);

        var window = _windowService.GetWindow(exampleViewModel);
        Assert.That(window.DataContext, Is.EqualTo(exampleViewModel));
    }

    [Test]
    public void ActiveWindowManagerCalledWhenShowingWindow()
    {
        var window = new Mock<IWindow>();
        _windowService.ShowWindow(window.Object);
        _activeWindowManager.Verify(x => x.AddWindow(window.Object), Times.Once);
    }

    [Test]
    public void ActiveWindowManagerCalledWhenShowingDialog()
    {
        var window = new Mock<IWindow>();
        _windowService.ShowDialog(window.Object);
        _activeWindowManager.Verify(x => x.AddWindow(window.Object), Times.Once);
    }

    [Test]
    public void WindowShownWhenShowingWindow()
    {
        var window = new Mock<IWindow>();
        _windowService.ShowWindow(window.Object);
        window.Verify(x => x.Show(), Times.Once);
    }

    [Test]
    public void WindowShownWhenShowingDialog()
    {
        var window = new Mock<IWindow>();
        _windowService.ShowDialog(window.Object);
        window.Verify(x => x.ShowDialog(), Times.Once);
    }
}