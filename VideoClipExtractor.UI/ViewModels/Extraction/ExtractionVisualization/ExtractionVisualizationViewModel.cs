using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

[UsedImplicitly]
public class ExtractionVisualizationViewModel : IExtractionVisualizationViewModel
{
    public ExtractionVisualizationViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExtractionRunner = viewModelProvider.Get<IExtractionRunnerViewModel>();
    }

    public bool ExtractionFinished { get; set; }

    public IExtractionRunnerViewModel ExtractionRunner { get; set; }

    public async Task ExtractVideos(IEnumerable<VideoViewModel> videos)
    {
        //await ExtractionRunner.ExtractVideos(videos);
    }
}