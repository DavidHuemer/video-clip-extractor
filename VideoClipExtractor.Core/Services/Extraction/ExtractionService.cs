using System.IO;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.Extraction.FileExtractionService;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction;

[UsedImplicitly]
public class ExtractionService(IDependencyProvider provider) : IExtractionService
{
    private readonly IFileExtractionService _fileExtractionService = provider.GetDependency<IFileExtractionService>();
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public event EventHandler? StartImageExtractions;

    public async Task Extract(VideoViewModel video)
    {
        ValidateVideo(video);
        await ExtractExtractions(video);
    }

    private void ValidateVideo(VideoViewModel video)
    {
        if (video.VideoStatus != VideoStatus.ReadyForExport)
            throw new VideoNotReadyForExportException(video.VideoStatus);

        if (_fileService.FileExists(video.LocalPath) == false)
            throw new FileNotFoundException(video.LocalPath);
    }

    private async Task ExtractExtractions(VideoViewModel video)
    {
        IEnumerable<IExtraction> imageExtractions = video.ImageExtractions.ToList();
        IEnumerable<IExtraction> videoExtractions = video.VideoExtractions.ToList();
        var extractions = imageExtractions.Concat(videoExtractions);

        foreach (var extraction in extractions)
        {
            await _fileExtractionService.Extract(video, extraction);
        }
    }
}