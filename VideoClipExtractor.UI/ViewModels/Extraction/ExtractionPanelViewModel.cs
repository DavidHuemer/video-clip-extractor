using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionVisualization;

namespace VideoClipExtractor.UI.ViewModels.Extraction;

[UsedImplicitly]
public class ExtractionPanelViewModel : BaseViewModel, IExtractionPanelViewModel
{
    #region Properties

    private bool CanExtract => Videos.Count > 0;

    public IExtractionVisualizationViewModel ExtractionVisualization { get; }

    public bool ShowVisualization { get; set; }
    

    public bool ExtractionFinished { get; set; }

    #endregion

    public ExtractionPanelViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExtractionVisualization = viewModelProvider.Get<IExtractionVisualizationViewModel>();
    }

    public void SetupExtraction(IEnumerable<VideoViewModel> videos)
    {
        var extractingVideos = videos
            .Where(video => video.VideoStatus == VideoStatus.ReadyForExport);

        Videos = new ObservableCollection<VideoViewModel>(extractingVideos);
    }

    public ObservableCollection<VideoViewModel> Videos { get; private set; } = [];

    #region Commands

    public ICommand ExtractCommand => new AsyncCommand(DoExtract, () => CanExtract);

    private async Task DoExtract()
    {
        ShowVisualization = true;
        await ExtractionVisualization.ExtractVideos(Videos);
        ExtractionFinished = true;
    }

    #endregion
}