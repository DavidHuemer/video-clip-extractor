using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Tests.Data.Extractions.Results;

[TestFixture]
[TestOf(typeof(ExtractionResult))]
public class ExtractionResultTest
{
    private const string ExtractionName = "image.png";
    private const string ExtractionPath = @$"C\Extractions\{ExtractionName}";

    [Test]
    public void ExceptionWithPathCreatesCorrectExtractionResult()
    {
        var exception = new Exception("Test exception");
        var result = new ExtractionResult(exception, ExtractionPath);
        Assert.Multiple(() =>
        {
            Assert.That(result.Path, Is.EqualTo(ExtractionPath));
            Assert.That(result.Name, Is.EqualTo(ExtractionName));
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Test exception"));
        });
    }

    [Test]
    public void ExceptionWithoutPathCreatesCorrectExtractionResult()
    {
        var exception = new Exception("Test exception");
        var result = new ExtractionResult(exception);

        Assert.Multiple(() =>
        {
            Assert.That(result.Path, Is.Empty);
            Assert.That(result.Name, Is.Empty);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Test exception"));
        });
    }

    [Test]
    public void ExtractionResultWithPathCreatesCorrectExtractionResult()
    {
        var result = new ExtractionResult(ExtractionPath);

        Assert.Multiple(() =>
        {
            Assert.That(result.Path, Is.EqualTo(ExtractionPath));
            Assert.That(result.Name, Is.EqualTo(ExtractionName));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.Empty);
        });
    }

    [Test]
    public void ExtractionResultIsCreatedCorrectly()
    {
        var result = new ExtractionResult("Test name", "Test message", true);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("Test name"));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Test message"));
        });
    }
}