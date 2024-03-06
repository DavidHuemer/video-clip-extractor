namespace VideoClipExtractor.Data.Extractions.Results;

/// <summary>
/// The result of a video that has been extracted
/// </summary>
public class VideoExtractionResult
{
    public VideoExtractionResult(List<ExtractionResult> extractionResults)
    {
        var failedExtractions = extractionResults.Where(e => !e.Success).ToList();
        Success = failedExtractions.Count == 0;
        ExtractionResults = extractionResults;

        if (failedExtractions.Count == 0)
        {
            Message = string.Empty;
        }
        else
        {
            Message = failedExtractions.Count == 1
                ? failedExtractions.First().Message
                : "Multiple extractions failed.";
        }
    }

    public VideoExtractionResult(Exception exception, IEnumerable<ExtractionResult> extractionResults)
    {
        Success = false;
        ExtractionResults = extractionResults;
        Message = exception.Message;
    }

    public VideoExtractionResult(Exception exception)
    {
        Message = exception.Message;
        ExtractionResults = [];
        Success = false;
    }

    /// <summary>
    /// Whether the extraction of the video was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The message of the result
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// The results of the extractions of the video
    /// </summary>
    public IEnumerable<ExtractionResult> ExtractionResults { get; }
}