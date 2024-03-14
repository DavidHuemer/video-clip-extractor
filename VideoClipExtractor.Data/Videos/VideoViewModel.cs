using System.Collections.ObjectModel;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using PropertyChanged;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Data.Videos;

public class VideoViewModel : BaseViewModel
{
    public VideoViewModel(CachedVideo video)
    {
        SourcePath = video.SourceVideo.Path;
        LocalPath = video.LocalPath;
        Name = video.SourceVideo.Name;
    }

    [UsedImplicitly]
    public VideoViewModel()
    {
    }

    public IEnumerable<IExtraction> GetExtractions()
    {
        IEnumerable<IExtraction> imageExtractions = ImageExtractions.ToList();
        IEnumerable<IExtraction> videoExtractions = VideoExtractions.ToList();
        return imageExtractions.Concat(videoExtractions);
    }

    public override bool Equals(object? obj)
    {
        return obj is VideoViewModel model &&
               VideoStatus == model.VideoStatus &&
               SourcePath == model.SourcePath &&
               LocalPath == model.LocalPath &&
               Name == model.Name &&
               IsExtracting == model.IsExtracting &&
               Bytes == model.Bytes &&
               ImageExtractions.SequenceEqual(model.ImageExtractions) &&
               VideoExtractions.SequenceEqual(model.VideoExtractions);
    }

    public override int GetHashCode() => HashCode.Combine(SourcePath, LocalPath, Name);

    #region Properties

    /// <summary>
    /// The status of the video
    /// </summary>
    public VideoStatus VideoStatus { get; set; } = VideoStatus.Unset;

    /// <summary>
    /// The path to the video file
    /// </summary>
    [DoNotNotify]
    public string SourcePath { get; [UsedImplicitly] init; } = "";

    /// <summary>
    /// The path to the cached video file
    /// </summary>
    [DoNotNotify]
    public string LocalPath { get; [UsedImplicitly] init; } = "";

    /// <summary>
    /// The name of the video
    /// </summary>
    [DoNotNotify]
    public string Name { get; [UsedImplicitly] init; } = "";

    public bool IsExtracting { get; set; }
    public VideoExtractionResult? ExtractionResult { get; set; }

    public long Bytes { get; [UsedImplicitly] init; }

    public ObservableCollection<ImageExtraction> ImageExtractions { get; init; } = [];
    public ObservableCollection<VideoExtraction> VideoExtractions { get; init; } = [];

    #endregion
}