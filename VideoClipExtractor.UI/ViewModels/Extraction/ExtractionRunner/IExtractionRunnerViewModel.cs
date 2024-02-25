using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

public interface IExtractionRunnerViewModel
{
    VideoViewModel? CurrentVideo { get; set; }
    
    Task ExtractVideos(IEnumerable<VideoViewModel> videos);
}