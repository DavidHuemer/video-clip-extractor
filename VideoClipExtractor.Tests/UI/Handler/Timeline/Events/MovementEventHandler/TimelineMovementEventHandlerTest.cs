using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.Events.MovementEventHandler;

[TestFixture]
[TestOf(typeof(TimelineMovementEventHandler))]
public class TimelineMovementEventHandlerTest : BaseViewModelTest
{
    private ViewModelMock<ITimelineNavigationViewModel> _timelineNavigationViewModelMock = null!;
    private TimelineMovementEventHandler _timelineMovementEventHandler = null!;

    public override void Setup()
    {
        base.Setup();
        _timelineNavigationViewModelMock = ViewModelProviderMock.CreateViewModelMock<ITimelineNavigationViewModel>();
        _timelineMovementEventHandler = new TimelineMovementEventHandler(DependencyMock.Object);
    }
}