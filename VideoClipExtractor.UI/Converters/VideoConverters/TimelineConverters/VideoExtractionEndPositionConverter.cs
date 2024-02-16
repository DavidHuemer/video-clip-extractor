using System.Globalization;
using System.Windows;
using System.Windows.Data;
using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.UI.Converters.VideoConverters.TimelineConverters;

public class VideoExtractionEndPositionConverter(ITimelineFrameWidthHandler? timelineFrameWidthHandler = null)
    : IMultiValueConverter
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler =
        timelineFrameWidthHandler ?? new TimelineFrameWidthHandler();

    public VideoExtractionEndPositionConverter() : this(null)
    {
    }


    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not int start) return new Thickness(0, 0, 0, 0);

        if (values[1] is not int end) return new Thickness(0, 0, 0, 0);

        if (values[2] is not int zoomLevel) return new Thickness(0, 0, 0, 0);

        if (values[3] is not double width) return new Thickness(0, 0, 0, 0);

        var widthMultiplier = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);

        var calculatedMargin = (end - start) * widthMultiplier;

        return new Thickness(calculatedMargin - (width / 2), 0, 0, 0);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}