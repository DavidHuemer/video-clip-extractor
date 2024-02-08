using System.ComponentModel;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

public interface IVideosExplorerViewModel : INotifyPropertyChanged
{
    public VideoViewModel? SelectedVideo { get; }

    public int SelectedIndex { get; set; }
}