namespace BaseUI.Handler;

public interface IProcessing<T>
{
    event Action<T> OnResultProcessed;
    event Action<Exception>? OnErrorOccurred;
}