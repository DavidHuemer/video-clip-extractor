using FFMpeg.Wrapper.Engine;

namespace VideoClipExtractor.Tests.FFMpeg.Engine;

[TestFixture]
[TestOf(typeof(MpegEngine))]
public class MpegEngineTest
{
    [Test]
    public void EmptyConstructor()
    {
        var mpegEngine = new MpegEngine();
        Assert.That(mpegEngine.FfMpegPath, Is.Not.Empty);
    }
}