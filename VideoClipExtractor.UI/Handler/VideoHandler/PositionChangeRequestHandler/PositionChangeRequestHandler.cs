using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Core.Managers.PlayStatusManager;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionChangeRequestHandler;

[Transient]
public class PositionChangeRequestHandler : IPositionChangeRequestHandler
{
    private readonly IFrameNavigationViewModel _frameNavigationViewModel;
    private readonly IPlayStatusManager _playStatusManager;
    private readonly IVideoPositionService _videoPositionService;

    private IVideoPlayer? _videoPlayer;

    public PositionChangeRequestHandler(IDependencyProvider provider)
    {
        _playStatusManager = provider.GetDependency<IPlayStatusManager>();
        _videoPositionService = provider.GetDependency<IVideoPositionService>();

        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _frameNavigationViewModel = viewModelProvider.Get<IFrameNavigationViewModel>();
    }

    public void Setup(IVideoPlayer videoPlayer)
    {
        _videoPlayer = videoPlayer;
        _videoPositionService.PositionChangeRequested += OnPositionChangeRequested;
    }

    private void OnPositionChangeRequested(VideoPosition position)
    {
        _playStatusManager.VideoPositionChanged();
        _videoPlayer!.Position = position.Time;
        _frameNavigationViewModel.VideoPosition = position;
    }
}