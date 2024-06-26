﻿namespace VideoClipExtractor.Tests.Basics.Data;

/// <summary>
/// Contains examples of video data.
/// </summary>
public static class VideoExamples1
{
    // private const string SourcePath = @$"{VideoRepositoryExamples.VideoRepositoryPath}\Video.mp4";
    // private const string LocalPath = @"C:\Cached\Video.mp4";
    //
    // public static CachedVideo GetCachedVideoExample(string name = "Video")
    // {
    //     var sourcePath = @$"{VideoRepositoryExamples.VideoRepositoryPath}\{name}.mp4";
    //     var localPath = @$"C:\Cached\{name}.mp4";
    //     return GetCachedVideoExample(sourcePath, localPath);
    // }
    //
    // public static Video GetVideoExample()
    // {
    //     return new Video(new CachedVideo(GetSourceVideoExample(), LocalPath));
    // }
    //
    // public static VideoViewModel GetVideoViewModelExample()
    // {
    //     return new VideoViewModel(GetVideoExample());
    // }
    //
    // /// <summary>
    // /// Returns a source video example.
    // /// </summary>
    // /// <returns></returns>
    // public static SourceVideo GetSourceVideo()
    // {
    //     return new SourceVideo(SourcePath, 1048);
    // }
    //
    // public static IEnumerable<VideoViewModel> GetExampleVideos(int nrVideos)
    // {
    //     var videos = new List<VideoViewModel>();
    //     for (var i = 0; i < nrVideos; i++)
    //     {
    //         videos.Add(GetVideoViewModelExample());
    //     }
    //
    //     return videos;
    // }

    // public static IEnumerable<VideoViewModel> GetRealisticVideoViewModels()
    // {
    //     var videos = GetExampleVideos(8).ToList();
    //     videos[0].ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample());
    //     videos[0].VideoStatus = VideoStatus.ReadyForExport;
    //
    //     videos[1].ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample());
    //     videos[1].ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample("", 50));
    //     videos[1].VideoStatus = VideoStatus.ReadyForExport;
    //
    //     videos[2].ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample());
    //     videos[2].VideoStatus = VideoStatus.Skipped;
    //
    //     videos[3].VideoExtractions.Add(ExtractionExamples.GetVideoExtractionExample());
    //     videos[3].VideoStatus = VideoStatus.ReadyForExport;
    //
    //     videos[4].VideoStatus = VideoStatus.Skipped;
    //
    //     videos[5].ImageExtractions.Add(ExtractionExamples.GetImageExtractionExample());
    //     videos[5].VideoStatus = VideoStatus.Unset;
    //
    //     return videos;
    // }
}