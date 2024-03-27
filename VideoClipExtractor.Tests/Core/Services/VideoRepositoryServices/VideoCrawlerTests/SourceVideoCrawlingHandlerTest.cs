using VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoRepositoryServices.VideoCrawlerTests;

[TestFixture]
[TestOf(typeof(SourceVideoCrawlingHandler))]
public class SourceVideoCrawlingHandlerTest
{
    [Test]
    public void ReturnTrueWhenSourceVideosEmpty()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        var sourceVideos = new List<SourceVideo>();

        var result = SourceVideoCrawlingHandler.ShouldCrawl(sourceVideo, sourceVideos);
        Assert.IsTrue(result);
    }

    [Test]
    public void ReturnTrueWhenSourceVideosDoesNotContainSourceVideo()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        var sourceVideos = new List<SourceVideo>
        {
            SourceVideoExamples.GetSourceVideoExample(path: SourceVideoExamples.GetSourcePath("OtherVideo.mp4")),
        };

        var result = SourceVideoCrawlingHandler.ShouldCrawl(sourceVideo, sourceVideos);
        Assert.IsTrue(result);
    }

    [Test]
    public void ReturnTrueWhenSourceVideosContainSourceVideoButNotChecked()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        var existingSourceVideo = SourceVideoExamples.GetSourceVideoExample(path: sourceVideo.Path);
        existingSourceVideo.Checked = false;

        var sourceVideos = new List<SourceVideo>
        {
            existingSourceVideo,
        };

        var result = SourceVideoCrawlingHandler.ShouldCrawl(sourceVideo, sourceVideos);
        Assert.IsTrue(result);
    }

    [Test]
    public void ReturnFalseWhenSourceVideosContainSourceVideoAndChecked()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        var existingSourceVideo = SourceVideoExamples.GetSourceVideoExample(path: sourceVideo.Path);
        existingSourceVideo.Checked = true;

        var sourceVideos = new List<SourceVideo>
        {
            existingSourceVideo,
        };

        var result = SourceVideoCrawlingHandler.ShouldCrawl(sourceVideo, sourceVideos);
        Assert.IsFalse(result);
    }
}