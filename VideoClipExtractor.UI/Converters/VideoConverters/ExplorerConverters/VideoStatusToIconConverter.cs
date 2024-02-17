using System.Globalization;
using System.Windows.Data;
using Material.Icons;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.Converters.VideoConverters.ExplorerConverters;

public class VideoStatusToIconConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is VideoStatus videoStorageState)
            return videoStorageState switch
            {
                VideoStatus.Unset => MaterialIconKind.QuestionMark,
                VideoStatus.Skipped => MaterialIconKind.Pin,
                VideoStatus.ReadyForExport => MaterialIconKind.Check,
                _ => MaterialIconKind.AlertCircle,
            };

        return MaterialIconKind.AlertCircle;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}