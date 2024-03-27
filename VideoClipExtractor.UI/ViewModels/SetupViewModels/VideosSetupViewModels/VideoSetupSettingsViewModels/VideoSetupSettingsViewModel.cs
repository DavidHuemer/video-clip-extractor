using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupSettingsViewModels;

public class VideoSetupSettingsViewModel : BaseViewModel, IVideoSetupSettingsViewModel
{
    private Project? _project;

    [DoNotNotify] private bool IsLoading { get; set; }

    private bool CanLoadVideos => Project != null && !IsLoading;
    public bool EnableSettings { get; private set; }

    public bool ReconsiderSkippedVideos { get; set; }
    public event Action<VideoSetupSettings>? LoadVideosRequested;

    public Project? Project
    {
        get => _project;
        set
        {
            _project = value;
            EnableSettings = value is { Videos.Count: > 0 };
        }
    }

    #region Commands

    public ICommand LoadVideos => new RelayCommand<string>(DoLoadVideos, _ => CanLoadVideos);

    private void DoLoadVideos(string? obj)
    {
        var settings = new VideoSetupSettings
        {
            ReconsiderSkippedVideos = ReconsiderSkippedVideos,
        };

        IsLoading = true;
        LoadVideosRequested?.Invoke(settings);
    }

    #endregion
}