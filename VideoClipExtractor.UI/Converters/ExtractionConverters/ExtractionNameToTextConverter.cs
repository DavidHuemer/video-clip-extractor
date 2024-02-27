using System.Globalization;
using System.Windows.Data;
using VideoClipExtractor.Data.Extractions.Basics;

namespace VideoClipExtractor.UI.Converters.ExtractionConverters
{
    public class ExtractionNameToTextConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not IExtraction extraction) return "---";

            if (extraction.Result == null)
                return extraction.Name == string.Empty ? "---" : extraction.Name;

            if (extraction.Result.Name == string.Empty)
                return extraction.Name == string.Empty ? "---" : extraction.Name;

            return extraction.Result.Name;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}