using System.Globalization;
using System.Windows.Data;
using Material.Icons;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.UI.Converters.VideoConverters;

public class PlayStatusToIconKindConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is PlayStatus playStatus)
        {
            return playStatus switch
            {
                PlayStatus.Playing => MaterialIconKind.Pause,
                PlayStatus.Paused => MaterialIconKind.Play,
                _ => MaterialIconKind.Play
            };
        }

        return MaterialIconKind.Play;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}