using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.Engine;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.VideoExtractions;

[UsedImplicitly]
public class VideoExtractionService(IDependencyProvider provider) : IVideoExtractionService
{
    private readonly IExtractionNameService _extractionNameService = provider.GetDependency<IExtractionNameService>();

    private readonly IExtractionVerificationService _extractionVerificationService =
        provider.GetDependency<IExtractionVerificationService>();

    private readonly IMpegEngine _mpegEngine = provider.GetDependency<IMpegEngine>();

    public async Task Extract(VideoViewModel video, VideoExtraction videoExtraction)
    {
        var extractionPath = _extractionNameService.GetVideoPath(video, videoExtraction);

        await _mpegEngine.ExtractVideoAsync(video.LocalPath, extractionPath,
            videoExtraction.Begin.Position.Duration.TimeSpan, videoExtraction.Position.Duration.TimeSpan);

        // Verify that the extraction was successful
        if (!_extractionVerificationService.ExtractionSucceeded(extractionPath))
            throw new ExtractionFailedException(extractionPath);
    }
}