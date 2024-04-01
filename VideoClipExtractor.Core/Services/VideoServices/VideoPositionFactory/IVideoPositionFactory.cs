using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Services.VideoServices.VideoPositionFactory;

public interface IVideoPositionFactory
{
    VideoPosition GetVideoPositionByFrame(int frame);

    VideoPosition GetVideoPositionByString(string videoPosition);
}