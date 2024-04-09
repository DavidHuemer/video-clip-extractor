using System.Windows.Input;
using BaseUI.Basics.DelayWrapper;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.PlayStatusManager;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

[Singleton]
public class VideoNavigationViewModel : BaseViewModelContainer, IVideoNavigationViewModel
{
    private readonly IDelayWrapper _delayWrapper;
    private readonly IPlayStatusManager _playStatusManager;

    public VideoNavigationViewModel(IDependencyProvider provider) : base(provider)
    {
        _playStatusManager = provider.GetDependency<IPlayStatusManager>();
        _delayWrapper = provider.GetDependency<IDelayWrapper>();
        FrameNavigationViewModel = ViewModelProvider.Get<IFrameNavigationViewModel>();

        _playStatusManager.PlayPause += (_, _) =>
        {
            PlayStatus = PlayStatus.Playing;
            PlayStatus = PlayStatus.Paused;
        };

        _playStatusManager.PlayStatusChanged += playStatus => { PlayStatus = playStatus; };
    }

    #region Properties

    public IFrameNavigationViewModel FrameNavigationViewModel { get; set; }

    public PlayStatus PlayStatus { get; set; } = PlayStatus.Paused;

    private VideoViewModel? _video;

    public VideoViewModel? Video
    {
        get => _video;
        set
        {
            _video = value;
            FrameNavigationViewModel.Video = value;
            if (value != null)
            {
                _delayWrapper.RunAfterDelay(500, () => { _playStatusManager.SetMainPlayStatus(PlayStatus.Playing); });
            }
        }
    }

    #endregion

    #region Commands

    public ICommand PlayPause => new RelayCommand<string>(DoPlayPause, _ => Video != null);

    private void DoPlayPause(string? obj)
    {
        if (_playStatusManager.MainPlayStatus == PlayStatus.Paused)
        {
            _playStatusManager.SetMainPlayStatus(PlayStatus.Playing);
        }
        else
        {
            _playStatusManager.SetMainPlayStatus(PlayStatus.Paused);
        }
    }

    #endregion
}