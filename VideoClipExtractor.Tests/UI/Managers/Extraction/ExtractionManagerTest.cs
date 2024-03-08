using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Managers.Extraction;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.ExtractionWindow;

namespace VideoClipExtractor.Tests.UI.Managers.Extraction;

[TestFixture]
[TestOf(typeof(ExtractionManager))]
public class ExtractionManagerTest : BaseViewModelTest
{
    private Mock<IExtractionWindowViewModel> _extractionWindowViewModelMock = null!;

    private ExtractionManager _extractionManager = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionWindowViewModelMock = ViewModelProviderMock.CreateViewModelMock<IExtractionWindowViewModel>();
        _extractionManager = new ExtractionManager(DependencyMock.Object);
    }

    [Test]
    public void SetupExtractionIsCalled()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels().ToList();
        _extractionManager.ExtractVideos(videos);
        _extractionWindowViewModelMock.Verify(x => x.SetupExtraction(videos), Times.Once);
    }

    [Test]
    public void ShowDialogIsCalled()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels().ToList();
        _extractionManager.ExtractVideos(videos);
        _extractionWindowViewModelMock.Verify(x => x.ShowDialog(), Times.Once);
    }
}