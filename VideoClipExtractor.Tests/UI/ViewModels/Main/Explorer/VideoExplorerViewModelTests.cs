using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.Explorer;

public class VideoExplorerViewModelTests
{
    private DependencyMock _dependencyMock = null!;
    private Mock<IVideoProviderManager> _videoProviderManagerMock = null!;

    [SetUp]
    public void Setup()
    {
        _dependencyMock = new DependencyMock();
        _videoProviderManagerMock = new Mock<IVideoProviderManager>();
    }

    [Test]
    public void VideosAreEmptyOnCreation()
    {
        _dependencyMock.AddMockDependency(_videoProviderManagerMock);
        var viewModel = new VideosExplorerViewModel(_dependencyMock.Object);
        Assert.That(viewModel.Videos, Is.Empty);
    }

    [Test]
    public void SelectedVideoIsNullOnCreation()
    {
        _dependencyMock.AddMockDependency(_videoProviderManagerMock);
        var viewModel = new VideosExplorerViewModel(_dependencyMock.Object);
        Assert.That(viewModel.SelectedVideo, Is.Null);
    }

    [Test]
    public void VideosAreAddedWhenVideoAddedEventIsRaised()
    {
        _dependencyMock.AddMockDependency(_videoProviderManagerMock);
        var viewModel = new VideosExplorerViewModel(_dependencyMock.Object);
        var video = VideoExamples.GetVideoExample();
        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, new VideoEventArgs(video));
        Assert.That(viewModel.Videos, Has.Member(video));
    }

    [Test]
    public void SelectedVideoIsSetWhenVideoAddedEventIsRaised()
    {
        _dependencyMock.AddMockDependency(_videoProviderManagerMock);
        var viewModel = new VideosExplorerViewModel(_dependencyMock.Object);
        var video = VideoExamples.GetVideoExample();
        _videoProviderManagerMock.Raise(m => m.VideoAdded += null!, new VideoEventArgs(video));

        Assert.That(viewModel.SelectedVideo, Is.Not.Null);
    }
}