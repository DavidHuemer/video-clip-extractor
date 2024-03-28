using System.Windows.Input;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupSettingsViewModels;

public interface IVideoSetupSettingsViewModel : IBaseViewModel
{
    bool EnableSettings { get; }

    bool ReconsiderSkippedVideos { get; set; }

    bool ShowProgress { get; }

    bool ShowStatistics { get; set; }

    Project? Project { get; set; }

    ICommand LoadVideos { get; }

    event Action<VideoSetupSettings> LoadVideosRequested;

    void LoadingFinished();
}