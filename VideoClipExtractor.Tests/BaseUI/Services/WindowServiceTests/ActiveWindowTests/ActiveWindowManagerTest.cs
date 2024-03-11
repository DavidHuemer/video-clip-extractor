using BaseUI.Services.WindowService;
using BaseUI.Services.WindowService.ActiveWindow;
using Moq;

namespace VideoClipExtractor.Tests.BaseUI.Services.WindowServiceTests.ActiveWindowTests;

[TestFixture]
[TestOf(typeof(ActiveWindowManager))]
public class ActiveWindowManagerTest
{
    [SetUp]
    public void SetUp()
    {
        _activeWindowManager = new ActiveWindowManager();
    }

    private ActiveWindowManager _activeWindowManager = null!;

    [Test]
    public void ActiveWindowNullAtBeginning()
    {
        Assert.That(_activeWindowManager.ActiveWindow, Is.Null);
    }

    [Test]
    public void ActiveWindowNotNullAfterAddingWindow()
    {
        var window = new Mock<IWindow>();
        _activeWindowManager.AddWindow(window.Object);
        Assert.That(_activeWindowManager.ActiveWindow, Is.Not.Null);
    }

    [Test]
    public void ActiveWindowNullAfterRemovingWindow()
    {
        var window = new Mock<IWindow>();
        _activeWindowManager.AddWindow(window.Object);
        window.Raise(w => w.Closed += null, window.Object, EventArgs.Empty);
        Assert.That(_activeWindowManager.ActiveWindow, Is.Null);
    }
}