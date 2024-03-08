using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionResult;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

[UsedImplicitly]
public class ExtractionVisualizationViewModel : BaseViewModel, IExtractionVisualizationViewModel
{
    private readonly IViewModelProvider _viewModelProvider;

    public ExtractionVisualizationViewModel(IDependencyProvider provider)
    {
        _viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ActiveViewModel = this;
    }

    public bool ExtractionFinished { get; set; }

    public IBaseViewModel ActiveViewModel { get; private set; }

    public async Task ExtractVideos(IEnumerable<VideoViewModel> videos)
    {
        var extractionRunner = _viewModelProvider.Get<IExtractionRunnerViewModel>();
        ActiveViewModel = extractionRunner;

        var extractionResult = await extractionRunner.ExtractVideos(videos);
        ExtractionFinished = true;

        var extractionResultViewModel = _viewModelProvider.Get<IExtractionResultViewModel>();
        ActiveViewModel = extractionResultViewModel;
        extractionResultViewModel.Result = extractionResult;
    }
}