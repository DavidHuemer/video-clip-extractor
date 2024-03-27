using Moq;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupResultViewModels;

namespace VideoClipExtractor.Tests.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupResultViewModels;

[TestFixture]
[TestOf(typeof(VideoSetupResultViewModel))]
public class VideoSetupResultViewModelTest : BaseViewModelTest
{
    private Mock<IVideoCrawler> _videoCrawler = null!;
    private VideoSetupResultViewModel _videoSetupResultViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoCrawler = DependencyMock.CreateMockDependency<IVideoCrawler>();
        _videoSetupResultViewModel = new VideoSetupResultViewModel(DependencyMock.Object);
    }

    [Test]
    public void CrawledVideosAreEmptyAtBeginning()
    {
        Assert.That(_videoSetupResultViewModel.CrawledVideos, Is.Empty);
    }

    [Test]
    public void FinishNotAllowedAtBeginning()
    {
        Assert.That(_videoSetupResultViewModel.Finish.CanExecute(null), Is.False);
    }

    [Test]
    public async Task LoadVideosCallsVideoCrawler()
    {
        await _videoSetupResultViewModel.LoadVideos();
        _videoCrawler.Verify(x => x.CrawlVideos(), Times.Once);
    }

    [Test]
    public void VideoCrawlerAddsVideos()
    {
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        _videoCrawler.Raise(x => x.VideosAdded += null!, sourceVideos);

        var crawledSourceVideos = _videoSetupResultViewModel.CrawledVideos.ToList();
        Assert.That(crawledSourceVideos, Is.EquivalentTo(sourceVideos));
    }

    [Test]
    public async Task FinishAllowedAfterVideosAreCrawled()
    {
        await _videoSetupResultViewModel.LoadVideos();
        Assert.That(_videoSetupResultViewModel.Finish.CanExecute(null), Is.True);
    }

    [Test]
    public async Task FinishInvokesVideosAdded()
    {
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        _videoCrawler.Raise(x => x.VideosAdded += null!, sourceVideos);
        await _videoSetupResultViewModel.LoadVideos();
        _videoSetupResultViewModel.VideosAdded += videos => { Assert.That(videos, Is.EquivalentTo(sourceVideos)); };
        _videoSetupResultViewModel.Finish.Execute(null);
    }
}