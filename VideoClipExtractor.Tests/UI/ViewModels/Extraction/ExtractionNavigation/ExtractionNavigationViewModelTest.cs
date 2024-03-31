using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Extraction.ExtractionNavigation;

[TestFixture]
[TestOf(typeof(ExtractionNavigationViewModel))]
public class ExtractionNavigationViewModelTest
{
    [SetUp]
    public void Setup()
    {
        _viewModel = new ExtractionNavigationViewModel();
    }

    private ExtractionNavigationViewModel _viewModel = null!;

    [Test]
    public void CurrentVideoIsNullByDefault()
    {
        Assert.That(_viewModel.CurrentVideo, Is.Null);
    }

    [Test]
    public void ExtractionsIsEmptyByDefault()
    {
        Assert.That(_viewModel.Extractions, Is.Empty);
    }

    [Test]
    public void ShowDetailsIsFalseByDefault()
    {
        Assert.That(_viewModel.ShowDetails, Is.False);
    }

    [Test]
    public void CurrentVideoInvokesOnPropertyChanged()
    {
        var invoked = false;
        _viewModel.PropertyChanged += (_, _) => invoked = true;
        _viewModel.CurrentVideo = VideoExamples.GetVideoViewModelExample();
        Assert.That(invoked, Is.True);
    }

    [Test]
    public void CurrentVideoSetsExtractions()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample());
        video.ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample());

        _viewModel.CurrentVideo = video;
        Assert.That(_viewModel.Extractions, Has.Count.EqualTo(2));
    }

    [Test]
    public void CurrentVideoSetsShowDetails()
    {
        _viewModel.CurrentVideo = VideoExamples.GetVideoViewModelExample();
        Assert.That(_viewModel.ShowDetails, Is.True);
    }
}