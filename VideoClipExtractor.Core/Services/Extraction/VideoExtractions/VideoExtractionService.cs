using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.BaseExtractions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.VideoExtractions;

[UsedImplicitly]
public class VideoExtractionService(IDependencyProvider provider)
    : BaseExtractionsService<VideoExtraction>(provider), IVideoExtractionService
{
    public new async Task<ExtractionResult> Extract(VideoViewModel video, VideoExtraction videoExtraction) =>
        await base.Extract(video, videoExtraction);

    protected override string GetExtractionPath(VideoViewModel video, VideoExtraction extraction) =>
        ExtractionNameService.GetVideoPath(video, extraction);

    protected override async Task
        RunExtraction(VideoViewModel video, VideoExtraction extraction, string extractionPath) =>
        await MpegEngine.ExtractVideoAsync(video.LocalPath, extractionPath,
            extraction.Begin.Position.Duration.TimeSpan, extraction.Position.Duration.TimeSpan);
}