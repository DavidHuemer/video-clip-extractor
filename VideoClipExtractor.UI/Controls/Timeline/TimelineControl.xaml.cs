using System.Windows;
using System.Windows.Controls;
using BaseUI.Basics.FrameworkElementWrapper;
using VideoClipExtractor.UI.Handler.Timeline.Events;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.UI.Controls.Timeline;

public partial class TimelineControl : UserControl
{
    public TimelineControl()
    {
        InitializeComponent();
    }

    private TimelineControlViewModel? ViewModel => DataContext as TimelineControlViewModel;

    private void TimelineControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (ViewModel == null) return;
        var eventCatcher = new TimelineEventHandler(new FrameworkElementWrapper(OuterCanvas), ViewModel);
    }
}