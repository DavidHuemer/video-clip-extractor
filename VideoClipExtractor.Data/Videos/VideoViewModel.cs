using System.Collections.ObjectModel;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Extractions;

namespace VideoClipExtractor.Data.Videos;

public class VideoViewModel : BaseViewModel
{
    public VideoViewModel(Video video)
    {
        VideoStatus = video.VideoStatus;
        LocalPath = video.Path;
        Name = video.Name;
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

    public string LocalPath { get; }

    public string Name { get; }

    public ObservableCollection<ImageExtraction> ImageExtractions { get; } = [];
    public ObservableCollection<VideoExtraction> VideoExtractions { get; } = [];

    #endregion
}