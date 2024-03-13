using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

public interface IWelcomeViewModel : IBaseViewModel
{
    event EventHandler? NewProjectRequested;
}