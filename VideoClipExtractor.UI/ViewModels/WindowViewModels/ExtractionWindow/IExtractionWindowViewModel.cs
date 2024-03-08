using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels.ExtractionWindow;

public interface IExtractionWindowViewModel : IWindowViewModel
{
    void SetupExtraction(IEnumerable<VideoViewModel> videos);
}