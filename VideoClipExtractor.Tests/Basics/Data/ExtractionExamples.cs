using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Tests.Basics.Data;

/// <summary>
/// Contains examples of extraction data.
/// </summary>
public static class ExtractionExamples
{
    public static ImageExtraction GetImageExtractionExample(string name = "", int frame = 30)
    {
        var extraction = new ImageExtraction(new VideoPosition(frame));
        if (!string.IsNullOrWhiteSpace(name))
        {
            extraction.Name = name;
        }

        return extraction;
    }

    public static VideoExtraction GetVideoExtractionExample(string name = "", int startFrame = 30, int endFrame = 60)
    {
        var extraction = new VideoExtraction(new VideoPosition(startFrame), new VideoPosition(endFrame));
        if (!string.IsNullOrWhiteSpace(name))
        {
            extraction.Name = name;
        }

        return extraction;
    }

    public static IEnumerable<ImageExtraction> GetImageExtractionExamples(int nrExtractions)
    {
        for (var i = 0; i < nrExtractions; i++)
        {
            yield return GetImageExtractionExample($"ImageExtraction{i}", i * 30);
        }
    }

    public static IEnumerable<VideoExtraction> GetVideoExtractionExamples(int nrExtractions)
    {
        for (var i = 0; i < nrExtractions; i++)
        {
            yield return GetVideoExtractionExample($"VideoExtraction{i}", i * 30, i * 60);
        }
    }
}