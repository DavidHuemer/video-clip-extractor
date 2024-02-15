using JetBrains.Annotations;
using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.Extractions.Basics;

namespace VideoClipExtractor.Core.Managers.TimelineManager.SelectionManager;

[UsedImplicitly]
public class TimelineExtractionSelectionManager : ITimelineExtractionSelectionManager
{
    private IExtractionViewModel? _selectedExtractionViewModel;

    public event EventHandler<SelectedExtractionChangedEventArgs>? SelectedExtractionChanged;

    public void Selected(IExtractionViewModel extractionViewModel)
    {
        if (_selectedExtractionViewModel != null)
        {
            _selectedExtractionViewModel.IsSelected = false;
        }

        extractionViewModel.IsSelected = true;
        _selectedExtractionViewModel = extractionViewModel;
        SelectedExtractionChanged?.Invoke(this, new SelectedExtractionChangedEventArgs(_selectedExtractionViewModel));
    }
}