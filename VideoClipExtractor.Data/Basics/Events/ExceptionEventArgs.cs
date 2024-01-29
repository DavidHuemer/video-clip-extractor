namespace VideoClipExtractor.Data.Basics.Events;

public class ExceptionEventArgs(Exception exception) : EventArgs
{
    public Exception Exception { get; } = exception;
}