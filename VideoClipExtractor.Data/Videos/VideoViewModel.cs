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
    }

    #region Properties

    public VideoStatus VideoStatus { get; set; }

    public string LocalPath { get; set; }

    public ObservableCollection<ImageExtractionViewModel> ImageExtractions { get; } = new();

    #endregion
}