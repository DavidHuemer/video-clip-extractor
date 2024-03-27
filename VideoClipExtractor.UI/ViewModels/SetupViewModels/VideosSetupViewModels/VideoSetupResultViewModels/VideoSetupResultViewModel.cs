using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupResultViewModels;

public class VideoSetupResultViewModel : BaseViewModel, IVideoSetupResultViewModel
{
    private readonly IVideoCrawler _videoCrawler;

    public VideoSetupResultViewModel(IDependencyProvider provider)
    {
        _videoCrawler = provider.GetDependency<IVideoCrawler>();
        _videoCrawler.VideosAdded += OnVideosAdded;
    }

    public event Action<List<SourceVideo>>? VideosAdded;

    public async Task LoadVideos()
    {
        await _videoCrawler.CrawlVideos();
        Crawled = true;
    }

    private void OnVideosAdded(List<SourceVideo> crawledVideos)
    {
        foreach (var video in crawledVideos)
        {
            CrawledVideos.Add(video);
        }
    }

    #region Properties

    public ObservableCollection<SourceVideo> CrawledVideos { get; } = [];

    private bool Crawled { get; set; }

    #endregion

    #region Commands

    public ICommand Finish => new RelayCommand<string>(DoFinish, _ => Crawled);

    private void DoFinish(string? obj) =>
        VideosAdded?.Invoke(CrawledVideos.ToList());

    #endregion
}