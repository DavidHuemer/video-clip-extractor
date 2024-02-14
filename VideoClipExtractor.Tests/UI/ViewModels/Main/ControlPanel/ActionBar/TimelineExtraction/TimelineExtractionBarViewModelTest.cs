using Moq;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

[TestFixture]
[TestOf(typeof(TimelineExtractionBarViewModel))]
public class TimelineExtractionBarViewModelTest : BaseViewModelTest
{
    private Mock<IVideoNavigationViewModel> _videoNavigationViewModelMock = null!;

    private TimelineExtractionBarViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoNavigationViewModelMock = ViewModelProviderMock.CreateViewModelMock<IVideoNavigationViewModel>();
        _viewModel = new TimelineExtractionBarViewModel(DependencyMock.Object);
    }


    [Test]
    public void ImageExtractionIsAdded()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _videoNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));
        _viewModel.AddImageExtraction.Execute(null);
        Assert.AreEqual(1, _viewModel.Video!.ImageExtractions.Count);
        Assert.AreEqual(30, _viewModel.Video.ImageExtractions[0].VideoPosition.Frame);
    }

    [Test]
    public void ImageExtractionIsAddedAtCorrectPosition()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video.ImageExtractions.Add(new ImageExtractionViewModel(new VideoPosition(20)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtractionViewModel(new VideoPosition(29)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtractionViewModel(new VideoPosition(31)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtractionViewModel(new VideoPosition(50)));

        _videoNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));

        _viewModel.AddImageExtraction.Execute(null);
        Assert.AreEqual(5, _viewModel.Video!.ImageExtractions.Count);
        Assert.AreEqual(30, _viewModel.Video.ImageExtractions[2].VideoPosition.Frame);
    }

    [Test]
    public void ImageExtractionCanBeAddedAtLastPosition()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video.ImageExtractions.Add(new ImageExtractionViewModel(new VideoPosition(20)));

        _videoNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));

        _viewModel.AddImageExtraction.Execute(null);
        Assert.AreEqual(2, _viewModel.Video!.ImageExtractions.Count);
        Assert.AreEqual(30, _viewModel.Video.ImageExtractions[1].VideoPosition.Frame);
    }
}