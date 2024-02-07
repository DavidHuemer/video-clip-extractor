using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

public interface IVideosExplorerViewModel
{
    public VideoViewModel? SelectedVideo { get; }

    public int SelectedIndex { get; set; }
}