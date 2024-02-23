using System.ComponentModel;
using System.IO;
using BaseUI.Exceptions.Basics;
using JetBrains.Annotations;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoCaching;

[UsedImplicitly]
public class VideoCacheService : IVideoCacheService
{
    public VideoCacheService()
    {
        _backgroundWorker.WorkerReportsProgress = true;
        _backgroundWorker.DoWork += RunCacheVideos;
        _backgroundWorker.ProgressChanged += NotifyVideoCached;
    }

    #region Events

    public event EventHandler<VideoCachedEventArgs>? VideoCached;

    #endregion

    public void Setup(IVideoRepository repository, string cachingDirectory)
    {
        _cacheInformation = new VideoCacheInformation(repository, cachingDirectory);
    }

    public void CacheVideo(SourceVideo video)
    {
        if (_cacheInformation == null) throw new NotSetupException(nameof(VideoCacheService), nameof(Setup));

        lock (_lock)
        {
            _cachingQueue.Enqueue(video);
        }

        if (!_backgroundWorker.IsBusy) _backgroundWorker.RunWorkerAsync();
    }

    /// <summary>
    ///     Is called when a video has been cached
    ///     Invokes the <see cref="VideoCached" /> event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NotifyVideoCached(object? sender, ProgressChangedEventArgs e)
    {
        if (e.UserState is not CachedVideo cachedVideo) return;

        VideoCached?.Invoke(this, new VideoCachedEventArgs(cachedVideo));
    }

    /// <summary>
    ///     This method runs in the background and caches the videos.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RunCacheVideos(object? sender, DoWorkEventArgs e)
    {
        Console.WriteLine("Run cached videos");

        while (true)
        {
            SourceVideo? videoToCache;

            lock (_lock)
            {
                if (_cachingQueue.Count > 0)
                    videoToCache = _cachingQueue.Dequeue();
                else
                    // Queue is empty, exit the loop
                    return;
            }

            // Now cache the video
            StoreVideo(videoToCache);
        }
    }

    /// <summary>
    ///     Stores the given video in the local cache.
    /// </summary>
    /// <param name="sourceVideo">The <see cref="SourceVideo" /> that should be stored locally</param>
    private void StoreVideo(SourceVideo sourceVideo)
    {
        if (_cacheInformation == null) throw new NotSetupException(nameof(VideoCacheService), nameof(Setup));

        Console.WriteLine($"Caching video: {sourceVideo}");
        var cacheVideoPath = Path.Combine(_cacheInformation.LocalCachePath, sourceVideo.FullName);

        // Check if the file already exists
        if (IsVideoCached(cacheVideoPath))
        {
            Console.WriteLine($"Video already cached: {sourceVideo}");
            _backgroundWorker.ReportProgress(0, new CachedVideo(sourceVideo, cacheVideoPath));
            return;
        }

        _cacheInformation.Repository.CopyFileByPath(sourceVideo.Path, cacheVideoPath);
        Console.WriteLine($"Cached video: {sourceVideo}");

        _backgroundWorker.ReportProgress(0, new CachedVideo(sourceVideo, cacheVideoPath));
        //NotifyFileCached(sourceVideo, cacheVideoPath);
    }

    /// <summary>
    ///     Returns true if the video is cached
    /// </summary>
    /// <param name="localVideoPath">The path of the video for which is checked if it is already cached</param>
    /// <returns>Whether the video is already cached</returns>
    private static bool IsVideoCached(string localVideoPath)
    {
        return File.Exists(localVideoPath);
    }

    #region Private Fields

    private readonly BackgroundWorker _backgroundWorker = new();
    private readonly Queue<SourceVideo> _cachingQueue = new();

    private VideoCacheInformation? _cacheInformation;

    private readonly object _lock = new();

    #endregion
}