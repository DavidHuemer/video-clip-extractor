using Moq;
using VideoClipExtractor.Core.Services.Extraction.ExtractionFactory;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class ExtractionFactoryMock : Mock<IExtractionFactory>
{
    public Mock<IImageExtraction> SetupAddImageExtraction()
    {
        var imageExtractionMock = new Mock<IImageExtraction>();
        Setup(x => x.GetImageExtraction(It.IsAny<VideoPosition>()))
            .Returns(imageExtractionMock.Object);

        return imageExtractionMock;
    }

    public Mock<IVideoExtraction> SetupAddVideoExtraction()
    {
        var videoExtractionMock = new Mock<IVideoExtraction>();
        Setup(x => x.GetVideoExtraction(It.IsAny<VideoPosition>(), It.IsAny<VideoViewModel>()))
            .Returns(videoExtractionMock.Object);

        return videoExtractionMock;
    }
}