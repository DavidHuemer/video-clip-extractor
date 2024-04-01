using FFMpeg.Wrapper.Data;
using Moq;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigationEditor;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer;

[TestFixture]
[TestOf(typeof(VideoPlayerNavigationViewModel))]
public class VideoPlayerNavigationViewModelTests : BaseViewModelTest
{
    private Mock<IVideoPlayerNavigationEditor> _videoPlayerNavigationEditor = null!;

    private VideoPlayerNavigationViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPlayerNavigationEditor = ViewModelProviderMock.CreateViewModelMock<IVideoPlayerNavigationEditor>();
        _viewModel = new VideoPlayerNavigationViewModel(DependencyMock.Object);
    }

    [Test]
    public void VideoPlayerNavigationEditorIsSet()
    {
        Assert.That(_viewModel.VideoPlayerNavigationEditor, Is.EqualTo(_videoPlayerNavigationEditor.Object));
    }

    [Test]
    public void VideoLengthNullAtBeginning()
    {
        Assert.That(_viewModel.VideoLength, Is.Null);
    }

    [Test]
    public void FrameCountIsZeroAtBeginning()
    {
        Assert.That(_viewModel.FrameCount, Is.Zero);
    }

    [Test]
    public void VideoLengthIsSetWhenVideoIsSet()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var info = GetVideoInfo();
        video.VideoInfo = info;
        _viewModel.Video = video;

        var expected = new VideoPosition(info.Duration, info.FrameRate);
        Assert.That(_viewModel.VideoLength, Is.EqualTo(expected));
    }

    [Test]
    public void FrameCountIsSetWhenVideoIsSet()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = GetVideoInfo();
        _viewModel.Video = video;

        Assert.That(_viewModel.FrameCount, Is.EqualTo(150));
    }

    [Test]
    public void VideoLengthIsSetToNullWhenVideoIsNull()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = GetVideoInfo();
        _viewModel.Video = video;

        _viewModel.Video = null;

        Assert.That(_viewModel.VideoLength, Is.Null);
    }

    private VideoInfo GetVideoInfo()
    {
        var timeSpan = new TimeSpan(0, 0, 0, 5, 0);
        const double frameRate = 30;
        return new VideoInfo(timeSpan, frameRate);
    }
}