using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.Engine;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ImageExtractions;

[UsedImplicitly]
public class ImageExtractionService(IDependencyProvider provider) : IImageExtractionService
{
    private readonly IExtractionNameService _extractionNameService = provider.GetDependency<IExtractionNameService>();

    private readonly IExtractionVerificationService _extractionVerificationService =
        provider.GetDependency<IExtractionVerificationService>();

    private readonly IMpegEngine _mpegEngine = provider.GetDependency<IMpegEngine>();

    public async Task Extract(VideoViewModel video, ImageExtraction imageExtraction)
    {
        var extractionPath = _extractionNameService.GetImagePath(video, imageExtraction);

        // Extract image from video
        await _mpegEngine.ExtractImageAsync(video.LocalPath, extractionPath,
            imageExtraction.Position.Duration.TimeSpan);

        // Verify that the extraction was successful
        if (!_extractionVerificationService.ExtractionSucceeded(extractionPath))
            throw new ExtractionFailedException(extractionPath);
    }
}