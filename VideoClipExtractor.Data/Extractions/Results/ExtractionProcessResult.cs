namespace VideoClipExtractor.Data.Extractions.Results;

/// <summary>
/// The result of videos that have been extracted
/// </summary>
public class ExtractionProcessResult
{
    public ExtractionProcessResult(IEnumerable<VideoExtractionResult> videoExtractionResults)
    {
        var failedVideoExtractions = videoExtractionResults
            .Where(x => !x.Success).ToList();

        Success = failedVideoExtractions.Count == 0;

        if (failedVideoExtractions.Count == 0)
        {
            Message = string.Empty;
            return;
        }

        Message = failedVideoExtractions.Count == 1
            ? failedVideoExtractions.First().Message
            : "Multiple videos failed to extract.";
    }

    public ExtractionProcessResult(Exception e)
    {
        Success = false;
        Message = e.Message;
    }

    /// <summary>
    /// If the extraction process was successful
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// The message of the extraction process
    /// </summary>
    public string Message { get; }
}