using System.Collections.ObjectModel;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Data.Videos;

public class VideoViewModel : BaseViewModel
{
    public VideoViewModel(Video video)
    {
        VideoStatus = video.VideoStatus;
        SourcePath = video.SourcePath;
        LocalPath = video.Path;
        Name = video.Name;
        Bytes = video.Bytes;
    }

    public IEnumerable<IExtraction> GetExtractions()
    {
        IEnumerable<IExtraction> imageExtractions = ImageExtractions.ToList();
        IEnumerable<IExtraction> videoExtractions = VideoExtractions.ToList();
        return imageExtractions.Concat(videoExtractions);
    }

    #region Properties

    private VideoStatus _videoStatus;

    public VideoStatus VideoStatus
    {
        get => _videoStatus;
        set
        {
            _videoStatus = value;
            OnPropertyChanged();
        }
    }

    public string SourcePath { get; set; }
    public string LocalPath { get; }
    public string Name { get; }


    private bool _isExtracting;

    public bool IsExtracting
    {
        get => _isExtracting;
        set
        {
            _isExtracting = value;
            OnPropertyChanged();
        }
    }

    private VideoExtractionResult? _extractionResult;

    public VideoExtractionResult? ExtractionResult
    {
        get => _extractionResult;
        set
        {
            _extractionResult = value;
            OnPropertyChanged();
        }
    }

    public long Bytes { get; set; }

    public ObservableCollection<ImageExtraction> ImageExtractions { get; } = [];
    public ObservableCollection<VideoExtraction> VideoExtractions { get; } = [];

    #endregion
}