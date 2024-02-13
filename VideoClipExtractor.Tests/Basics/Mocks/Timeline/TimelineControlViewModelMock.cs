using Moq;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.Tests.Basics.Mocks.Timeline;

/// <summary>
/// Mock that can be used when <see cref="ITimelineControlViewModel"/> is needed
/// </summary>
public class TimelineControlViewModelMock
{
    public TimelineControlViewModelMock()
    {
        ViewModelMock = new Mock<ITimelineControlViewModel>();
        TimelineNavigationViewModel = new TimelineNavigationViewModel();
        ViewModelMock.SetupGet(x => x.TimelineNavigationViewModel)
            .Returns(TimelineNavigationViewModel);
    }

    public Mock<ITimelineControlViewModel> ViewModelMock { get; }

    public TimelineNavigationViewModel TimelineNavigationViewModel { get; }

    public ITimelineControlViewModel Object => ViewModelMock.Object;
}