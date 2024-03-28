using System.Collections.ObjectModel;
using System.ComponentModel;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

/// <summary>
/// View model for the videos explorer.
/// </summary>
public interface IVideosExplorerViewModel : INotifyPropertyChanged
{
    public VideoViewModel? SelectedVideo { get; }

    public int SelectedIndex { get; set; }

    public Project Project { set; }

    ObservableCollection<VideoViewModel> Videos { get; }
}