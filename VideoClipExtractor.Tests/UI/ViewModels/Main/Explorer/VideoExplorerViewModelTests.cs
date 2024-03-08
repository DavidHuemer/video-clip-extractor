using System.Collections.ObjectModel;
using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Managers.Extraction;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.Explorer;

public class VideoExplorerViewModelTests : BaseViewModelTest
{
    private Mock<IExtractionManager> _extractionManagerMock = null!;
    private Mock<IVideoProviderManager> _videoProviderManagerMock = null!;

    private VideosExplorerViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoProviderManagerMock = DependencyMock.CreateMockDependency<IVideoProviderManager>();
        _extractionManagerMock = DependencyMock.CreateMockDependency<IExtractionManager>();

        _viewModel = new VideosExplorerViewModel(DependencyMock.Object);
    }

    [Test]
    public void VideosAreEmptyOnCreation()
    {
        Assert.That(_viewModel.Videos, Is.Empty);
    }

    [Test]
    public void SelectedVideoIsNullOnCreation()
    {
        Assert.That(_viewModel.SelectedVideo, Is.Null);
    }

    [Test]
    public void VideosAreAddedWhenVideoAddedEventIsRaised()
    {
        var video = VideoExamples.GetVideoExample();
        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, new VideoEventArgs(video));
        Assert.That(_viewModel.Videos, Has.Count.EqualTo(1));
    }

    [Test]
    public void SelectedVideoIsSetWhenVideoAddedEventIsRaised()
    {
        var video = VideoExamples.GetVideoExample();
        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, new VideoEventArgs(video));

        Assert.That(_viewModel.SelectedVideo, Is.Not.Null);
    }

    [Test]
    public void ExportVideosCommandCallsExtractionManager()
    {
        var videos = VideoExamples.GetRealisticVideoViewModels();
        _viewModel.Videos = new ObservableCollection<VideoViewModel>(videos);
        _viewModel.ExportVideos.Execute(null);
        _extractionManagerMock.Verify(m => m.ExtractVideos(_viewModel.Videos), Times.Once);
    }
}