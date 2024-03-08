using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionResult;

namespace VideoClipExtractor.Tests.UI.ViewModels.Extraction.ExtractionResult;

[TestFixture]
[TestOf(typeof(ExtractionResultViewModel))]
public class ExtractionResultViewModelTest : BaseViewModelTest
{
    private Mock<IExtractionNavigationViewModel> _extractionNavigationViewModelMock = null!;

    private ExtractionResultViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _extractionNavigationViewModelMock =
            ViewModelProviderMock.CreateViewModelMock<IExtractionNavigationViewModel>();
        _viewModel = new ExtractionResultViewModel(DependencyMock.Object);
    }


    [Test]
    public void ResultSetsTheViewModelProperties()
    {
        var extractionProcessResult = ExtractionResultExamples.GetSuccessExtractionProcessResultExample();
        _viewModel.Result = extractionProcessResult;

        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Success, Is.EqualTo(extractionProcessResult.Success));
            Assert.That(_viewModel.ShowMessage, Is.False);
            Assert.That(_viewModel.Message, Is.Empty);
            Assert.That(_viewModel.SuccessfulExtractions, Is.EqualTo("5/5"));
            Assert.That(_viewModel.StoredBytes, Is.EqualTo(200));
            Assert.That(_viewModel.SavedBytes, Is.EqualTo(150 * 5));
            Assert.That(_viewModel.ByteDifference, Is.EqualTo(200 - 150 * 5));
        });
    }

    [Test]
    public void GoBackCanExecute()
    {
        Assert.That(_viewModel.GoBack.CanExecute(null), Is.True);
    }

    [Test]
    public void GoBackSetsCurrentVideoToNull()
    {
        _viewModel.GoBack.Execute(null);
        _extractionNavigationViewModelMock.VerifySet(x => x.CurrentVideo = null, Times.Once);
    }
}