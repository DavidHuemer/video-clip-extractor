using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Extensions;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.Managers.Timeline.SelectionManager;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

[UsedImplicitly]
[Singleton]
public class TimelineExtractionBarViewModel : BaseViewModel, ITimelineExtractionBarViewModel
{
    private readonly ITimelineExtractionSelectionManager _extractionSelectionManager;
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;


    public TimelineExtractionBarViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.Get<IVideoNavigationViewModel>();
        _extractionSelectionManager = provider.GetDependency<ITimelineExtractionSelectionManager>();
    }

    public ICommand AddVideoExtraction => new RelayCommand<string>(DoAddVideoExtraction, _ => true);

    public VideoViewModel? Video { get; set; }

    public ICommand AddImageExtraction => new RelayCommand<string>(DoAddImageExtraction, _ => true);

    private void DoAddImageExtraction(string? obj)
    {
        var pos = _videoNavigationViewModel.VideoPosition;
        var newImageExtraction = new ImageExtraction(pos);
        newImageExtraction.SetupSelection(HandleSelection);

        var comparison =
            new Comparison<ImageExtraction>((x, y) => x.Position.Frame.CompareTo(y.Position.Frame));

        Video?.ImageExtractions.InsertSorted(newImageExtraction, comparison);
        Console.WriteLine("Image extraction added!");
    }

    private void DoAddVideoExtraction(string? obj)
    {
        var begin = _videoNavigationViewModel.VideoPosition;
        var end = new VideoPosition(begin.Frame + 30);

        var newVideoExtraction = new VideoExtraction(begin, end);
        newVideoExtraction.SetupSelection(HandleSelection);

        Video?.VideoExtractions.Add(newVideoExtraction);
        Console.WriteLine("Video extraction added!");
    }

    private void HandleSelection(IExtractionViewModel extractionViewModel)
    {
        _extractionSelectionManager.Selected(extractionViewModel);
    }
}