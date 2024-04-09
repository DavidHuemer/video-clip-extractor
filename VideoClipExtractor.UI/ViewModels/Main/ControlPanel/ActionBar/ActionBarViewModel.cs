using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;

[Singleton]
public class ActionBarViewModel : BaseViewModelContainer, IActionBarViewModel
{
    public ActionBarViewModel(IDependencyProvider provider) : base(provider)
    {
        VideoNavigationViewModel = ViewModelProvider.Get<IVideoNavigationViewModel>();
        TimelineExtractionBarViewModel = ViewModelProvider.Get<ITimelineExtractionBarViewModel>();
        TimelineNavigationViewModel = ViewModelProvider.Get<ITimelineNavigationViewModel>();
    }

    #region Properties

    public IVideoNavigationViewModel VideoNavigationViewModel { get; set; }
    public ITimelineExtractionBarViewModel TimelineExtractionBarViewModel { get; }
    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    public VideoViewModel? Video
    {
        set
        {
            VideoNavigationViewModel.Video = value;
            TimelineExtractionBarViewModel.Video = value;
        }
    }

    #endregion
}