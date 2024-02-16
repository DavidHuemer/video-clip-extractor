using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.Extractions.Basics;

namespace VideoClipExtractor.UI.Managers.Timeline.SelectionManager;

public interface ITimelineExtractionSelectionManager
{
    IExtractionViewModel? SelectedExtractionViewModel { get; }
    event EventHandler<SelectedExtractionChangedEventArgs> SelectedExtractionChanged;
    void Selected(IExtractionViewModel? extractionViewModel);
}