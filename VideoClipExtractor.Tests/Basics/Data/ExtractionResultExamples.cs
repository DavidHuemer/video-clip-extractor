using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class ExtractionResultExamples
{
    public const string Name = "az_123.mp4";
    private const string Path = @$"C:\Extractions\{Name}";

    public static ExtractionResult GetSuccessResultExample()
    {
        return new ExtractionResult(Path);
    }

    public static IEnumerable<ExtractionResult> GetSuccessResultExamples(int nrExtractions)
    {
        for (var i = 0; i < nrExtractions; i++)
        {
            yield return new ExtractionResult(Path);
        }
    }

    public static ExtractionResult GetFailureResultExample(string message = "This is a failure message.")
    {
        return new ExtractionResult(Path, message, false);
    }

    public static VideoExtractionResult GetSuccessVideoExtractionResultExample()
    {
        return new VideoExtractionResult([GetSuccessResultExample()]);
    }

    public static IEnumerable<VideoExtractionResult> GetSuccessVideoExtractionResultExamples(int nrExtractions)
    {
        for (var i = 0; i < nrExtractions; i++)
        {
            yield return new VideoExtractionResult([GetSuccessResultExample()]);
        }
    }

    public static VideoExtractionResult GetFailureVideoExtractionResultExample(
        string message = "This is a failure message.")
    {
        return new VideoExtractionResult(new Exception(message));
    }

    public static ExtractionProcessResult GetSuccessExtractionProcessResultExample()
    {
        return new ExtractionProcessResult([]);
    }
}