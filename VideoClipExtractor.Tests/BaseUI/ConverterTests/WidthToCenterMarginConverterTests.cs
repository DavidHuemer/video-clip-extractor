using System.Globalization;
using System.Windows;
using BaseUI.Converters;

namespace VideoClipExtractor.Tests.BaseUI.ConverterTests;

public class WidthToCenterMarginConverterTests
{
    [Test]
    [TestCase(10.0, -5.0)]
    [TestCase(0.0, 0.0)]
    public void ConvertReturnsCorrectValue(double width, double expected)
    {
        // Arrange
        var converter = new WidthToCenterMarginConverter();
        var expectedThickness = new Thickness(expected, 0, 0, 0);

        // Act
        var result = converter.Convert(width, typeof(Thickness), null, CultureInfo.CurrentCulture);

        // Assert
        Assert.That(result, Is.EqualTo(expectedThickness));
    }

    [Test]
    public void ConvertWithNoParameterReturnsDefaultMargin()
    {
        // Arrange
        var converter = new WidthToCenterMarginConverter();
        var expectedThickness = new Thickness(0, 0, 0, 0);

        // Act
        var result = converter.Convert(null, typeof(Thickness), null, CultureInfo.CurrentCulture);

        // Assert
        Assert.That(result, Is.EqualTo(expectedThickness));
    }
}