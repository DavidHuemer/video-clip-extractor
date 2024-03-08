using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Dialogs;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;
using VideoClipExtractor.Data.Basics.Events;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

public class VideosSetupWindowViewModel : WindowViewModel
{
    #region Private Fields

    private readonly IDependencyProvider _provider;

    #endregion

    public VideosSetupWindowViewModel(IDependencyProvider provider, bool isInitializing) : base(provider)
    {
        EnableSettings = isInitializing;
        _provider = provider;

        var projectManager = provider.GetDependency<IProjectManager>();
        var project = projectManager.Project;

        if (project == null)
        {
            var dialogService = provider.GetDependency<IDialogService>();
            dialogService.Show(new Exception("No project is opened."));
        }

        Project = project!;
    }

    #region Properties

    public bool EnableSettings { get; }

    public bool KeepSkippedVideos { get; set; }

    public bool IsCrawling { get; set; }
    public ObservableCollection<SourceVideo> CrawledVideos { get; } = [];
    public int VideosCount { get; private set; }

    private bool HasCrawled { get; set; }

    private bool CanFinish => HasCrawled && !IsCrawling;

    [DoNotNotify] private Project? Project { get; }

    #endregion


    #region Commands

    public ICommand GetVideos => new RelayCommand<string>(DoGetVideos, _ => !IsCrawling);

    private void DoGetVideos(string? obj)
    {
        var crawler = _provider.GetDependency<IVideoCrawler>();
        crawler.VideoAdded += OnVideoAdded;
        crawler.CrawlerFinished += OnCrawlerFinished;
        crawler.ExceptionThrown += OnExceptionThrown;
        crawler.CrawlVideos();
        IsCrawling = true;
    }

    private void OnExceptionThrown(object? sender, ExceptionEventArgs e)
    {
        _provider.GetDependency<IDialogService>().Show(e.Exception);
    }

    public ICommand Cancel => new RelayCommand<string>(DoCancel, _ => !IsCrawling);

    private void DoCancel(string? obj)
    {
        CloseWindow();
    }

    public ICommand Finish => new RelayCommand<string>(DoFinish, _ => CanFinish);

    private void DoFinish(string? obj)
    {
        if (Project == null) return;

        Project.Videos = CrawledVideos.ToList();

        var projectManager = _provider.GetDependency<IProjectManager>();
        projectManager.StoreProject();

        CloseWindow();
    }

    #endregion

    #region Crawler Events

    private void OnVideoAdded(object? sender, SourceVideoEventArgs e)
    {
        CrawledVideos.Add(e.SourceVideo);
        VideosCount++;
    }

    private void OnCrawlerFinished(object? sender, EventArgs e)
    {
        IsCrawling = false;
        HasCrawled = true;
    }

    #endregion
}