using System.Collections.ObjectModel;
using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerActionBar;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerActionBarTests;

[TestFixture]
[TestOf(typeof(VideoPlayerActionBarViewModel))]
public class VideoPlayerActionBarViewModelTest : BaseViewModelTest
{
    private Mock<IVideoProviderManager> _videoProviderManagerMock = null!;
    private Mock<IVideosExplorerViewModel> _videosExplorerViewModelMock = null!;
    private VideoPlayerActionBarViewModel _actionBar = null!;

    public override void Setup()
    {
        base.Setup();

        _videosExplorerViewModelMock = ViewModelProviderMock.CreateViewModelMock<IVideosExplorerViewModel>();
        _videoProviderManagerMock = DependencyMock.CreateMockDependency<IVideoProviderManager>();
        _actionBar = new VideoPlayerActionBarViewModel(DependencyMock.Object);
    }

    private void SetupSelectedVideo(VideoViewModel video)
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(video);
    }

    private void SetupVideos(int nrVideos)
    {
        var videos = new ObservableCollection<VideoViewModel>();
        for (var i = 0; i < nrVideos; i++)
        {
            videos.Add(VideoExamples.GetVideoViewModelExample());
        }

        _videosExplorerViewModelMock.SetupGet(m => m.Videos)
            .Returns(videos);
    }

    [Test]
    [TestCase(0, false, false)]
    [TestCase(0, true, false)]
    [TestCase(1, true, true)]
    [TestCase(1, false, false)]
    [TestCase(2, true, true)]
    public void PreviousCommandCanExecute(int selectedIndex, bool videoSelected, bool expected)
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedIndex).Returns(selectedIndex);
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(videoSelected ? VideoExamples.GetVideoViewModelExample() : null);

        Assert.That(_actionBar.Previous.CanExecute(null), Is.EqualTo(expected));
    }

    [Test]
    public void PreviousCommandReducesSelectedIndex()
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedIndex).Returns(1);
        _actionBar.Previous.Execute(null);

        _videosExplorerViewModelMock.VerifySet(m => m.SelectedIndex = 0);
    }

    [Test]
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void SkipCommandCanExecute(bool videoSelected, bool expected)
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(videoSelected ? VideoExamples.GetVideoViewModelExample() : null);
        Assert.That(_actionBar.Skip.CanExecute(null), Is.EqualTo(expected));
    }

    [Test]
    public void SkipCommandSetsVideoStatusToSkipped()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        SetupSelectedVideo(video);
        SetupVideos(5);
        _actionBar.Skip.Execute(null);

        Assert.That(video.VideoStatus, Is.EqualTo(VideoStatus.Skipped));
    }

    [Test]
    public void SkipCommandCallsVideoProviderManagerNext()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(2);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _actionBar.Skip.Execute(null);
        _videoProviderManagerMock.Verify(m => m.Next());
    }

    [Test]
    public void SkipCommandNavigatesToNextVideo()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _actionBar.Skip.Execute(null);
        _videosExplorerViewModelMock.VerifySet(m => m.SelectedIndex = 2);
    }

    [Test]
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void FinishCommandCanExecute(bool videoSelected, bool expected)
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(videoSelected ? VideoExamples.GetVideoViewModelExample() : null);
        Assert.That(_actionBar.Finish.CanExecute(null), Is.EqualTo(expected));
    }

    [Test]
    public void FinishCommandSetsVideoStatusToReadyForExport()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        SetupSelectedVideo(video);
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _actionBar.Finish.Execute(null);
        Assert.That(video.VideoStatus, Is.EqualTo(VideoStatus.ReadyForExport));
    }

    [Test]
    public void FinishCommandCallsVideoProviderManagerNext()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(4);
        _actionBar.Finish.Execute(null);
        _videoProviderManagerMock.Verify(m => m.Next());
    }

    [Test]
    public void FinishCommandNavigatesToNextVideo()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _actionBar.Finish.Execute(null);
        _videosExplorerViewModelMock.VerifySet(m => m.SelectedIndex = 2);
    }
}