using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

public interface IExtractionVisualizationViewModel : IBaseViewModel
{
    Task ExtractVideos(IEnumerable<VideoViewModel> videos);

    #region Properties

    public bool ExtractionFinished { get; set; }

    IBaseViewModel ActiveViewModel { get; }

    #endregion
}