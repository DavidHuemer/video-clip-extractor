using System.Collections.Concurrent;

namespace BaseUI.Handler;

public abstract class AsyncQueueProcessor<TInput, TOutput> : IProcessing<TOutput>
{
    /// <summary>
    /// The cancellation token source for the processing task
    /// </summary>
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// The queue of items to process
    /// </summary>
    private readonly ConcurrentQueue<TInput> _inputQueue = new();

    private bool _isProcessing;

    /// <summary>
    /// Invoked when a result has been processed
    /// </summary>
    public event Action<TOutput>? OnResultProcessed;

    /// <summary>
    /// Invoked when an error occurred during processing
    /// </summary>
    public event Action<Exception>? OnErrorOccurred;

    protected abstract TOutput Process(TInput sourcePath);

    private async Task ProcessQueueAsync()
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            if (_inputQueue.TryDequeue(out var item))
            {
                try
                {
                    var result = await Task.Run(() => Process(item));
                    OnResultProcessed?.Invoke(result);
                }
                catch (Exception ex)
                {
                    OnErrorOccurred?.Invoke(ex);
                }
            }
            else
            {
                _isProcessing = false;
                return;
            }
        }
    }

    protected void Enqueue(TInput item)
    {
        _inputQueue.Enqueue(item);
        if (_isProcessing) return;
        _isProcessing = true;
        _ = ProcessQueueAsync();
    }

    public async Task StopProcessingAsync()
    {
        await _cancellationTokenSource.CancelAsync();
        _isProcessing = false;
        await Task.CompletedTask;
    }
}