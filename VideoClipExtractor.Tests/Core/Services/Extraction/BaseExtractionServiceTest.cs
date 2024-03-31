using Moq;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.Extraction;

public abstract class BaseExtractionServiceTest : BaseDependencyTest
{
    protected Mock<IExtractionNameService> ExtractionNameService = null!;

    protected string ExtractionPath = "";
    protected Mock<IExtractionVerificationService> ExtractionVerificationService = null!;

    protected VideoViewModel VideoViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        ExtractionNameService = DependencyMock.CreateMockDependency<IExtractionNameService>();
        ExtractionVerificationService = DependencyMock.CreateMockDependency<IExtractionVerificationService>();

        VideoViewModel = VideoExamples.GetVideoViewModelExample();
    }

    protected void SetupVerificationService() =>
        ExtractionVerificationService
            .Setup(x => x.CheckExtraction(ExtractionPath))
            .Returns(ExtractionResultExamples.GetSuccessResultExample);
}