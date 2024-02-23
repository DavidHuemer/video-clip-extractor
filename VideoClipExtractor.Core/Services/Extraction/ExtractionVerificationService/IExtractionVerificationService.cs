namespace VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;

public interface IExtractionVerificationService
{
    /// <summary>
    /// Returns true if the extraction was successful
    /// </summary>
    /// <param name="path">The path to the extraction</param>
    /// <returns>If the extraction was successfully</returns>
    bool ExtractionSucceeded(string path);
}