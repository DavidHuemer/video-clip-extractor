using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Data.Videos;

[TestFixture]
[TestOf(typeof(SourceVideo))]
public class SourceVideoTest
{
    [Test]
    [TestCase(@"C\Source\Video.mp4", "Video.mp4")]
    [TestCase(@"C\Video.mp4", "Video.mp4")]
    [TestCase(@"C\Source\Test\Video.mp4", "Video.mp4")]
    [TestCase(@"C/Source/Test/Video.mp4", "Video.mp4")]
    [TestCase(@"C/Video.mp4", "Video.mp4")]
    [TestCase(@"\\Intern\Test\Video.mp4", "Video.mp4")]
    [TestCase("", "")]
    public void FullNameIsCorrect(string path, string expectedFullName)
    {
        var sourceVideo = new SourceVideo(path, 1048);
        Assert.That(sourceVideo.FullName, Is.EqualTo(expectedFullName));
    }

    [Test]
    [TestCase(@"C\Source\Video.mp4", "Video")]
    [TestCase(@"C\Video.mp4", "Video")]
    [TestCase(@"C\Source\Test\Video.mp4", "Video")]
    [TestCase(@"C/Source/Test/Video.mp4", "Video")]
    [TestCase(@"C/Video.mp4", "Video")]
    [TestCase(@"\\Intern\Test\Video.mp4", "Video")]
    [TestCase("", "")]
    public void NameIsCorrect(string path, string expectedName)
    {
        var sourceVideo = new SourceVideo(path, 1048);
        Assert.That(sourceVideo.Name, Is.EqualTo(expectedName));
    }
}