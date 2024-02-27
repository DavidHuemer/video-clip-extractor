namespace VideoClipExtractor.Data.Extractions.Results;

public class ExtractionResult
{
    public ExtractionResult(Exception exception, string name = "")
    {
        Name = name;
        Success = false;
        Message = exception.Message;
    }

    public ExtractionResult(string name, string message, bool success)
    {
        Name = name;
        Success = success;
        Message = message;
    }

    public string Name { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; }
}