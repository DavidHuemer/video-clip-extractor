using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.ImageExtractions;
using VideoClipExtractor.Core.Services.Extraction.VideoExtractions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.FileExtractionService;

[UsedImplicitly]
public class FileExtractionService(IDependencyProvider provider) : IFileExtractionService
{
    private readonly IImageExtractionService
        _imageExtractionService = provider.GetDependency<IImageExtractionService>();

    private readonly IVideoExtractionService
        _videoExtractionService = provider.GetDependency<IVideoExtractionService>();

    public async Task<ExtractionResult> Extract(VideoViewModel video, IExtraction extraction)
    {
        try
        {
            return extraction switch
            {
                ImageExtraction imageExtraction => await _imageExtractionService.Extract(video, imageExtraction),
                VideoExtraction videoExtraction => await _videoExtractionService.Extract(video, videoExtraction),
                _ => throw new ArgumentOutOfRangeException(nameof(extraction))
            };
        }
        catch (Exception e)
        {
            return new ExtractionResult(e);
        }
    }
}