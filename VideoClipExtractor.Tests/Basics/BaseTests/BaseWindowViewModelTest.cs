using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using Moq;

namespace VideoClipExtractor.Tests.Basics.BaseTests;

public abstract class BaseWindowViewModelTest : BaseViewModelTest
{
    protected Mock<IWindow> WindowMock = null!;
    protected Mock<IWindowService> WindowServiceMock = null!;

    public override void Setup()
    {
        base.Setup();
        WindowServiceMock = DependencyMock.CreateMockDependency<IWindowService>();
        WindowMock = new Mock<IWindow>();

        WindowServiceMock.Setup(ws => ws.GetWindow(It.IsAny<WindowViewModel>()))
            .Returns(WindowMock.Object);
    }
}