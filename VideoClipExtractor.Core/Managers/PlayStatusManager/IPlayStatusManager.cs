using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Managers.PlayStatusManager;

public interface IPlayStatusManager
{
    PlayStatus MainPlayStatus { get; }
    event Action<PlayStatus> PlayStatusChanged;
    event EventHandler PlayPause;

    void SetMainPlayStatus(PlayStatus playStatus);

    void VideoPositionChanged();
}