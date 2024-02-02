using Moq;
using VideoClipExtractor.Data.VideoRepos;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public static class RepositoryMocks
{
    public static Mock<IVideoRepository> GetVideoRepositoryMock()
    {
        // Return a mock of IVideoRepository using Moq
        return new Mock<IVideoRepository>();
    }
}