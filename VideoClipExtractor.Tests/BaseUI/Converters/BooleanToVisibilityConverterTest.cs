using System.Globalization;
using System.Windows;
using BaseUI.Converters;

namespace VideoClipExtractor.Tests.BaseUI.Converters;

[TestFixture]
[TestOf(typeof(BooleanToVisibilityConverter))]
public class BooleanToVisibilityConverterTest
{
    private BooleanToVisibilityConverter _booleanToVisibilityConverter = null!;

    [SetUp]
    public void Setup()
    {
        _booleanToVisibilityConverter = new BooleanToVisibilityConverter();
    }

    [Test]
    [TestCase(true, Visibility.Visible)]
    [TestCase(false, Visibility.Collapsed)]
    public void ConvertWithNoOptionalParameterReturnsCorrectValue(bool b, Visibility expected)
    {
        var result = _booleanToVisibilityConverter.Convert(b, typeof(Visibility), null, CultureInfo.InvariantCulture);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(true, Visibility.Visible, Visibility.Visible)]
    [TestCase(false, Visibility.Visible, Visibility.Collapsed)]
    [TestCase(true, Visibility.Collapsed, Visibility.Collapsed)]
    [TestCase(false, Visibility.Collapsed, Visibility.Visible)]
    public void ConvertWithOptionalParameterReturnsCorrectValue(bool value, Visibility parameter, Visibility expected)
    {
        var result =
            _booleanToVisibilityConverter.Convert(value, typeof(Visibility), parameter, CultureInfo.InvariantCulture);

        Assert.That(result, Is.EqualTo(expected));
    }
}