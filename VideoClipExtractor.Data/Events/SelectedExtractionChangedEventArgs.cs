using VideoClipExtractor.Data.Extractions.Basics;

namespace VideoClipExtractor.Data.Events;

public class SelectedExtractionChangedEventArgs(IExtractionViewModel? extractionViewModel) : EventArgs
{
    public IExtractionViewModel? ExtractionViewModel { get; set; } = extractionViewModel;
}