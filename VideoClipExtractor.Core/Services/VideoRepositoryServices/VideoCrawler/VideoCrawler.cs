using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Data.Basics.Events;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;

[UsedImplicitly]
public class VideoCrawler(IDependencyProvider provider) : IVideoCrawler
{
    #region Private Fields

    private readonly BackgroundWorker _worker = new();

    #endregion

    public void CrawlVideos()
    {
        _worker.DoWork += (_, _) => { RunCrawler(); };
        _worker.RunWorkerCompleted += OnWorkerCompleted;
        _worker.WorkerReportsProgress = true;
        _worker.ProgressChanged += OnProgressChanged;
        _worker.RunWorkerAsync();
    }

    private void RunCrawler()
    {
        try
        {
            var repo = provider.GetDependency<IVideoRepositoryManager>().VideoRepository;
            if (repo == null) return;
            var files = repo.GetFiles();

            foreach (var file in files) _worker.ReportProgress(0, file);
        }
        catch (Exception e)
        {
            Application.Current.Dispatcher?.Invoke(
                () => { ExceptionThrown?.Invoke(this, new ExceptionEventArgs(e)); },
                DispatcherPriority.Normal);
        }
    }

    private void OnWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        CrawlerFinished?.Invoke(this, EventArgs.Empty);
    }

    private void OnProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        if (e.UserState is not SourceVideo file) return;

        VideoAdded?.Invoke(this, new SourceVideoEventArgs(file));
    }

    #region Events

    public event EventHandler<SourceVideoEventArgs>? VideoAdded;

    public event EventHandler? CrawlerFinished;

    public event EventHandler<ExceptionEventArgs>? ExceptionThrown;

    #endregion
}