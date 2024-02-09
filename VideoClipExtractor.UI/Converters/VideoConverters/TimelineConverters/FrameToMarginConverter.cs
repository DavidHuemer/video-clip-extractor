using System.Globalization;
using System.Windows;
using System.Windows.Data;
using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.UI.Converters.VideoConverters.TimelineConverters
{
    public class FrameToMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values[0] is not int frame) return new Thickness(0, 0, 0, 0);

            if (values[1] is not int zoomLevel) return new Thickness(0, 0, 0, 0);

            var widthMultiplier = TimelineFrameWidthHandler.GetFrameWidth(zoomLevel);

            var calculatedMargin = frame * widthMultiplier;
            return new Thickness(calculatedMargin, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}