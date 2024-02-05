using Moq;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.Explorer;

public class VideoExplorerViewModelTests : BaseDependencyTest
{
    private Mock<IVideoManager> _videoManagerMock = null!;
    private Mock<IVideoProviderManager> _videoProviderManagerMock = null!;

    private VideosExplorerViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoProviderManagerMock = new Mock<IVideoProviderManager>();
        _videoManagerMock = new Mock<IVideoManager>();

        AddMockDependency(_videoProviderManagerMock);
        AddMockDependency(_videoManagerMock);
        _viewModel = new VideosExplorerViewModel(DependencyMock.Object);
    }

    [Test]
    public void VideosAreEmptyOnCreation()
    {
        Assert.That(_viewModel.Videos, Is.Empty);
    }

    [Test]
    public void SelectedVideoIsNullOnCreation()
    {
        Assert.That(_viewModel.SelectedVideo, Is.Null);
    }

    [Test]
    public void VideosAreAddedWhenVideoAddedEventIsRaised()
    {
        var video = VideoExamples.GetVideoExample();
        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, new VideoEventArgs(video));
        Assert.That(_viewModel.Videos, Has.Member(video));
    }

    [Test]
    public void SelectedVideoIsSetWhenVideoAddedEventIsRaised()
    {
        var video = VideoExamples.GetVideoExample();
        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, new VideoEventArgs(video));

        Assert.That(_viewModel.SelectedVideo, Is.Not.Null);
    }

    [Test]
    public void SelectedVideoSetsVideoManagerVideo()
    {
        var video = VideoExamples.GetVideoExample();
        _viewModel.SelectedVideo = video;

        _videoManagerMock.VerifySet(m => m.Video = video, Times.Once);
    }
}