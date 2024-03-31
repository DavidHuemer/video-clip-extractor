using Moq;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.Managers.Timeline.SelectionManager;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

[TestFixture]
[TestOf(typeof(TimelineExtractionBarViewModel))]
public class TimelineExtractionBarViewModelTest : BaseViewModelTest
{
    private Mock<IFrameNavigationViewModel> _frameNavigationViewModelMock = null!;
    private Mock<ITimelineExtractionSelectionManager> _selectionManager = null!;

    private TimelineExtractionBarViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _selectionManager = DependencyMock.CreateMockDependency<ITimelineExtractionSelectionManager>();
        _frameNavigationViewModelMock = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        _viewModel = new TimelineExtractionBarViewModel(DependencyMock.Object);
    }

    [Test]
    public void AddImageExtractionNotAllowedAtBeginning()
    {
        Assert.That(_viewModel.AddImageExtraction.CanExecute(null), Is.False);
    }

    [Test]
    public void AddVideoExtractionNotAllowedAtBeginning()
    {
        Assert.That(_viewModel.AddVideoExtraction.CanExecute(null), Is.False);
    }

    [Test]
    public void ImageExtractionIsAdded()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));
        _viewModel.AddImageExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.ImageExtractions.Count, Is.EqualTo(1));
            Assert.That(_viewModel.Video.ImageExtractions[0].Position.Frame, Is.EqualTo(30));
        });
    }

    [Test]
    public void ImageExtractionIsAddedAtCorrectPosition()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(20)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(29)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(31)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(50)));

        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));

        _viewModel.AddImageExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.ImageExtractions.Count, Is.EqualTo(5));
            Assert.That(_viewModel.Video.ImageExtractions[2].Position.Frame, Is.EqualTo(30));
        });
    }

    [Test]
    public void ImageExtractionCanBeAddedAtLastPosition()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(20)));

        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));

        _viewModel.AddImageExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.ImageExtractions, Has.Count.EqualTo(2));
            Assert.That(_viewModel.Video.ImageExtractions[1].Position.Frame, Is.EqualTo(30));
        });
    }

    [Test]
    public void VideoExtractionIsAdded()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(new VideoPosition(30));
        _viewModel.AddVideoExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.VideoExtractions.Count, Is.EqualTo(1));
            Assert.That(_viewModel.Video.VideoExtractions[0].Begin.Position.Frame, Is.EqualTo(30));
            Assert.That(_viewModel.Video.VideoExtractions[0].End.Position.Frame, Is.EqualTo(60));
        });
    }
}