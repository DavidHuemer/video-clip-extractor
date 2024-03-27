using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupResultViewModels;

public interface IVideoSetupResultViewModel : IBaseViewModel
{
    ObservableCollection<SourceVideo> CrawledVideos { get; }

    ICommand Finish { get; }
    event Action<List<SourceVideo>> VideosAdded;
    Task LoadVideos();
}