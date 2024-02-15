using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Extensions;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Managers.TimelineManager.SelectionManager;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

[UsedImplicitly]
public class TimelineExtractionBarViewModel : BaseViewModel, ITimelineExtractionBarViewModel
{
    private readonly ITimelineExtractionSelectionManager _extractionSelectionManager;
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;


    public TimelineExtractionBarViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();
        _extractionSelectionManager = provider.GetDependency<ITimelineExtractionSelectionManager>();
    }

    public ICommand AddVideoExtraction => new RelayCommand<string>(DoAddVideoExtraction, _ => true);

    public VideoViewModel? Video { get; set; }

    public ICommand AddImageExtraction => new RelayCommand<string>(DoAddImageExtraction, _ => true);

    private void DoAddImageExtraction(string? obj)
    {
        var pos = _videoNavigationViewModel.VideoPosition;
        var newImageExtraction = new ImageExtractionViewModel(pos);
        newImageExtraction.SetupSelection(HandleSelection);

        var comparison =
            new Comparison<ImageExtractionViewModel>((x, y) => x.VideoPosition.Frame.CompareTo(y.VideoPosition.Frame));

        Video?.ImageExtractions.InsertSorted(newImageExtraction, comparison);
        Console.WriteLine("Image extraction added!");
    }

    private void DoAddVideoExtraction(string? obj)
    {
        var begin = _videoNavigationViewModel.VideoPosition;
        var end = new VideoPosition(begin.Frame + 30);

        var newVideoExtraction = new VideoExtractionViewModel(begin, end);
        newVideoExtraction.SetupSelection(HandleSelection);

        Video?.VideoExtractions.Add(newVideoExtraction);
        Console.WriteLine("Video extraction added!");
    }

    private void HandleSelection(IExtractionViewModel extractionViewModel)
    {
        _extractionSelectionManager.Selected(extractionViewModel);
    }
}