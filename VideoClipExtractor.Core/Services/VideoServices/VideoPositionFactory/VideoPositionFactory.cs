using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Services.VideoServices.VideoPositionFactory;

[Singleton]
public class VideoPositionFactory(IDependencyProvider provider) : IVideoPositionFactory
{
    private readonly IVideoManager _videoManager = provider.GetDependency<IVideoManager>();

    public VideoPosition GetVideoPositionByFrame(int frame)
    {
        var video = _videoManager.Video;
        return video == null
            ? GetVideoPositionByFrame(frame, 30)
            : GetVideoPositionByFrame(frame, video.VideoInfo.FrameRate);
    }

    private VideoPosition GetVideoPositionByFrame(int frame, double framerate)
    {
        var totalDurationSeconds = frame / framerate;

        var timespan = TimeSpan.FromSeconds(totalDurationSeconds);
        return new VideoPosition(timespan, framerate);
    }
}