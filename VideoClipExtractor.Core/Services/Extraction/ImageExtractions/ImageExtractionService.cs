using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.BaseExtractions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ImageExtractions;

[UsedImplicitly]
public class ImageExtractionService(IDependencyProvider provider)
    : BaseExtractionsService<ImageExtraction>(provider), IImageExtractionService
{
    public new async Task<ExtractionResult> Extract(VideoViewModel video, ImageExtraction imageExtraction) =>
        await base.Extract(video, imageExtraction);

    protected override string GetExtractionPath(VideoViewModel video, ImageExtraction extraction) =>
        ExtractionNameService.GetImagePath(video, extraction);

    protected override async Task
        RunExtraction(VideoViewModel video, ImageExtraction extraction, string extractionPath) =>
        await MpegExtractionRunner.ExtractImageAsync(video.LocalPath, extractionPath,
            extraction.Position.Duration.TimeSpan);
}