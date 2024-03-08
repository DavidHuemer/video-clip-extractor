namespace VideoClipExtractor.Data.Extractions.Results;

/// <summary>
/// The result of videos that have been extracted
/// </summary>
public class ExtractionProcessResult
{
    public ExtractionProcessResult(List<VideoExtractionResult> videoExtractionResults)
    {
        VideoExtractionResults = videoExtractionResults;

        var failedVideoExtractions = VideoExtractionResults
            .Where(x => !x.Success).ToList();

        Success = failedVideoExtractions.Count == 0;
        SavedBytes = VideoExtractionResults.Sum(x => x.SavedBytes);

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
        VideoExtractionResults = [];
    }

    public IEnumerable<VideoExtractionResult> VideoExtractionResults { get; }

    public IEnumerable<VideoExtractionResult> SuccessfulVideoExtractions => VideoExtractionResults
        .Where(x => x.Success);

    public IEnumerable<VideoExtractionResult> FailedVideoExtractions => VideoExtractionResults
        .Where(x => !x.Success);

    /// <summary>
    /// If the extraction process was successful
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// The message of the extraction process
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// How many bytes were saved
    /// </summary>
    public long SavedBytes { get; set; }

    /// <summary>
    /// The amount of bytes that were stored by the extractions
    /// </summary>
    public long StoredBytes => VideoExtractionResults.Sum(x => x.CreatedBytes);
}