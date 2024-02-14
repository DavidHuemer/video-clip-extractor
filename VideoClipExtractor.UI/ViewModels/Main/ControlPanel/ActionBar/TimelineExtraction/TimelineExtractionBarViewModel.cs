using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Extensions;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;

[UsedImplicitly]
public class TimelineExtractionBarViewModel : BaseViewModel, ITimelineExtractionBarViewModel
{
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;

    public TimelineExtractionBarViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();
    }

    public VideoViewModel? Video { get; set; }

    public ICommand AddImageExtraction => new RelayCommand<string>(DoAddImageExtraction, _ => true);

    private void DoAddImageExtraction(string? obj)
    {
        var pos = _videoNavigationViewModel.VideoPosition;
        var newImageExtraction = new ImageExtractionViewModel(pos);

        var comparison =
            new Comparison<ImageExtractionViewModel>((x, y) => x.VideoPosition.Frame.CompareTo(y.VideoPosition.Frame));

        Video?.ImageExtractions.InsertSorted(newImageExtraction, comparison);
        Console.WriteLine("Image extraction added!");
    }
}