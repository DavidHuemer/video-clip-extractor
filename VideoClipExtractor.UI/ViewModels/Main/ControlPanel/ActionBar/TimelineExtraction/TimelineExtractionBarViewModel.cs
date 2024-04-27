using System.Windows.Input;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

[Singleton]
public class TimelineExtractionBarViewModel : BaseViewModelContainer, ITimelineExtractionBarViewModel
{
    private readonly IExtractionFactory _extractionFactory;
    private readonly ITimelineExtractionSelectionManager _extractionSelectionManager;
    private readonly IFrameNavigationViewModel _frameNavigationViewModel;

    private VideoViewModel? _video;


    public TimelineExtractionBarViewModel(IDependencyProvider provider) : base(provider)
    {
        _extractionSelectionManager = provider.GetDependency<ITimelineExtractionSelectionManager>();
        _frameNavigationViewModel = ViewModelProvider.Get<IFrameNavigationViewModel>();
        _extractionFactory = provider.GetDependency<IExtractionFactory>();
    }

    public ICommand AddVideoExtraction => new RelayCommand<string>(DoAddVideoExtraction, _ => Video != null);

    public VideoViewModel? Video
    {
        get => _video;
        set
        {
            _video = value;
            OnPropertyChanged();

            if (_video == null) return;

            foreach (var imageExtraction in _video.ImageExtractions)
            {
                imageExtraction.SetupSelection(HandleSelection);
            }

            foreach (var videoExtraction in _video.VideoExtractions)
            {
                videoExtraction.SetupSelection(HandleSelection);
            }
        }
    }

    public ICommand AddImageExtraction => new RelayCommand<string>(DoAddImageExtraction, _ => Video != null);

    private void DoAddImageExtraction(string? obj)
    {
        var pos = _frameNavigationViewModel.VideoPosition;
        var newImageExtraction = _extractionFactory.GetImageExtraction(pos);
        newImageExtraction.SetupSelection(HandleSelection);

        var comparison =
            new Comparison<IImageExtraction>((x, y) => x.Position.Frame.CompareTo(y.Position.Frame));

        Video?.ImageExtractions.InsertSorted(newImageExtraction, comparison);
    }

    private void DoAddVideoExtraction(string? obj)
    {
        var begin = _frameNavigationViewModel.VideoPosition;

        var newVideoExtraction = _extractionFactory.GetVideoExtraction(begin, Video!);
        newVideoExtraction.SetupSelection(HandleSelection);

        Video?.VideoExtractions.Add(newVideoExtraction);
    }

    private void HandleSelection(IExtractionViewModel extractionViewModel)
    {
        _extractionSelectionManager.Selected(extractionViewModel);
    }
}