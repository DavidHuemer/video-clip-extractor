using System.Collections.ObjectModel;
using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerNavigationViewModelTests : BaseViewModelTest
{
    private Mock<IVideoProviderManager> _videoProviderManagerMock = null!;
    private Mock<IVideosExplorerViewModel> _videosExplorerViewModelMock = null!;
    private VideoPlayerNavigationViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videosExplorerViewModelMock = new Mock<IVideosExplorerViewModel>();
        AddViewModel(_videosExplorerViewModelMock);

        _videoProviderManagerMock = new Mock<IVideoProviderManager>();
        AddMockDependency(_videoProviderManagerMock);
        _viewModel = new VideoPlayerNavigationViewModel(DependencyMock.Object);
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

    #region Previous

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

        Assert.That(_viewModel.Previous.CanExecute(null), Is.EqualTo(expected));
    }

    [Test]
    public void PreviousCommandReducesSelectedIndex()
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedIndex).Returns(1);
        _viewModel.Previous.Execute(null);

        _videosExplorerViewModelMock.VerifySet(m => m.SelectedIndex = 0);
    }

    #endregion

    #region Skip

    [Test]
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void SkipCommandCanExecute(bool videoSelected, bool expected)
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(videoSelected ? VideoExamples.GetVideoViewModelExample() : null);
        Assert.That(_viewModel.Skip.CanExecute(null), Is.EqualTo(expected));
    }

    [Test]
    public void SkipCommandSetsVideoStatusToSkipped()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        SetupSelectedVideo(video);
        SetupVideos(5);
        _viewModel.Skip.Execute(null);

        Assert.That(video.VideoStatus, Is.EqualTo(VideoStatus.Skipped));
    }

    [Test]
    public void SkipCommandCallsVideoProviderManagerNext()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(2);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _viewModel.Skip.Execute(null);
        _videoProviderManagerMock.Verify(m => m.Next());
    }

    [Test]
    public void SkipCommandNavigatesToNextVideo()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _viewModel.Skip.Execute(null);
        _videosExplorerViewModelMock.VerifySet(m => m.SelectedIndex = 2);
    }

    #endregion

    #region Finish

    [Test]
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void FinishCommandCanExecute(bool videoSelected, bool expected)
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(videoSelected ? VideoExamples.GetVideoViewModelExample() : null);
        Assert.That(_viewModel.Finish.CanExecute(null), Is.EqualTo(expected));
    }

    [Test]
    public void FinishCommandSetsVideoStatusToReadyForExport()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        SetupSelectedVideo(video);
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _viewModel.Finish.Execute(null);
        Assert.That(video.VideoStatus, Is.EqualTo(VideoStatus.ReadyForExport));
    }

    [Test]
    public void FinishCommandCallsVideoProviderManagerNext()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(4);
        _viewModel.Finish.Execute(null);
        _videoProviderManagerMock.Verify(m => m.Next());
    }

    [Test]
    public void FinishCommandNavigatesToNextVideo()
    {
        SetupSelectedVideo(VideoExamples.GetVideoViewModelExample());
        SetupVideos(5);
        _videosExplorerViewModelMock.SetupGet(x => x.SelectedIndex).Returns(1);
        _viewModel.Finish.Execute(null);
        _videosExplorerViewModelMock.VerifySet(m => m.SelectedIndex = 2);
    }

    #endregion
}