using System.Globalization;
using System.Windows.Data;
using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.UI.Converters.VideoConverters.TimelineConverters;

public class VideoExtractionToWidthConverter(ITimelineFrameWidthHandler? timelineFrameWidthHandler = null)
    : IMultiValueConverter
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler =
        timelineFrameWidthHandler ?? new TimelineFrameWidthHandler();

    public VideoExtractionToWidthConverter() : this(null)
    {
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not int start) return 0;

        if (values[1] is not int end) return 0;

        if (values[2] is not int zoomLevel) return 0;

        var widthMultiplier = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);

        return (end - start) * widthMultiplier;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}