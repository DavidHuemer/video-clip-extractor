using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Managers.VideoManager;

/// <summary>
/// Manages the current video
/// As normal for managers, this should be a singleton
/// </summary>
public interface IVideoManager
{
    /// <summary>
    /// The current video
    /// </summary>
    public Video? Video { get; set; }

    #region Events

    public event EventHandler<VideoChangedEventArgs>? VideoChanged;

    #endregion
}