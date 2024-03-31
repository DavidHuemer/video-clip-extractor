using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Data.Extractions.Basics;

public interface IExtraction
{
    public string Name { get; set; }

    public ExtractionResult? Result { get; set; }

    void SetupSelection(Action<IExtractionViewModel> selectionCallback);
}