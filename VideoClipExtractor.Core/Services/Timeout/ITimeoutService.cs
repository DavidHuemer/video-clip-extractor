namespace VideoClipExtractor.Core.Services.Timeout;

public interface ITimeoutService
{
    event EventHandler EndTimeout;

    void RequestTimeout();

    void CancelTimeout();
}