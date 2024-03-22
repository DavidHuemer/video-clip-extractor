using System.Collections.ObjectModel;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data.VideoExamples;

public static class VideoExamples
{
    public static VideoViewModel GetVideoViewModelExample(string videoName = "Video",
        VideoStatus videoStatus = VideoStatus.Unset,
        List<ImageExtraction>? imageExtractions = null,
        List<VideoExtraction>? videoExtractions = null)
    {
        return new VideoViewModel(CachedVideoExamples.GetCachedVideoExample(videoName))
        {
            VideoStatus = videoStatus,
            ImageExtractions = new ObservableCollection<ImageExtraction>(imageExtractions ?? []),
            VideoExtractions = new ObservableCollection<VideoExtraction>(videoExtractions ?? []),
        };
    }

    public static List<VideoViewModel> GetVideoViewModelExamples(int count)
    {
        return Enumerable.Range(1, count)
            .Select(x => GetVideoViewModelExample($"Video {x}"))
            .ToList();
    }

    public static VideoViewModel GetVideoViewModelBySourceVideo(SourceVideo sourceVideo)
    {
        return new VideoViewModel(CachedVideoExamples.GetCachedVideoExampleBySourceVideo(sourceVideo));
    }

    private static VideoViewModel GetRealisticVideoViewModel(int index)
    {
        var videoStatus = RealisticVideos.VideoStatusArray[index];
        var imageExtractions = ExtractionExamples
            .GetImageExtractionExamples(RealisticVideos.NrImageExtractions[index]);

        var videoExtractions = ExtractionExamples
            .GetVideoExtractionExamples(RealisticVideos.NrVideoExtractions[index]);


        return GetVideoViewModelExample(
            videoStatus: videoStatus,
            imageExtractions: imageExtractions,
            videoExtractions: videoExtractions
        );
    }

    public static List<VideoViewModel> GetRealisticVideoViewModels()
    {
        return Enumerable.Range(0, RealisticVideos.WorkingVideosCount)
            .Select(GetRealisticVideoViewModel)
            .ToList();
    }
}