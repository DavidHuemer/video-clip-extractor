namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;

/// <summary>
/// Responsible for dispatching the current position of a video.
/// </summary>
public interface IVideoPositionDispatcher
{
    /// <summary>
    /// Starts the dispatcher
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the dispatcher
    /// </summary>
    void Stop();

    event EventHandler? PositionDispatched;
}