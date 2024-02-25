using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

[UsedImplicitly]
public class ExtractionRunnerViewModel : IExtractionRunnerViewModel
{
    private IExtractionService _extractionService;

    public VideoViewModel? CurrentVideo { get; set; }

    public ExtractionRunnerViewModel(IDependencyProvider provider)
    {
        _extractionService = provider.GetDependency<IExtractionService>();
    }

    public async Task ExtractVideos(IEnumerable<VideoViewModel> videos)
    {
        var videosList = videos.ToList();

        for (int i = 0; i < videosList.Count; i++)
        {
            var video = videosList[i];
            CurrentVideo = video;

            // TODO: Extract video by calling the extraction service
            await _extractionService.Extract(video);
        }
    }
}