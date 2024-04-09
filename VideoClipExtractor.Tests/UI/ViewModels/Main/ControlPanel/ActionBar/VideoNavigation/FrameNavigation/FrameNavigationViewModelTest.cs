using Moq;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

[TestFixture]
[TestOf(typeof(FrameNavigationViewModel))]
public class FrameNavigationViewModelTest : BaseDependencyTest
{
    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _videoPositionService = DependencyMock.CreateMockDependency<IVideoPositionService>();
        _frameNavigationViewModel = new FrameNavigationViewModel(DependencyMock.Object);
    }

    private Mock<IVideoPositionService> _videoPositionService = null!;
    private FrameNavigationViewModel _frameNavigationViewModel = null!;

    [Test]
    public void VideoPositionZeroAtBeginning()
    {
        Assert.That(_frameNavigationViewModel.VideoPosition1.Frame, Is.EqualTo(0));
    }

    [Test]
    public void GoBackwardNotAllowedAtBeginning()
    {
        Assert.IsFalse(_frameNavigationViewModel.GoBackward.CanExecute(null));
    }

    [Test]
    public void GoForwardNotAllowedAtBeginning()
    {
        Assert.IsFalse(_frameNavigationViewModel.GoForward.CanExecute(null));
    }

    [Test]
    public void GoBackwardAllowedWhenVideoIsNotNull()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _frameNavigationViewModel.Video = video;
        Assert.IsTrue(_frameNavigationViewModel.GoBackward.CanExecute(null));
    }

    [Test]
    public void GoForwardAllowedWhenVideoIsNotNull()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _frameNavigationViewModel.Video = video;
        Assert.IsTrue(_frameNavigationViewModel.GoForward.CanExecute(null));
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(200, 199)]
    public void GoBackwardCallsVideoPositionService(int current, int expectedRequestFrame)
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _frameNavigationViewModel.Video = video;
        var currentDuration = new VideoPosition1(current);
        _frameNavigationViewModel.VideoPosition1 = currentDuration;
        _frameNavigationViewModel.GoBackward.Execute(null);

        var expectedPosition = new VideoPosition(TimeSpan.Zero, 30);
        _videoPositionService.Verify(x => x.RequestPositionChange(expectedPosition), Times.Once);
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(200, 201)]
    public void GoForwardCallsVideoPositionService(int current, int expectedRequestFrame)
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _frameNavigationViewModel.Video = video;
        var currentDuration = new VideoPosition1(current);
        _frameNavigationViewModel.VideoPosition1 = currentDuration;

        _frameNavigationViewModel.GoForward.Execute(null);
        var expectedPosition = new VideoPosition(TimeSpan.Zero, 30);
        _videoPositionService.Verify(x => x.RequestPositionChange(expectedPosition), Times.Once);
    }
}