using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

[UsedImplicitly]
public class VideoNavigationViewModel : BaseViewModel, IVideoNavigationViewModel
{
    #region Properties

    public PlayStatus PlayStatus { get; set; } = PlayStatus.Paused;

    #endregion

    #region Commands

    public ICommand PlayPause => new RelayCommand<string>(DoPlayPause, _ => true);

    private void DoPlayPause(string? obj)
    {
        PlayStatus = PlayStatus == PlayStatus.Playing
            ? PlayStatus.Paused
            : PlayStatus.Playing;
    }

    #endregion
}