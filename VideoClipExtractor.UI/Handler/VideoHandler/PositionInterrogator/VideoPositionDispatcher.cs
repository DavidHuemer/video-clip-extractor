using System.Windows.Threading;

namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;

public class VideoPositionDispatcher : IVideoPositionDispatcher
{
    private const int DispatchInterval = 12;

    private readonly DispatcherTimer _dispatcher;

    public VideoPositionDispatcher()
    {
        _dispatcher = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(DispatchInterval),
        };

        _dispatcher.Tick += (_, _) => PositionDispatched?.Invoke(this, EventArgs.Empty);
    }

    public void Start() => _dispatcher.Start();

    public void Stop() => _dispatcher.Stop();

    public event EventHandler? PositionDispatched;
}