using System.ComponentModel;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public interface IVideoPlayerViewModel : INotifyPropertyChanged
{
    IControlPanelViewModel ControlPanelViewModel { get; }
}