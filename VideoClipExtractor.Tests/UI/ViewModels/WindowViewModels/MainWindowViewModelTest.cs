using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.ViewModels.Main;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Menu;
using VideoClipExtractor.UI.ViewModels.WindowViewModels;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.WelcomeWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels;

[TestFixture]
[TestOf(typeof(MainWindowViewModel))]
public class MainWindowViewModelTest : BaseViewModelTest
{
    private Mock<IWindowService> _windowServiceMock = null!;
    private Mock<IWelcomeWindowViewModel> _welcomeWindowViewModelMock = null!;
    private Mock<IMenuViewModel> _menuViewModelMock = null!;
    private Mock<IMainControlViewModel> _mainControlViewModelMock = null!;
    private Mock<IControlPanelViewModel> _controlPanelViewModelMock = null!;

    private MainWindowViewModel _mainWindowViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _windowServiceMock = DependencyMock.CreateMockDependency<IWindowService>();
        _mainControlViewModelMock = ViewModelProviderMock.CreateViewModelMock<IMainControlViewModel>();
        _menuViewModelMock = ViewModelProviderMock.CreateViewModelMock<IMenuViewModel>();
        _controlPanelViewModelMock = ViewModelProviderMock.CreateViewModelMock<IControlPanelViewModel>();
        _welcomeWindowViewModelMock = ViewModelProviderMock.CreateViewModelMock<IWelcomeWindowViewModel>();

        _mainWindowViewModel = new MainWindowViewModel(DependencyMock.Object);
    }


    [Test]
    public void ShowCallsShowWindowOfWindowService()
    {
        var windowMock = new Mock<IWindow>();

        _windowServiceMock.Setup(x => x.GetWindow(It.IsAny<WindowViewModel>()))
            .Returns(windowMock.Object);
        _mainWindowViewModel.Show();
        _windowServiceMock.Verify(x => x.ShowWindow(windowMock.Object), Times.Once);
    }

    [Test]
    public void WelcomeWindowIsShownWhenContentIsRendered()
    {
        var windowMock = new Mock<IWindow>();
        _windowServiceMock.Setup(x => x.GetWindow(It.IsAny<WindowViewModel>()))
            .Returns(windowMock.Object);
        _mainWindowViewModel.Show();
        windowMock.Raise(x => x.ContentRendered += null, windowMock.Object, null!);
        _welcomeWindowViewModelMock.Verify(x => x.ShowDialog(), Times.Once);
    }
}