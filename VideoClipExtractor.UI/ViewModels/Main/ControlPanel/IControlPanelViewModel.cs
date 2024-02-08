using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel;

public interface IControlPanelViewModel
{
    public IActionBarViewModel ActionBarViewModel { get; set; }
}