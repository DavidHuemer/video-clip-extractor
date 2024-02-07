using BaseUI.ViewModels;

namespace VideoClipExtractor.Data.Videos;

public class VideoViewModel : BaseViewModel
{
    public VideoViewModel(Video video)
    {
        VideoStatus = video.VideoStatus;
    }

    #region Properties

    public VideoStatus VideoStatus { get; set; }

    #endregion
}