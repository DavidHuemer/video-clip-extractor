using System.Globalization;
using Material.Icons;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Converters.ExtractionConverters;

namespace VideoClipExtractor.Tests.UI.Converters.ExtractionConverters;

[TestFixture]
[TestOf(typeof(ExtractionResultToIconConverter))]
public class ExtractionResultToIconConverterTest
{
    [SetUp]
    public void Setup()
    {
        _converter = new ExtractionResultToIconConverter();
    }

    private ExtractionResultToIconConverter _converter = null!;


    [Test]
    public void NullReturnsQuestionMark()
    {
        var result = _converter.Convert(null, typeof(MaterialIconKind), null, CultureInfo.InvariantCulture);

        Assert.That(result, Is.EqualTo(MaterialIconKind.QuestionMark));
    }

    [Test]
    public void SuccessReturnsCheck()
    {
        var result = _converter.Convert(ExtractionResultExamples.GetSuccessResultExample(),
            typeof(MaterialIconKind), null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo(MaterialIconKind.Check));
    }

    [Test]
    public void FailureReturnsError()
    {
        var result = _converter.Convert(ExtractionResultExamples.GetFailureResultExample(),
            typeof(MaterialIconKind), null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo(MaterialIconKind.Error));
    }
}