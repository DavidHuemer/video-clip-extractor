using System.Collections.ObjectModel;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

[UsedImplicitly]
public class ExtractionRunnerViewModel : BaseViewModel, IExtractionRunnerViewModel
{
    private readonly IExtractionService _extractionService;

    public ExtractionRunnerViewModel(IDependencyProvider provider)
    {
        _extractionService = provider.GetDependency<IExtractionService>();

        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExtractionNavigation = viewModelProvider.Get<IExtractionNavigationViewModel>();
    }

    public IExtractionNavigationViewModel ExtractionNavigation { get; set; }

    public ObservableCollection<IExtraction> Extractions { get; set; } = [];

    public async Task<ExtractionProcessResult> ExtractVideos(IEnumerable<VideoViewModel> videos)
    {
        try
        {
            var videosList = videos.ToList();
            var videoExtractionResults = await RunVideosExtraction(videosList);
            return new ExtractionProcessResult(videoExtractionResults.ToList());
        }
        catch (Exception e)
        {
            return new ExtractionProcessResult(e);
        }
    }

    private async Task<IEnumerable<VideoExtractionResult>> RunVideosExtraction(List<VideoViewModel> videosList)
    {
        var videoExtractionResults = new List<VideoExtractionResult>();

        foreach (var video in videosList)
        {
            ExtractionNavigation.CurrentVideo = video;
            video.IsExtracting = true;

            var result = await ExtractVideo(video);
            video.ExtractionResult = result;
            videoExtractionResults.Add(result);

            video.IsExtracting = false;
        }

        return videoExtractionResults;
    }

    private async Task<VideoExtractionResult> ExtractVideo(VideoViewModel video)
    {
        try
        {
            var extractions = video.GetExtractions();
            Extractions = new ObservableCollection<IExtraction>(extractions);

            return await _extractionService.Extract(video);
        }
        catch (Exception e)
        {
            return new VideoExtractionResult(e);
        }
    }
}