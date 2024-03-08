using System.Globalization;
using System.Windows.Data;

namespace VideoClipExtractor.UI.Converters.ExtractionConverters;

public class ExtractionProcessResultToHeaderConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool result) return "Unknown";

        return result ? "Extraction: Success" : "Extraction: Failed";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}