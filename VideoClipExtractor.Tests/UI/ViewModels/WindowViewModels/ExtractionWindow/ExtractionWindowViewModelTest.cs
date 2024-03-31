using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Extraction;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.ExtractionWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels.ExtractionWindow;

[TestFixture]
[TestOf(typeof(ExtractionWindowViewModel))]
public class ExtractionWindowViewModelTest : BaseViewModelTest
{
    private Mock<IExtractionPanelViewModel> _extractionPanelViewModelMock = null!;

    private ExtractionWindowViewModel _extractionWindowViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionPanelViewModelMock = ViewModelProviderMock.CreateViewModelMock<IExtractionPanelViewModel>();
        _extractionWindowViewModel = new ExtractionWindowViewModel(DependencyMock.Object);
    }


    [Test]
    public void SetupExtractionIsCalledOnExtractionPanel()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels().ToList();
        _extractionWindowViewModel.SetupExtraction(videos);
        _extractionPanelViewModelMock.Verify(x => x.SetupExtraction(videos), Times.Once);
    }
}