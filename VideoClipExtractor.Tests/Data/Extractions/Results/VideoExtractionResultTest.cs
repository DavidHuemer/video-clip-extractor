using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Data.Extractions.Results;

[TestFixture]
[TestOf(typeof(VideoExtractionResult))]
public class VideoExtractionResultTest
{
    [Test]
    public void ConstructorWithExceptionCreatesVideoExtractionResultCorrectly()
    {
        var exception = new Exception("Test exception");
        var result = new VideoExtractionResult(exception);
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo(exception.Message));
            Assert.That(result.ExtractionResults, Is.Empty);
        });
    }

    [Test]
    public void ConstructorWithEmptyExtractionsReturnsCorrectVideoExtractionResult()
    {
        var result = new VideoExtractionResult([]);
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.Empty);
            Assert.That(result.ExtractionResults, Is.Empty);
        });
    }

    [Test]
    public void ConstructorWithAllSuccessExtractionsReturnsCorrectVideoExtractionResult()
    {
        var extractions = ExtractionResultExamples.GetSuccessResultExamples(4).ToList();
        var result = new VideoExtractionResult(extractions);
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.Empty);
            Assert.That(result.ExtractionResults, Is.EquivalentTo(extractions));
        });
    }

    [Test]
    public void ConstructorWithSingleFailureExtractionsReturnsCorrectVideoExtractionResult()
    {
        var extractions = new List<ExtractionResult>
        {
            ExtractionResultExamples.GetSuccessResultExample(),
            ExtractionResultExamples.GetFailureResultExample("Extraction failed"),
            ExtractionResultExamples.GetSuccessResultExample(),
            ExtractionResultExamples.GetSuccessResultExample()
        };
        var result = new VideoExtractionResult(extractions);
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Extraction failed"));
            Assert.That(result.ExtractionResults, Is.EquivalentTo(extractions));
        });
    }

    [Test]
    public void ConstructorWithMultipleFailureExtractionsReturnsCorrectVideoExtractionResult()
    {
        var extractions = new List<ExtractionResult>
        {
            ExtractionResultExamples.GetSuccessResultExample(),
            ExtractionResultExamples.GetFailureResultExample("Extraction failed"),
            ExtractionResultExamples.GetFailureResultExample("Extraction failed"),
            ExtractionResultExamples.GetSuccessResultExample()
        };
        var result = new VideoExtractionResult(extractions);
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Multiple extractions failed."));
            Assert.That(result.ExtractionResults, Is.EquivalentTo(extractions));
        });
    }
}