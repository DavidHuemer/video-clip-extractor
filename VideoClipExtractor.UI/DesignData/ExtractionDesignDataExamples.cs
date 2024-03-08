using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.UI.DesignData
{
    public class ExtractionDesignDataExamples
    {
        public static ImageExtraction GetExampleImageExtraction()
        {
            return new ImageExtraction(new VideoPosition(50));
        }
    }
}