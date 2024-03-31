using System.ComponentModel;
using Moq;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.Managers.VideoManager;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.Tests.UI.Managers.VideoManagerTests;

[TestFixture]
[TestOf(typeof(VideoManager))]
public class VideoManagerTest : BaseViewModelTest
{
    private Mock<IVideosExplorerViewModel> _videosExplorerMock = null!;
    private VideoManager _videoManager = null!;

    public override void Setup()
    {
        base.Setup();
        _videosExplorerMock = ViewModelProviderMock.CreateViewModelMock<IVideosExplorerViewModel>();
        _videoManager = new VideoManager(DependencyMock.Object);
    }

    [Test]
    public void GetVideoReturnsExplorerVideo()
    {
        var expect = VideoExamples.GetVideoViewModelExample();
        _videosExplorerMock.SetupGet(x => x.SelectedVideo).Returns(expect);
        var result = _videoManager.Video;

        Assert.That(result, Is.EqualTo(expect));
    }

    [Test]
    public void PropertyChangedOnExplorerRaisesEvent()
    {
        VideoViewModel? result = null;
        _videoManager.VideoChanged += (v) => result = v;

        var expect = VideoExamples.GetVideoViewModelExample();
        _videosExplorerMock.SetupGet(x => x.SelectedVideo).Returns(expect);
        _videosExplorerMock.Raise(x => x.PropertyChanged += null, this,
            new PropertyChangedEventArgs(nameof(IVideosExplorerViewModel.SelectedVideo)));

        Assert.That(result, Is.EqualTo(expect));
    }
}