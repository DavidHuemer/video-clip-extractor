using System.ComponentModel;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public interface IVideoPlayerViewModel : INotifyPropertyChanged
{
    IDependencyProvider Provider { get; }
    IControlPanelViewModel ControlPanelViewModel { get; }
}