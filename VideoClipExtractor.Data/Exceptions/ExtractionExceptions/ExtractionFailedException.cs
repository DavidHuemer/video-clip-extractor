namespace VideoClipExtractor.Data.Exceptions.ExtractionExceptions;

public class ExtractionFailedException(string extractionPath) : Exception($"Extraction failed: {extractionPath}");