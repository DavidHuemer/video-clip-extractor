using Moq;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
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
    public void ActiveViewModelIsSetToItselfAtBeginning()
    {
        Assert.That(_viewModel.ActiveViewModel, Is.SameAs(_viewModel));
    }

    [Test]
    public void ExtractCommandNotAllowedAtBeginning()
    {
        Assert.IsFalse(_viewModel.ExtractCommand.CanExecute(null));
    }

    [Test]
    public void ExtractCommandNotAllowedWhenVideosAreEmpty()
    {
        _viewModel.SetupExtraction(new List<VideoViewModel>());
        Assert.IsFalse(_viewModel.ExtractCommand.CanExecute(null));
    }

    [Test]
    public void ExtractCommandAllowedWhenVideosAreAdded()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels();
        _viewModel.SetupExtraction(videos);
        Assert.IsTrue(_viewModel.ExtractCommand.CanExecute(null));
    }

    [Test]
    public void SetupExtractionSetsVideosCorrectly()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels().ToList();
        _viewModel.SetupExtraction(videos);

        // Assert that only videos are set to the Videos Property that are not unset (VideoStatus Property)
        Assert.That(_viewModel.Videos, Is.EquivalentTo(videos.Where(video => video.VideoStatus != VideoStatus.Unset)));
    }

    [Test]
    public void ActiveViewModelIsSetToVisualizationWhenExtracting()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels().ToList();
        _viewModel.SetupExtraction(videos);
        _viewModel.ExtractCommand.Execute(null);

        Assert.That(_viewModel.ActiveViewModel, Is.SameAs(_extractionVisualizationViewModelMock.Object));
    }

    [Test]
    public void ExtractExtractsVideos()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels();
        _viewModel.SetupExtraction(videos);

        _viewModel.ExtractCommand.Execute(null);

        _extractionVisualizationViewModelMock.Verify(
            viewModel => viewModel.ExtractVideos(_viewModel.Videos),
            Times.Once);
    }

    [Test]
    public void ExtractionFinishedTrueWhenExtractionFinished()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels();
        _viewModel.SetupExtraction(videos);

        _viewModel.ExtractCommand.Execute(null);

        Assert.IsTrue(_viewModel.ExtractionFinished);
    }
}