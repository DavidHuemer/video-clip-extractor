using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;

[Transient]
public class VideosSetupWindowViewModel : WindowViewModel, IVideosSetupWindowViewModel
{
    public VideosSetupWindowViewModel(IDependencyProvider provider) : base(provider)
    {
        VideosSetupViewModel = ViewModelProvider.Get<IVideosSetupViewModel>();
        VideosSetupViewModel.Finish += OnFinished;
    }

    public IVideosSetupViewModel VideosSetupViewModel { get; }

    private void OnFinished(object? sender, EventArgs e) => CloseWindow();
}