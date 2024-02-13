using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Events;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;

[UsedImplicitly]
public class TimelineMarkerEventHandler : ITimelineMarkerEventHandler
{
    private readonly ITimelineFrameWidthHandler _timelineFrameWidthHandler;
    private readonly ITimelineNavigationViewModel _timelineNavigationViewModel;
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;
    private readonly IVideoPositionService _videoPositionService;

    private IFrameworkElement? _timelineControl;
    private bool isMoving;

    public TimelineMarkerEventHandler(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _timelineNavigationViewModel = viewModelProvider.GetViewModel<ITimelineNavigationViewModel>();
        _videoNavigationViewModel = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();

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
        isMoving = true;
        UpdateMarkerPosition(position);
    }

    public void StopMarkerMovement()
    {
        isMoving = false;
    }

    private void OnTimelineMouseMove(object? sender, MouseEventArgsWrapper e)
    {
        if (!isMoving || _timelineControl == null) return;

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

        _videoPositionService.RequestPositionChange(new VideoPosition(frame));
    }
}