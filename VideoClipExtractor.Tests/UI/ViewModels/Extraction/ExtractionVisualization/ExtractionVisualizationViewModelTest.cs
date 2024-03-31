using Moq;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionResult;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

namespace VideoClipExtractor.Tests.UI.ViewModels.Extraction.ExtractionVisualization;

[TestFixture]
[TestOf(typeof(ExtractionVisualizationViewModel))]
public class ExtractionVisualizationViewModelTest : BaseViewModelTest
{
    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _extractionRunnerMock = ViewModelProviderMock.CreateViewModelMock<IExtractionRunnerViewModel>();
        _extractionResultMock = ViewModelProviderMock.CreateViewModelMock<IExtractionResultViewModel>();
        _viewModel = new ExtractionVisualizationViewModel(DependencyMock.Object);
    }

    private Mock<IExtractionRunnerViewModel> _extractionRunnerMock = null!;
    private Mock<IExtractionResultViewModel> _extractionResultMock = null!;

    private ExtractionVisualizationViewModel _viewModel = null!;

    [Test]
    public void ActiveViewModelIsVisualizationAtBeginning()
    {
        Assert.That(_viewModel.ActiveViewModel, Is.EqualTo(_viewModel));
    }

    [Test]
    public void ExtractionFinishedFalseAtBeginning()
    {
        Assert.IsFalse(_viewModel.ExtractionFinished);
    }

    [Test]
    public async Task ExtractVideosCallsExtractionRunner()
    {
        var videos = VideoExamples.GetVideoViewModelExamples(4).ToList();
        await _viewModel.ExtractVideos(videos);
        _extractionRunnerMock.Verify(x => x.ExtractVideos(videos), Times.Once);
    }

    [Test]
    public void ActiveViewModelIsSetToExtractionRunner()
    {
        var videos = VideoExamples.GetVideoViewModelExamples(4).ToList();
        var t = new TaskCompletionSource<ExtractionProcessResult>();
        _extractionRunnerMock.Setup(x => x.ExtractVideos(videos)).Returns(t.Task);
        Task.Run(() => _viewModel.ExtractVideos(videos));
        Task.Delay(50).Wait();
        Assert.That(_viewModel.ActiveViewModel, Is.EqualTo(_extractionRunnerMock.Object));
        t.SetCanceled();
    }

    [Test]
    public async Task ExtractionResultIsSet()
    {
        var videos = VideoExamples.GetVideoViewModelExamples(4).ToList();
        _extractionRunnerMock.Setup(x => x.ExtractVideos(videos))
            .ReturnsAsync(ExtractionResultExamples.GetSuccessExtractionProcessResultExample());

        await _viewModel.ExtractVideos(videos);

        _extractionResultMock.VerifySet(x => x.Result = It.IsAny<ExtractionProcessResult>(), Times.Once);
    }

    [Test]
    public async Task ActiveViewModelIsSetToExtractionResultViewModel()
    {
        var videos = VideoExamples.GetVideoViewModelExamples(4).ToList();
        _extractionRunnerMock.Setup(x => x.ExtractVideos(videos))
            .ReturnsAsync(ExtractionResultExamples.GetSuccessExtractionProcessResultExample());

        await _viewModel.ExtractVideos(videos);
        Assert.That(_viewModel.ActiveViewModel, Is.EqualTo(_extractionResultMock.Object));
    }
}