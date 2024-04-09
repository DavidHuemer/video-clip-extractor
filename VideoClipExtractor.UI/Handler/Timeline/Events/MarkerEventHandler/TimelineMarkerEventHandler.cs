using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Events;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;

[Singleton]
public class TimelineMarkerEventHandler : ITimelineMarkerEventHandler
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler;
    private readonly ITimelineNavigationViewModel _timelineNavigationViewModel;
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;
    private readonly IVideoPositionService _videoPositionService;
    private bool _isMoving;

    private IFrameworkElement? _timelineControl;

    public TimelineMarkerEventHandler(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _timelineNavigationViewModel = viewModelProvider.Get<ITimelineNavigationViewModel>();
        _videoNavigationViewModel = viewModelProvider.Get<IVideoNavigationViewModel>();

        _videoPositionService = provider.GetDependency<IVideoPositionService>();
        _timelineFrameWidthHandler = provider.GetDependency<ITimelineFrameWidthHandler>();
    }

    public void Setup(IFrameworkElement timelineControl)
    {
        _timelineControl = timelineControl;
        timelineControl.MouseMove += OnTimelineMouseMove;
    }

    public void StartMarkerMovement(Point position)
    {
        _isMoving = true;
        UpdateMarkerPosition(position);
    }

    public void StopMarkerMovement()
    {
        _isMoving = false;
    }

    private void OnTimelineMouseMove(object? sender, MouseEventArgsWrapper e)
    {
        if (!_isMoving || _timelineControl == null) return;

        if (_videoNavigationViewModel.PlayStatus == PlayStatus.Playing)
            _videoNavigationViewModel.PlayStatus = PlayStatus.Paused;

        _videoNavigationViewModel.PlayStatus = PlayStatus.Playing;
        _videoNavigationViewModel.PlayStatus = PlayStatus.Paused;

        var position = e.GetPosition(_timelineControl);
        UpdateMarkerPosition(position);
    }

    private void UpdateMarkerPosition(Point position)
    {
        var movementPos = position.X - 200 + _timelineNavigationViewModel.MovementPosition;
        if (movementPos < 0) movementPos = 0;

        SetFrameByPosition(movementPos);
    }

    private void SetFrameByPosition(double xPos)
    {
        var frameWidth = _timelineFrameWidthHandler.GetFrameWidth(_timelineNavigationViewModel.ZoomLevel);
        var frame = (int)Math.Round(xPos / frameWidth);

        var frameRate = _videoNavigationViewModel.Video?.VideoInfo.FrameRate ?? 50;
        _videoPositionService.RequestPositionChange(new VideoPosition(frame, frameRate));
    }
}