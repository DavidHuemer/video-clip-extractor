using System.Collections.ObjectModel;
using BaseUI.Services.Provider.Attributes;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;

[UsedImplicitly]
[Singleton]
public class ExtractionNavigationViewModel : BaseViewModel, IExtractionNavigationViewModel
{
    private VideoViewModel? _currentVideo;

    public VideoViewModel? CurrentVideo
    {
        get => _currentVideo;
        set
        {
            _currentVideo = value;
            OnPropertyChanged();

            Extractions = value != null
                ? new ObservableCollection<IExtraction>(value.GetExtractions())
                : [];

            ShowDetails = value != null;
        }
    }

    public ObservableCollection<IExtraction> Extractions { get; private set; } = [];
    public bool ShowDetails { get; set; }
}