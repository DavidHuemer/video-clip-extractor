using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Basics.MouseCursorHandler;
using BaseUI.Events;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Managers.Timeline.SelectionManager;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.ExtensionMovement;

[UsedImplicitly]
public class ExtractionMovementEventHandler : IExtractionMovementEventHandler
{
    private readonly IMouseCursorHandler _mouseCursorHandler;
    private readonly ITimelineExtractionSelectionManager _timelineExtractionSelection;
    private readonly ITimelineNavigationViewModel _timelineNavigationViewModel;
    private readonly ITimelinePositionHandler _timelinePositionHandler;
    private double _startExtractionElementPosition;
    private double _startMousePosition;

    private VideoPosition? _startVideoPosition;

    private IFrameworkElement? _timelineControl;

    public ExtractionMovementEventHandler(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _timelineNavigationViewModel = viewModelProvider.Get<ITimelineNavigationViewModel>();

        _timelineExtractionSelection = provider.GetDependency<ITimelineExtractionSelectionManager>();
        _timelineExtractionSelection.SelectedExtractionChanged += OnSelectedExtractionChanged;

        _mouseCursorHandler = provider.GetDependency<IMouseCursorHandler>();
        _timelinePositionHandler = provider.GetDependency<ITimelinePositionHandler>();
    }

    public void Setup(IFrameworkElement timelineControl)
    {
        _timelineControl = timelineControl;
        timelineControl.MouseMove += OnTimelineMouseMove;
    }

    private void OnSelectedExtractionChanged(object? sender, SelectedExtractionChangedEventArgs e)
    {
        if (e.ExtractionViewModel == null || _timelineControl == null) return;

        _startVideoPosition = e.ExtractionViewModel.Position;
        _startExtractionElementPosition =
            _timelinePositionHandler.GetPositionAtFrame(_startVideoPosition.Frame,
                _timelineNavigationViewModel.ZoomLevel);

        _startMousePosition = _mouseCursorHandler.GetMousePosition(_timelineControl).X;
    }

    private void OnTimelineMouseMove(object? sender, MouseEventArgsWrapper e)
    {
        if (_timelineNavigationViewModel.MovementState != MovementState.Extraction) return;

        var extraction = _timelineExtractionSelection.SelectedExtractionViewModel;

        if (extraction == null || _timelineControl == null) return;

        var position = e.GetPosition(_timelineControl);

        var diff = position.X - _startMousePosition;

        var newElementPos = _startExtractionElementPosition + diff;

        var newFrame =
            _timelinePositionHandler.GetFrameAtPosition(newElementPos, _timelineNavigationViewModel.ZoomLevel);

        var newFrameInt = (int)newFrame;

        extraction.Position = new VideoPosition(newFrameInt);
    }
}