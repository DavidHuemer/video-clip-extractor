using System.Globalization;
using System.Windows.Data;
using Material.Icons;
using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.UI.Converters.ExtractionConverters
{
    public class ExtractionResultToIconConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ExtractionResult result)
            {
                return result.Success ? MaterialIconKind.Check : MaterialIconKind.Error;
            }

            return MaterialIconKind.QuestionMark;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}