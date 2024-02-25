using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.Managers.Extraction;

public interface IExtractionManager
{
    void ExtractVideos(IEnumerable<VideoViewModel> videos);
}