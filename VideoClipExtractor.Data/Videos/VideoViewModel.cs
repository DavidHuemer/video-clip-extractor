using BaseUI.ViewModels;

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

    #endregion
}