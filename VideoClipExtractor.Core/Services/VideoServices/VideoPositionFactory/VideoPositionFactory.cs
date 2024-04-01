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

    public VideoPosition GetVideoPositionByString(string videoPosition)
    {
        var video = _videoManager.Video;
        return video == null
            ? GetVideoPositionByString(videoPosition, 30)
            : GetVideoPositionByString(videoPosition, video.VideoInfo.FrameRate);
    }

    private VideoPosition GetVideoPositionByFrame(int frame, double framerate)
    {
        var totalDurationSeconds = frame / framerate;

        var timespan = TimeSpan.FromSeconds(totalDurationSeconds);
        return new VideoPosition(timespan, framerate);
    }

    private VideoPosition GetVideoPositionByString(string videoPosition, double framerate)
    {
        var videoPositionParts = videoPosition.Split(':');
        if (videoPositionParts.Length != 4)
        {
            throw new ArgumentException("Video position must be in the format of HH:MM:SS:FF");
        }

        try
        {
            var hours = int.Parse(videoPositionParts[0]);
            var minutes = int.Parse(videoPositionParts[1]);
            var seconds = int.Parse(videoPositionParts[2]);
            var frames = int.Parse(videoPositionParts[3]);

            var totalDurationSeconds = hours * 3600 + minutes * 60 + seconds + frames / framerate;
            var timespan = TimeSpan.FromSeconds(totalDurationSeconds);
            return new VideoPosition(timespan, framerate);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Video position must be in the format of HH:MM:SS:FF");
        }
    }
}