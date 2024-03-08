using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

namespace VideoClipExtractor.UI.ViewModels.Extraction;

[UsedImplicitly]
public class ExtractionPanelViewModel : BaseViewModel, IExtractionPanelViewModel
{
    public ExtractionPanelViewModel(IDependencyProvider provider)
    {
        _viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExtractionNavigation = _viewModelProvider.Get<IExtractionNavigationViewModel>();
        ActiveViewModel = this;
    }

    public void SetupExtraction(IEnumerable<VideoViewModel> videos)
    {
        var extractingVideos = videos
            .Where(video => video.VideoStatus != VideoStatus.Unset);

        Videos = new ObservableCollection<VideoViewModel>(extractingVideos);
    }

    #region Properties

    private readonly IViewModelProvider _viewModelProvider;

    public ObservableCollection<VideoViewModel> Videos { get; private set; } = [];

    private bool CanExtract => Videos.Count > 0;

    public IBaseViewModel ActiveViewModel { get; set; }
    public IExtractionNavigationViewModel ExtractionNavigation { get; }

    public bool ExtractionFinished { get; private set; }

    #endregion

    #region Commands

    public ICommand ExtractCommand => new AsyncCommand(DoExtract, () => CanExtract);

    private async Task DoExtract()
    {
        var extractionVisualization = _viewModelProvider.Get<IExtractionVisualizationViewModel>();
        ActiveViewModel = extractionVisualization;
        await extractionVisualization.ExtractVideos(Videos);
        ExtractionFinished = true;
    }

    #endregion
}