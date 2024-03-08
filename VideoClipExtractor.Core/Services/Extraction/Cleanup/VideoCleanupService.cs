using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.Extraction.Cleanup.CacheCleanup;
using VideoClipExtractor.Core.Services.Extraction.Cleanup.VideoRepoCleanup;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.Cleanup;

[Singleton]
public class VideoCleanupService(IDependencyProvider provider) : IVideoCleanupService
{
    private readonly ICacheCleanupService _cacheCleanupService =
        provider.GetDependency<ICacheCleanupService>();

    private readonly IExtractionVerificationService _extractionValidationService =
        provider.GetDependency<IExtractionVerificationService>();

    private readonly IVideoRepoCleanupService _videoRepoCleanupService =
        provider.GetDependency<IVideoRepoCleanupService>();


    public VideoExtractionResult CleanupVideo(VideoViewModel video, List<ExtractionResult> extractionResults)
    {
        try
        {
            _extractionValidationService.ValidateExtractionResults(extractionResults);
            _cacheCleanupService.CleanUpCachedVideo(video);

            if (video.VideoStatus != VideoStatus.ReadyForExport)
                return new VideoExtractionResult(extractionResults);

            _videoRepoCleanupService.CleanupVideo(video);

            return new VideoExtractionResult(extractionResults, video.Bytes);
        }
        catch (Exception e)
        {
            return new VideoExtractionResult(e, extractionResults);
        }
    }
}