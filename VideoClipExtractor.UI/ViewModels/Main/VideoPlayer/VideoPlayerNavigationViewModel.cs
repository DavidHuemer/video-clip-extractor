using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Managers.VideoProviderManager;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

/// <summary>
///     Viewmodel for the navigation part of the video player.
///     Choosing next video, export, ...
/// </summary>
[UsedImplicitly]
public class VideoPlayerNavigationViewModel(IDependencyProvider provider)
    : BaseViewModel, IVideoPlayerNavigationViewModel
{
    #region Private Fields

    private readonly IVideoProviderManager _videoProviderManager = provider.GetDependency<IVideoProviderManager>();

    #endregion

    #region Commands

    public ICommand Skip => new RelayCommand<string>(DoSkip, _ => true);

    private void DoSkip(string? obj)
    {
        _videoProviderManager.Next();
    }

    #endregion
}