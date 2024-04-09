using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.UI.Timeline;

public class TimelineIndicator
{
    public TimelineIndicator(VideoPosition videoPosition, double supporterSize)
    {
        VideoPosition = videoPosition;
        SupporterSteps = new double[4];

        for (var i = 0; i < SupporterSteps.Length; i++)
        {
            SupporterSteps[i] = supporterSize * (i + 1);
        }
    }

    public VideoPosition VideoPosition { get; set; }

    public double[] SupporterSteps { get; set; }
}