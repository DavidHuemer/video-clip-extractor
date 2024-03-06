using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Data.Extractions.Results;

[TestFixture]
[TestOf(typeof(ExtractionProcessResult))]
public class ExtractionProcessResultTest
{
    [Test]
    public void ConstructorWithExceptionReturnsCorrectResult()
    {
        var exception = new Exception("Test");
        var result = new ExtractionProcessResult(exception);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("Test"));
        });
    }

    [Test]
    public void ConstructorWithWithNoFailedExtractionsReturnsCorrectResult()
    {
        var videoExtractionResults = ExtractionResultExamples.GetSuccessVideoExtractionResultExamples(4);
        var result = new ExtractionProcessResult(videoExtractionResults);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void ConstructorWithWithOneFailedExtractionReturnsCorrectResult()
    {
        var videoExtractionResults = new List<VideoExtractionResult>
        {
            ExtractionResultExamples.GetSuccessVideoExtractionResultExample(),
            ExtractionResultExamples.GetFailureVideoExtractionResultExample()
        };
        var result = new ExtractionProcessResult(videoExtractionResults);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("This is a failure message."));
        });
    }

    [Test]
    public void ConstructorWithWithMultipleFailedExtractionsReturnsCorrectResult()
    {
        var videoExtractionResults = new List<VideoExtractionResult>
        {
            ExtractionResultExamples.GetFailureVideoExtractionResultExample(),
            ExtractionResultExamples.GetFailureVideoExtractionResultExample()
        };
        var result = new ExtractionProcessResult(videoExtractionResults);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("Multiple videos failed to extract."));
        });
    }
}