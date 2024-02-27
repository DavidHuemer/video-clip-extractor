using System.Globalization;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Converters.ExtractionConverters;

namespace VideoClipExtractor.Tests.UI.Converters.ExtractionConverters;

[TestFixture]
[TestOf(typeof(ExtractionNameToTextConverter))]
public class ExtractionNameToTextConverterTest
{
    [SetUp]
    public void SetUp()
    {
        _extractionNameToTextConverter = new ExtractionNameToTextConverter();
    }

    private ExtractionNameToTextConverter _extractionNameToTextConverter;

    [Test]
    public void NullReturnsDashed()
    {
        var result = _extractionNameToTextConverter.Convert(null, null, null, null);
        Assert.That(result, Is.EqualTo("---"));
    }

    [Test]
    public void EmptyNameReturnsDashed()
    {
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        imageExtraction.Name = string.Empty;

        var result =
            _extractionNameToTextConverter.Convert(imageExtraction, typeof(string), null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo("---"));
    }

    [Test]
    public void NonEmptyNameReturnsName()
    {
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        imageExtraction.Name = "Name";

        var result =
            _extractionNameToTextConverter.Convert(imageExtraction, typeof(string), null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo("Name"));
    }

    [Test]
    [TestCase("")]
    [TestCase("Name")]
    public void ExtractionWithResultReturnsResultName(string extractionName)
    {
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        imageExtraction.Name = extractionName;
        imageExtraction.Result = ExtractionResultExamples.GetSuccessResultExample();

        var result =
            _extractionNameToTextConverter.Convert(imageExtraction, typeof(string), null, CultureInfo.InvariantCulture);

        Assert.That(result, Is.EqualTo(ExtractionResultExamples.Name));
    }

    [Test]
    [TestCase("", "---")]
    [TestCase("Name", "Name")]
    public void ConvertWithResultWithoutNameReturnsCorrectValue(string extractionName, string expected)
    {
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        imageExtraction.Name = extractionName;

        var extractionResult = ExtractionResultExamples.GetSuccessResultExample();
        extractionResult.Name = "";
        imageExtraction.Result = extractionResult;

        var result =
            _extractionNameToTextConverter.Convert(imageExtraction, typeof(string), null, CultureInfo.InvariantCulture);

        Assert.That(result, Is.EqualTo(expected));
    }


    [Test]
    public void METHOD()
    {
    }
}