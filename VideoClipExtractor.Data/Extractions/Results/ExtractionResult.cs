namespace VideoClipExtractor.Data.Extractions.Results;

/// <summary>
/// The extraction result of a single extraction (image or video)
/// </summary>
public class ExtractionResult
{
    public ExtractionResult(Exception exception, string path = "")
    {
        Path = path;
        Success = false;
        Message = exception.Message;
    }

    public ExtractionResult(string path, long bytes)
    {
        Path = path;
        Success = true;
        Bytes = bytes;
    }

    public ExtractionResult(string path, string message, bool success)
    {
        Path = path;
        Success = success;
        Message = message;
    }

    /// <summary>
    /// The path to the extracted file
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// The name of the extracted file
    /// </summary>
    public string Name => Path.Split('\\').Last();

    /// <summary>
    /// If the extraction was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The message of the extraction
    /// </summary>
    public string Message { get; } = string.Empty;

    /// <summary>
    /// How many bytes the extracted file has
    /// </summary>
    public long Bytes { get; }
}