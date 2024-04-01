using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.VideoManager;

/// <summary>
///     Manages the current video
///     As normal for managers, this should be a singleton
/// </summary>
public interface IVideoManager
{
    /// <summary>
    ///     The current video
    /// </summary>
    public VideoViewModel? Video { get; set; }

    #region Events

    public event Action<VideoViewModel?> VideoChanged;

    #endregion
}