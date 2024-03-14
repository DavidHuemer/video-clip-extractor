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

    public static List<ImageExtraction> GetImageExtractionExamples(int nrExtractions)
    {
        return Enumerable.Range(0, nrExtractions)
            .Select(i => GetImageExtractionExample(frame: i * 30))
            .ToList();
    }

    public static List<VideoExtraction> GetVideoExtractionExamples(int nrExtractions)
    {
        return Enumerable.Range(0, nrExtractions)
            .Select(i => GetVideoExtractionExample($"VideoExtraction{i}", i * 30, i * 60))
            .ToList();
    }
}