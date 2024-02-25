using System.Globalization;
using System.Windows.Data;

namespace BaseUI.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool b) return Visibility.Collapsed;

        return parameter switch
        {
            Visibility.Visible => b ? Visibility.Visible : Visibility.Collapsed,
            Visibility.Collapsed => b ? Visibility.Collapsed : Visibility.Visible,
            _ => b ? Visibility.Visible : Visibility.Collapsed
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Visibility visibility) return visibility == Visibility.Visible;

        return false;
    }
}