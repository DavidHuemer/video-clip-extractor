﻿using FFMpeg.Wrapper.Engine;
using Moq;
using VideoClipExtractor.Core.Services.Extraction.VideoExtractions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.VideoExtractions;

[TestFixture]
[TestOf(typeof(VideoExtractionService))]
public class VideoExtractionServiceTest : BaseExtractionServiceTest
{
    private Mock<IMpegEngine> _mpegEngine = null!;
    private VideoExtraction _videoExtraction = null!;
    private VideoExtractionService _videoExtractionService = null!;

    public override void Setup()
    {
        base.Setup();
        _mpegEngine = DependencyMock.CreateMockDependency<IMpegEngine>();

        _videoExtraction = ExtractionExamples.GetVideoExtractionExample();
        _videoExtractionService = new VideoExtractionService(DependencyMock.Object);
        ExtractionPath = @"C\Output\Path\video.mp4";
    }

    [Test]
    public async Task VideoIsExtracted()
    {
        SetupVideoPath();
        SetupVerificationService();

        await _videoExtractionService.Extract(VideoViewModel, _videoExtraction);

        _mpegEngine.Verify(
            x => x.ExtractVideoAsync(VideoViewModel.LocalPath, ExtractionPath,
                _videoExtraction.Begin.Position.Duration.TimeSpan, _videoExtraction.Position.Duration.TimeSpan),
            Times.Once);
    }

    [Test]
    public async Task VerificationServiceIsCalled()
    {
        SetupVideoPath();
        SetupVerificationService();

        await _videoExtractionService.Extract(VideoViewModel, _videoExtraction);
        ExtractionVerificationService.Verify(x => x.CheckExtraction(ExtractionPath), Times.Once);
    }

    private void SetupVideoPath() =>
        ExtractionNameService
            .Setup(x => x.GetVideoPath(VideoViewModel, _videoExtraction))
            .Returns(ExtractionPath);
}