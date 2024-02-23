using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.ImageExtractions;
using VideoClipExtractor.Core.Services.Extraction.VideoExtractions;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.FileExtractionService;

[UsedImplicitly]
public class FileExtractionService(IDependencyProvider provider) : IFileExtractionService
{
    private readonly IImageExtractionService
        _imageExtractionService = provider.GetDependency<IImageExtractionService>();

    private readonly IVideoExtractionService
        _videoExtractionService = provider.GetDependency<IVideoExtractionService>();

    public async Task Extract(VideoViewModel video, IExtraction extraction)
    {
        switch (extraction)
        {
            case ImageExtraction imageExtraction:
                await _imageExtractionService.Extract(video, imageExtraction);
                break;
            case VideoExtraction videoExtraction:
                await _videoExtractionService.Extract(video, videoExtraction);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(extraction));
        }
    }
}