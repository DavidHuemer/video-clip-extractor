using System.Windows.Input;
using BaseUI.Basics.DelayWrapper;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

[Singleton]
public class VideoNavigationViewModel : BaseViewModelContainer, IVideoNavigationViewModel
{
    private readonly IDelayWrapper _delayWrapper;

    public VideoNavigationViewModel(IDependencyProvider provider) : base(provider)
    {
        _delayWrapper = provider.GetDependency<IDelayWrapper>();
        FrameNavigationViewModel = ViewModelProvider.Get<IFrameNavigationViewModel>();
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
                _delayWrapper.RunAfterDelay(500, () => PlayStatus = PlayStatus.Playing);
            }
        }
    }

    #endregion

    #region Commands

    public ICommand PlayPause => new RelayCommand<string>(DoPlayPause, _ => Video != null);

    private void DoPlayPause(string? obj)
    {
        PlayStatus = PlayStatus == PlayStatus.Playing
            ? PlayStatus.Paused
            : PlayStatus.Playing;
    }

    #endregion
}