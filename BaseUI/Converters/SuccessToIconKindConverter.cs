using System.Globalization;
using System.Windows.Data;

namespace BaseUI.Converters
{
    public class SuccessToIconKindConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool success)
            {
                return success ? Material.Icons.MaterialIconKind.CheckCircle : Material.Icons.MaterialIconKind.Error;
            }

            return Material.Icons.MaterialIconKind.QuestionMark;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}