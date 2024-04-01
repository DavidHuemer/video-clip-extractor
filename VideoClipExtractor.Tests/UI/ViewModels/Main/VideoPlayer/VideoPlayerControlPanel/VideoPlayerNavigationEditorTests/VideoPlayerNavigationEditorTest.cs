using Moq;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionFactory;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigationEditor;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.
    VideoPlayerNavigationEditorTests;

[TestFixture]
[TestOf(typeof(VideoPlayerNavigationEditor))]
public class VideoPlayerNavigationEditorTest : BaseViewModelTest
{
    private Mock<IVideoPositionFactory> _videoPositionFactory = null!;
    private Mock<IVideoPositionService> _videoPositionService = null!;
    private ViewModelMock<IFrameNavigationViewModel> _frameNavigationViewModel = null!;
    private VideoPlayerNavigationEditor _videoPlayerNavigationEditor = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPositionFactory = DependencyMock.CreateMockDependency<IVideoPositionFactory>();
        _videoPositionService = DependencyMock.CreateMockDependency<IVideoPositionService>();
        _frameNavigationViewModel = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        _videoPlayerNavigationEditor = new VideoPlayerNavigationEditor(DependencyMock.Object);
    }

    [Test]
    public void FrameIsCorrectlyReturned()
    {
        var videoPosition = new VideoPosition(TimeSpan.FromSeconds(10), 50);
        _frameNavigationViewModel.SetupGet(x => x.VideoPosition).Returns(videoPosition);
        var result = _videoPlayerNavigationEditor.Frame;
        Assert.That(videoPosition.Frame, Is.EqualTo(result));
    }

    [Test]
    public void VideoPositionIsCorrectlyReturned()
    {
        var videoPosition = new VideoPosition(TimeSpan.FromSeconds(10), 50);
        _frameNavigationViewModel.SetupGet(x => x.VideoPosition).Returns(videoPosition);
        var result = _videoPlayerNavigationEditor.VideoPosition;
        Assert.That(videoPosition.ToString(), Is.EqualTo(result));
    }

    [Test]
    public void FrameIsCorrectlySet()
    {
        var oldVideoPosition = new VideoPosition(TimeSpan.FromSeconds(20), 50);
        _frameNavigationViewModel.SetupGet(x => x.VideoPosition).Returns(oldVideoPosition);

        var newVideoPosition = new VideoPosition(TimeSpan.FromSeconds(10), 50);
        _videoPositionFactory.Setup(x => x.GetVideoPositionByFrame(50)).Returns(newVideoPosition);

        _videoPlayerNavigationEditor.Frame = 50;
        _videoPositionService.Verify(x => x.RequestPositionChange(newVideoPosition), Times.Once);
    }

    [Test]
    public void VideoPositionIsCorrectlySet()
    {
        var oldVideoPosition = new VideoPosition(TimeSpan.FromSeconds(20), 50);
        _frameNavigationViewModel.SetupGet(x => x.VideoPosition).Returns(oldVideoPosition);

        var newVideoPosition = new VideoPosition(TimeSpan.FromSeconds(10), 50);
        _videoPositionFactory.Setup(x => x.GetVideoPositionByString("00:00:00:15")).Returns(newVideoPosition);

        _videoPlayerNavigationEditor.VideoPosition = "00:00:00:15";
        _videoPositionService.Verify(x => x.RequestPositionChange(newVideoPosition), Times.Once);
    }

    [Test]
    public void VideoPositionNotSetWhenStringIsInvalid()
    {
        var oldVideoPosition = new VideoPosition(TimeSpan.FromSeconds(20), 50);
        _frameNavigationViewModel.SetupGet(x => x.VideoPosition).Returns(oldVideoPosition);

        _videoPositionFactory.Setup(x => x.GetVideoPositionByString("00:00:00:15:00")).Throws(new ArgumentException());
        _videoPlayerNavigationEditor.VideoPosition = "00:00:00:15:00";
        _videoPositionService.Verify(x => x.RequestPositionChange(It.IsAny<VideoPosition>()), Times.Never);
    }

    [Test]
    public void FrameNavigationPropertyChangeInvokesPropertyChanged()
    {
        var frameInvoked = false;
        var videoPositionInvoked = false;

        _videoPlayerNavigationEditor.PropertyChanged += (_, args) =>
        {
            switch (args.PropertyName)
            {
                case nameof(VideoPlayerNavigationEditor.Frame):
                    frameInvoked = true;
                    break;
                case nameof(VideoPlayerNavigationEditor.VideoPosition):
                    videoPositionInvoked = true;
                    break;
            }
        };

        _frameNavigationViewModel.RaisePropertyChanged(nameof(IFrameNavigationViewModel.VideoPosition));

        Assert.Multiple(() =>
        {
            Assert.That(frameInvoked, Is.True);
            Assert.That(videoPositionInvoked, Is.True);
        });
    }

    [Test]
    public void FrameNavigationPropertyDoesNotInvokeVideoPositionPropertyChangeWhenEditing()
    {
        var frameInvoked = false;
        var videoPositionInvoked = false;

        _videoPlayerNavigationEditor.PropertyChanged += (_, args) =>
        {
            switch (args.PropertyName)
            {
                case nameof(VideoPlayerNavigationEditor.Frame):
                    frameInvoked = true;
                    break;
                case nameof(VideoPlayerNavigationEditor.VideoPosition):
                    videoPositionInvoked = true;
                    break;
            }
        };

        _videoPlayerNavigationEditor.StartVideoPositionEdit();
        _frameNavigationViewModel.RaisePropertyChanged(nameof(IFrameNavigationViewModel.VideoPosition));

        Assert.Multiple(() =>
        {
            Assert.That(frameInvoked, Is.True);
            Assert.That(videoPositionInvoked, Is.False);
        });
    }

    [Test]
    public void FrameNavigationPropertyDoesInvokeVideoPositionPropertyChangeWhenEditingIsEnded()
    {
        var frameInvoked = false;
        var videoPositionInvoked = false;

        _videoPlayerNavigationEditor.PropertyChanged += (_, args) =>
        {
            switch (args.PropertyName)
            {
                case nameof(VideoPlayerNavigationEditor.Frame):
                    frameInvoked = true;
                    break;
                case nameof(VideoPlayerNavigationEditor.VideoPosition):
                    videoPositionInvoked = true;
                    break;
            }
        };

        _videoPlayerNavigationEditor.StartVideoPositionEdit();
        _videoPlayerNavigationEditor.EndVideoPositionEdit();
        _frameNavigationViewModel.RaisePropertyChanged(nameof(IFrameNavigationViewModel.VideoPosition));

        Assert.Multiple(() =>
        {
            Assert.That(frameInvoked, Is.True);
            Assert.That(videoPositionInvoked, Is.True);
        });
    }
}