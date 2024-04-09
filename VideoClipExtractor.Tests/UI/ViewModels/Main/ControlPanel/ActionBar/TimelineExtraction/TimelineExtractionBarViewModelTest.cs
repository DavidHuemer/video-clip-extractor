using Moq;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;
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
    private ExtractionFactoryMock _extractionFactory = null!;

    private TimelineExtractionBarViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _selectionManager = DependencyMock.CreateMockDependency<ITimelineExtractionSelectionManager>();
        _frameNavigationViewModelMock = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        _extractionFactory = new ExtractionFactoryMock();
        DependencyMock.AddMockDependency(_extractionFactory);
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
        var videoPosition = new VideoPosition(100, 50);
        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(videoPosition);
        var imageExtraction = _extractionFactory.SetupAddImageExtraction();
        _viewModel.AddImageExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.ImageExtractions.Count, Is.EqualTo(1));
            Assert.That(_viewModel.Video.ImageExtractions[0], Is.EqualTo(imageExtraction.Object));
        });
    }

    [Test]
    public void ImageExtractionIsAddedAtCorrectPosition()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(20, 50)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(29, 50)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(31, 50)));
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(50, 50)));

        var addedPosition = new VideoPosition(30, 50);

        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(addedPosition);

        var imageExtraction = _extractionFactory.SetupAddImageExtraction();
        imageExtraction.SetupGet(x => x.Position).Returns(addedPosition);

        _viewModel.AddImageExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.ImageExtractions.Count, Is.EqualTo(5));
            Assert.That(_viewModel.Video.ImageExtractions[2], Is.EqualTo(imageExtraction.Object));
        });
    }

    [Test]
    public void ImageExtractionCanBeAddedAtLastPosition()
    {
        _viewModel.Video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video.ImageExtractions.Add(new ImageExtraction(new VideoPosition(20, 50)));

        var position = new VideoPosition(30, 50);

        _frameNavigationViewModelMock.SetupGet(x => x.VideoPosition)
            .Returns(position);

        var videoExtraction = _extractionFactory.SetupAddImageExtraction();
        videoExtraction.SetupGet(x => x.Position).Returns(position);

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
            .Returns(new VideoPosition(30, 50));

        var videoExtraction = _extractionFactory.SetupAddVideoExtraction();

        _viewModel.AddVideoExtraction.Execute(null);
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.Video!.VideoExtractions.Count, Is.EqualTo(1));
            Assert.That(_viewModel.Video.VideoExtractions[0], Is.EqualTo(videoExtraction.Object));
        });
    }
}