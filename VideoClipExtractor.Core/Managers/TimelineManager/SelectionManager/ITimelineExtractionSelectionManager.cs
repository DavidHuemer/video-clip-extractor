using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.Extractions.Basics;

namespace VideoClipExtractor.Core.Managers.TimelineManager.SelectionManager;

public interface ITimelineExtractionSelectionManager
{
    event EventHandler<SelectedExtractionChangedEventArgs> SelectedExtractionChanged;
    void Selected(IExtractionViewModel extractionViewModel);
}