using Moq;
using VideoClipExtractor.Core.Services.Extraction;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

namespace VideoClipExtractor.Tests.UI.ViewModels.Extraction.ExtractionRunner;

[TestFixture]
[TestOf(typeof(ExtractionRunnerViewModel))]
public class ExtractionRunnerViewModelTest : BaseViewModelTest
{
    private Mock<IExtractionService> _extractionServiceMock = null!;
    private Mock<IExtractionNavigationViewModel> _extractionNavigationViewModelMock = null!;
    private ExtractionRunnerViewModel _extractionRunnerViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionServiceMock = DependencyMock.CreateMockDependency<IExtractionService>();
        _extractionNavigationViewModelMock =
            ViewModelProviderMock.CreateViewModelMock<IExtractionNavigationViewModel>();

        _extractionRunnerViewModel = new ExtractionRunnerViewModel(DependencyMock.Object);
    }

    [Test]
    public async Task CurrentVideoIsSetForAllVideos()
    {
        _extractionServiceMock.Setup(x => x.Extract(It.IsAny<VideoViewModel>()))
            .ReturnsAsync(ExtractionResultExamples.GetSuccessVideoExtractionResultExample());

        var videos = VideoExamples.GetVideoViewModelExamples(5).ToList();
        await _extractionRunnerViewModel.ExtractVideos(videos);

        foreach (var video in videos)
        {
            _extractionNavigationViewModelMock.VerifySet(
                x => x.CurrentVideo = video, Times.Once);
        }
    }

    [Test]
    public async Task ExtractIsCalledForAllVideos()
    {
        var videos = VideoExamples.GetVideoViewModelExamples(5).ToList();
        await _extractionRunnerViewModel.ExtractVideos(videos);

        foreach (var video in videos)
        {
            _extractionServiceMock.Verify(x => x.Extract(video), Times.Once);
        }
    }

    [Test]
    public async Task ExtractIsCalledForAllVideosWhenExceptionIsThrown()
    {
        var videos = VideoExamples.GetVideoViewModelExamples(5).ToList();
        _extractionServiceMock.Setup(x => x.Extract(It.IsAny<VideoViewModel>()))
            .Throws<Exception>();

        await _extractionRunnerViewModel.ExtractVideos(videos);

        foreach (var video in videos)
        {
            _extractionServiceMock.Verify(x => x.Extract(video), Times.Once);
        }
    }
}