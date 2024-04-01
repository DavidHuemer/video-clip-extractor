using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.VideoManager;

[Singleton]
public class VideoManager : IVideoManager
{
    private VideoViewModel? _video;
    public event Action<VideoViewModel?>? VideoChanged;

    public VideoViewModel? Video
    {
        get => _video;
        set
        {
            _video = value;
            VideoChanged?.Invoke(_video);
        }
    }
}