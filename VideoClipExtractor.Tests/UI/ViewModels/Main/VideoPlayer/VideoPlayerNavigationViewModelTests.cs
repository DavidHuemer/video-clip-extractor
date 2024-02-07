using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
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

    [Test]
    [TestCase(0, false, false)]
    [TestCase(0, true, false)]
    [TestCase(1, true, true)]
    [TestCase(1, true, true)]
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

        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(video);
        _viewModel.Skip.Execute(null);

        Assert.That(video.VideoStatus, Is.EqualTo(VideoStatus.Skipped));
    }

    [Test]
    public void SkipCommandCallsVideoProviderManagerNext()
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(VideoExamples.GetVideoViewModelExample());
        _viewModel.Skip.Execute(null);

        _videoProviderManagerMock.Verify(m => m.Next());
    }

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

        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(video);
        _viewModel.Finish.Execute(null);

        Assert.That(video.VideoStatus, Is.EqualTo(VideoStatus.ReadyForExport));
    }

    [Test]
    public void FinishCommandCallsVideoProviderManagerNext()
    {
        _videosExplorerViewModelMock.SetupGet(m => m.SelectedVideo)
            .Returns(VideoExamples.GetVideoViewModelExample());
        _viewModel.Finish.Execute(null);

        _videoProviderManagerMock.Verify(m => m.Next());
    }
}