using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

public interface IExtractionVisualizationViewModel
{
    #region Properties

    public bool ExtractionFinished { get; set; }

    #endregion

    Task ExtractVideos(IEnumerable<VideoViewModel> videos);
}