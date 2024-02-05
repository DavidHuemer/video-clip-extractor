using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

public interface IVideosExplorerViewModel
{
    public Video? SelectedVideo { get; set; }
}