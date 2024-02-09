﻿using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

[UsedImplicitly]
public class VideoNavigationViewModel : BaseViewModel, IVideoNavigationViewModel
{
    #region Properties

    public PlayStatus PlayStatus { get; set; } = PlayStatus.Paused;

    private VideoViewModel? _video;

    public VideoViewModel? Video
    {
        get => _video;
        set
        {
            _video = value;
            if (value != null)
            {
                PlayStatus = PlayStatus.Playing;
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