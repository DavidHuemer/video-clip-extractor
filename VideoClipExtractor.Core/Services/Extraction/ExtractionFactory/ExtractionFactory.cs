using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionFactory;

[Transient]
public class ExtractionFactory : IExtractionFactory
{
    public IImageExtraction GetImageExtraction(VideoPosition position) => new ImageExtraction(position);

    public IVideoExtraction GetVideoExtraction(VideoPosition begin, VideoViewModel video)
    {
        var videoEnd = new VideoPosition(video.VideoInfo.Duration, video.VideoInfo.FrameRate);
        var extractionEnd = new VideoPosition(begin.Time.Add(TimeSpan.FromSeconds(5)), begin.FrameRate);

        if (extractionEnd.Time > videoEnd.Time)
        {
            extractionEnd = videoEnd;
        }

        return new VideoExtraction(begin, extractionEnd);
    }
}