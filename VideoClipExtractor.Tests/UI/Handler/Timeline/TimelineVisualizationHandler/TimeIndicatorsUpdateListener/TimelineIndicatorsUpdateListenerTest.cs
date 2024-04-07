using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicatorsUpdateListener;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicatorsUpdateListener;

[TestFixture]
[TestOf(typeof(TimelineIndicatorsUpdateListener))]
public class TimelineIndicatorsUpdateListenerTest
{
    [SetUp]
    public void Setup()
    {
        _timelineControlViewModelMock = new ViewModelMock<ITimelineControlViewModel>();
        _timelineIndicatorsUpdateListener = new TimelineIndicatorsUpdateListener();
    }

    private ViewModelMock<ITimelineControlViewModel> _timelineControlViewModelMock = null!;
    private TimelineIndicatorsUpdateListener _timelineIndicatorsUpdateListener = null!;


    [Test]
    [TestCase(nameof(TimelineControlViewModel.TimelineNavigationViewModel.ZoomLevel))]
    [TestCase(nameof(TimelineControlViewModel.TimelineNavigationViewModel.MovementPosition))]
    [TestCase(nameof(TimelineControlViewModel.TimelineNavigationViewModel.TimelineControlWidth))]
    public void TimelineIndicatorsUpdateRequestedIsInvoked(string propertyName)
    {
        var eventRaised = false;
        _timelineIndicatorsUpdateListener.TimelineIndicatorsUpdateRequested += (_, _) => eventRaised = true;
        _timelineIndicatorsUpdateListener.Setup(_timelineControlViewModelMock.Object);

        _timelineControlViewModelMock.RaisePropertyChanged(propertyName);
        Assert.IsTrue(eventRaised);
    }
}