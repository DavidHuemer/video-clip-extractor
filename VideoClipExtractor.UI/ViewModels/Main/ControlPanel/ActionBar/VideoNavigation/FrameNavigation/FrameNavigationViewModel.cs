using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

[Singleton]
public class FrameNavigationViewModel(IDependencyProvider provider) : BaseViewModel, IFrameNavigationViewModel
{
    private readonly IVideoPositionService _videoPositionService = provider.GetDependency<IVideoPositionService>();


    public VideoPosition VideoPosition { get; set; } = new(TimeSpan.Zero, 25);
    public ICommand GoBackward => new RelayCommand<string>(DoGoBackward, _ => Video != null);

    public ICommand GoForward => new RelayCommand<string>(DoGoForward, _ => Video != null);

    public VideoViewModel? Video { get; set; }

    public VideoPosition1 VideoPosition1 { get; set; } = new(0);

    private void DoGoBackward(string? obj)
    {
        var targetPosition = VideoPosition1.Frame - 1;
        if (targetPosition < 0)
        {
            targetPosition = 0;
        }

        var targetVideoPosition = new VideoPosition1(targetPosition);
        _videoPositionService.RequestPositionChange(new VideoPosition(TimeSpan.Zero, 30));
    }

    private void DoGoForward(string? obj)
    {
        var targetPosition = VideoPosition1.Frame + 1;
        var targetVideoPosition = new VideoPosition1(targetPosition);
        _videoPositionService.RequestPositionChange(new VideoPosition(TimeSpan.Zero, 30));
    }
}