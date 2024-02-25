using Moq;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.Extraction;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

namespace VideoClipExtractor.Tests.UI.ViewModels.Extraction;

[TestFixture]
[TestOf(typeof(ExtractionPanelViewModel))]
public class ExtractionPanelViewModelTest : BaseViewModelTest
{
    private Mock<IExtractionVisualizationViewModel> _extractionVisualizationViewModelMock = null!;
    private ExtractionPanelViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionVisualizationViewModelMock =
            ViewModelProviderMock.CreateViewModelMock<IExtractionVisualizationViewModel>();
        _viewModel = new ExtractionPanelViewModel(DependencyMock.Object);
    }

    [Test]
    public void ExtractCommandNotAllowedAtBeginning()
    {
        Assert.IsFalse(_viewModel.ExtractCommand.CanExecute(null));
    }

    [Test]
    public void ShowVisualizationIsFalseAtBeginning()
    {
        Assert.IsFalse(_viewModel.ShowVisualization);
    }

    [Test]
    public void ExtractCommandAllowedWhenVideosAreAdded()
    {
        var videos = GetVideos();
        _viewModel.SetupExtraction(videos);
        Assert.IsTrue(_viewModel.ExtractCommand.CanExecute(null));
    }

    [Test]
    public void ShowVisualizationIsFalseWhenVideosAreAdded()
    {
        var videos = GetVideos();
        _viewModel.SetupExtraction(videos);
        Assert.IsFalse(_viewModel.ShowVisualization);
    }

    [Test]
    public void ShowVisualizationIsTrueWhenExtractionStarted()
    {
        var videos = GetVideos();
        _viewModel.SetupExtraction(videos);
        _viewModel.ExtractCommand.Execute(null);
        Assert.IsTrue(_viewModel.ShowVisualization);
    }

    [Test]
    public void ExtractExtractsVideos()
    {
        var videos = GetVideos();
        _viewModel.SetupExtraction(videos);

        _viewModel.ExtractCommand.Execute(null);

        _extractionVisualizationViewModelMock.Verify(
            viewModel => viewModel.ExtractVideos(_viewModel.Videos),
            Times.Once);
    }

    [Test]
    public void ExtractionFinishedTrueWhenExtractionFinished()
    {
        var videos = GetVideos();
        _viewModel.SetupExtraction(videos);

        _viewModel.ExtractCommand.Execute(null);

        Assert.IsTrue(_viewModel.ExtractionFinished);
    }

    private static List<VideoViewModel> GetVideos()
    {
        var videos = new List<VideoViewModel>
        {
            VideoExamples.GetVideoViewModelExample(),
            VideoExamples.GetVideoViewModelExample(),
            VideoExamples.GetVideoViewModelExample(),
        };

        videos[0].VideoStatus = VideoStatus.ReadyForExport;
        videos[1].VideoStatus = VideoStatus.ReadyForExport;
        return videos;
    }
}