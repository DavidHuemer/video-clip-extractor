using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class ExtractionResultExamples
{
    public const string Name = "az_123.mp4";

    public static ExtractionResult GetSuccessResultExample()
    {
        return new ExtractionResult(Name, "This is a success message.", true);
    }
}