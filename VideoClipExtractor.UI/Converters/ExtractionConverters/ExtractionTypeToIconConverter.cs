using System.Globalization;
using System.Windows.Data;
using Material.Icons;
using VideoClipExtractor.Data.Extractions;

namespace VideoClipExtractor.UI.Converters.ExtractionConverters
{
    public class ExtractionTypeToIconConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value switch
            {
                ImageExtraction => MaterialIconKind.Image,
                VideoExtraction => MaterialIconKind.Video,
                _ => MaterialIconKind.QuestionMark
            };

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}