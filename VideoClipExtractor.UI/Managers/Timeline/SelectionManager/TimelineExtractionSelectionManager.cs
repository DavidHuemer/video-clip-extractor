using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Managers.Timeline.SelectionManager;

[UsedImplicitly]
[Singleton]
public class TimelineExtractionSelectionManager : ITimelineExtractionSelectionManager
{
    private readonly ITimelineNavigationViewModel _timelineNavigation;

    public TimelineExtractionSelectionManager(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _timelineNavigation = viewModelProvider.Get<ITimelineNavigationViewModel>();
    }

    public event EventHandler<SelectedExtractionChangedEventArgs>? SelectedExtractionChanged;

    public void Selected(IExtractionViewModel? extractionViewModel)
    {
        if (SelectedExtractionViewModel != null)
        {
            SelectedExtractionViewModel.IsSelected = false;
        }

        SelectedExtractionViewModel = extractionViewModel;

        if (extractionViewModel == null) return;

        HandleMovement();
        extractionViewModel.IsSelected = true;
        SelectedExtractionChanged?.Invoke(this, new SelectedExtractionChangedEventArgs(SelectedExtractionViewModel));
    }

    public IExtractionViewModel? SelectedExtractionViewModel { get; private set; }

    private void HandleMovement()
    {
        if (_timelineNavigation.MovementState != MovementState.None) return;

        _timelineNavigation.MovementState = MovementState.Extraction;
    }
}