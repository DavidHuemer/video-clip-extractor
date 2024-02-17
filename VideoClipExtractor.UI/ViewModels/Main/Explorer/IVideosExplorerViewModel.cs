using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

public interface IVideosExplorerViewModel : INotifyPropertyChanged
{
    public VideoViewModel? SelectedVideo { get; }

    public int SelectedIndex { get; set; }

    ICommand ExportVideos { get; }

    ObservableCollection<VideoViewModel> Videos { get; }
}