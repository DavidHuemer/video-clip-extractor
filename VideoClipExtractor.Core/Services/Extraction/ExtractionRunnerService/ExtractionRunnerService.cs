using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.FileExtractionService;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionRunnerService;

[UsedImplicitly]
public class ExtractionRunnerService(IDependencyProvider provider) : IExtractionRunnerService
{
    private readonly IFileExtractionService _fileExtractionService = provider.GetDependency<IFileExtractionService>();

    public async Task<List<ExtractionResult>> ExtractVideo(VideoViewModel video)
    {
        var extractions = video.GetExtractions().ToList();
        var extractionResults = await RunExtractions(video, extractions);
        return extractionResults;
    }

    private async Task<List<ExtractionResult>> RunExtractions(VideoViewModel video,
        IEnumerable<IExtraction> extractions)
    {
        var results = new List<ExtractionResult>();

        foreach (var extraction in extractions)
        {
            var extractionResult = await ExtractExtraction(video, extraction);
            extraction.Result = extractionResult;
            results.Add(extractionResult);
        }

        return results;
    }

    private async Task<ExtractionResult> ExtractExtraction(VideoViewModel video, IExtraction extraction)
    {
        try
        {
            return await _fileExtractionService.Extract(video, extraction);
        }
        catch (Exception e)
        {
            return new ExtractionResult(e);
        }
    }
}