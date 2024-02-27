using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Tests.Data.Extractions.Results;

[TestFixture]
[TestOf(typeof(ExtractionResult))]
public class ExtractionResultTest
{
    [Test]
    public void ExceptionCreatesCorrectExtractionResult()
    {
        var exception = new Exception("Test exception");
        var result = new ExtractionResult(exception, "Test name");
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("Test name"));
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Test exception"));
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