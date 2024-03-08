using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.Extraction.Cleanup;
using VideoClipExtractor.Core.Services.Extraction.ExtractionRunnerService;
using VideoClipExtractor.Core.Services.Extraction.VideoValidationService;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction;

[Singleton]
public class ExtractionService(IDependencyProvider provider) : IExtractionService
{
    private readonly IExtractionRunnerService _extractionRunnerService =
        provider.GetDependency<IExtractionRunnerService>();

    private readonly IVideoCleanupService _videoCleanupService =
        provider.GetDependency<IVideoCleanupService>();

    private readonly IVideoValidationService _videoValidationService =
        provider.GetDependency<IVideoValidationService>();

    public async Task<VideoExtractionResult> Extract(VideoViewModel video)
    {
        try
        {
            _videoValidationService.ValidateVideoForExtraction(video);
            return await RunExtraction(video);
        }
        catch (Exception e)
        {
            return new VideoExtractionResult(e);
        }
    }

    private async Task<VideoExtractionResult> RunExtraction(VideoViewModel video)
    {
        var extractionResults = await _extractionRunnerService
            .ExtractVideo(video);

        return CleanupVideo(video, extractionResults);
    }

    private VideoExtractionResult CleanupVideo(VideoViewModel video, List<ExtractionResult> extractionResults)
    {
        try
        {
            return _videoCleanupService.CleanupVideo(video, extractionResults);
        }
        catch (Exception e)
        {
            return new VideoExtractionResult(e, extractionResults);
        }
    }
}