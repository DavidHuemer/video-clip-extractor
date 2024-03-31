using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.MpegExtraction;
using VideoClipExtractor.Core.Services.Extraction.ExtractionNames;
using VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.BaseExtractions;

/// <summary>
/// Base class for extraction services.
/// </summary>
/// <typeparam name="T">The type of extraction that should be extracted</typeparam>
public abstract class BaseExtractionsService<T>(IDependencyProvider provider)
    where T : IExtraction
{
    private readonly IExtractionVerificationService _extractionVerificationService =
        provider.GetDependency<IExtractionVerificationService>();

    protected readonly IExtractionNameService ExtractionNameService =
        provider.GetDependency<IExtractionNameService>();

    protected readonly IMpegExtractionRunner MpegExtractionRunner = provider.GetDependency<IMpegExtractionRunner>();

    /// <summary>
    /// Extracts the given extraction from the given video.
    /// </summary>
    /// <param name="video">The video from which the extraction should be extracted</param>
    /// <param name="extraction">The extraction that should be extracted</param>
    /// <returns></returns>
    protected async Task<ExtractionResult> Extract(VideoViewModel video, T extraction)
    {
        try
        {
            var extractionPath = GetExtractionPath(video, extraction);
            return await ExtractToFile(video, extraction, extractionPath);
        }
        catch (Exception e)
        {
            return new ExtractionResult(e);
        }
    }

    private async Task<ExtractionResult> ExtractToFile(VideoViewModel video, T extraction, string extractionPath)
    {
        try
        {
            await RunExtraction(video, extraction, extractionPath);
            return _extractionVerificationService.CheckExtraction(extractionPath);
        }
        catch (Exception e)
        {
            return new ExtractionResult(e, extractionPath);
        }
    }

    /// <summary>
    /// Returns the path where the extraction should be saved.
    /// </summary>
    /// <param name="video">The video from which the extraction should be extracted</param>
    /// <param name="extraction">The extraction that should be extracted</param>
    /// <returns>The path where the extraction should be saved</returns>
    protected abstract string GetExtractionPath(VideoViewModel video, T extraction);

    /// <summary>
    /// Extracts the extraction to the given path.
    /// </summary>
    /// <param name="video">The video from which the extraction should be extracted</param>
    /// <param name="extraction">The extraction that should be extracted</param>
    /// <param name="extractionPath">The path where the extraction should be saved</param>
    protected abstract Task RunExtraction(VideoViewModel video, T extraction, string extractionPath);
}