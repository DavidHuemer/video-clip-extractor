using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.Timeout;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Managers.PlayStatusManager;

[Singleton]
public class PlayStatusManager : IPlayStatusManager
{
    private readonly ITimeoutService _timeoutService;

    public PlayStatusManager(IDependencyProvider provider)
    {
        _timeoutService = provider.GetDependency<ITimeoutService>();
        _timeoutService.EndTimeout += OnTimeoutEnded;
    }

    public event Action<PlayStatus>? PlayStatusChanged;
    public event EventHandler? PlayPause;

    public void SetMainPlayStatus(PlayStatus playStatus)
    {
        MainPlayStatus = playStatus;
        PlayStatusChanged?.Invoke(MainPlayStatus);

        if (playStatus == PlayStatus.Paused)
        {
            _timeoutService.CancelTimeout();
        }
    }

    public void VideoPositionChanged()
    {
        if (MainPlayStatus == PlayStatus.Paused)
        {
            PlayPause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            PlayStatusChanged?.Invoke(PlayStatus.Paused);
            _timeoutService.RequestTimeout();
        }
    }

    public PlayStatus MainPlayStatus { get; private set; } = PlayStatus.Paused;

    private void OnTimeoutEnded(object? sender, EventArgs e)
    {
        PlayStatusChanged?.Invoke(PlayStatus.Playing);
    }
}