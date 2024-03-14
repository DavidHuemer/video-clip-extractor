using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data.VideoExamples;

public static class RealisticVideos
{
    public const int RealisticSourceVideosCount = 20;
    public const int ExtractedVideosCount = 4;

    public static readonly VideoStatus[] VideoStatusArray =
    [
        VideoStatus.ReadyForExport,
        VideoStatus.ReadyForExport,
        VideoStatus.Skipped,
        VideoStatus.Unset,
    ];

    public static readonly int WorkingVideosCount = VideoStatusArray.Length;

    public static readonly int[] NrImageExtractions = [2, 0, 3, 1];

    public static readonly int[] NrVideoExtractions = [0, 1, 0, 2];
}