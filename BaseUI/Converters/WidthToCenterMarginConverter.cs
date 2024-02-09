using System.Globalization;
using System.Windows.Data;

namespace BaseUI.Converters
{
    public class WidthToCenterMarginConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is not double width
                ? new Thickness(0, 0, 0, 0)
                : new Thickness(-width / 2, 0, 0, 0);

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}