using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Managers.VideoManager;

[UsedImplicitly]
public class VideoManager : IVideoManager
{
    #region Events

    public event EventHandler<VideoChangedEventArgs>? VideoChanged;

    #endregion

    #region Properties

    private Video? _video;

    public Video? Video
    {
        get => _video;
        set
        {
            _video = value;
            VideoChanged?.Invoke(this, new VideoChangedEventArgs(value));
        }
    }

    #endregion
}