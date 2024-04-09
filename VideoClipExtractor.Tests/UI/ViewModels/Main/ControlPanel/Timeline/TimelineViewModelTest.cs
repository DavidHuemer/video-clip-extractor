using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.ControlPanel.Timeline;

[TestFixture]
[TestOf(typeof(TimelineViewModel))]
public class TimelineViewModelTest : BaseViewModelTest
{
    private ViewModelMock<ITimelineControlViewModel> _timelineControlViewModelMock = null!;
    private TimelineViewModel _viewModel;

    public override void Setup()
    {
        base.Setup();
        _timelineControlViewModelMock = ViewModelProviderMock.CreateViewModelMock<ITimelineControlViewModel>();
        _viewModel = new TimelineViewModel(DependencyMock.Object);
    }

    [Test]
    public void VideoSetsTimelineControlViewModelVideo()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        _viewModel.Video = video;
        _timelineControlViewModelMock.VerifySet(x => x.VideoViewModel = video);
    }
}