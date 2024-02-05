using Moq;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Mocks;
using static NUnit.Framework.Assert;

namespace VideoClipExtractor.Tests.Core.Managers.VideoProviderManagerTests;

public class VideoProviderManagerTests
{
    private DependencyMock _dependencyMock = null!;

    [SetUp]
    public void Setup()
    {
        _dependencyMock = new DependencyMock();
    }

    [Test]
    public void SetupVideoProvider()
    {
        // Arrange
        var videoProvider = new Mock<IVideoProvider>();
        _dependencyMock.AddMockDependency(videoProvider);
        var videoProviderManager = new VideoProviderManager(_dependencyMock.Object);
        var project = ProjectExamples.GetExampleProject();
        var repository = new Mock<IVideoRepository>();
        videoProviderManager.Setup(project, repository.Object);

        videoProvider.Verify(x => x.Setup(project, repository.Object), Times.Once);
    }

    [Test]
    public void VideoAddedInvokedWhenProviderRaisesEvent()
    {
        // Arrange
        var videoProvider = new Mock<IVideoProvider>();
        _dependencyMock.AddMockDependency(videoProvider);
        var videoProviderManager = new VideoProviderManager(_dependencyMock.Object);
        var project = ProjectExamples.GetExampleProject();
        var repository = new Mock<IVideoRepository>();
        videoProviderManager.Setup(project, repository.Object);


        var eventRaised = false;
        videoProviderManager.VideoAdded += (_, _) => eventRaised = true;

        // Act
        videoProvider.Raise(x => x.VideoAdded += null,
            new VideoEventArgs(VideoExamples.GetVideoExample()));
        That(eventRaised, Is.True);
    }
}