using System.IO;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.VideoValidationService;

[Transient]
public class VideoValidationService(IDependencyProvider provider) : IVideoValidationService
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public void ValidateVideoForExtraction(VideoViewModel video)
    {
        if (video.VideoStatus == VideoStatus.Unset)
            throw new VideoNotReadyForExportException(video.VideoStatus);

        if (_fileService.FileExists(video.LocalPath) == false)
            throw new FileNotFoundException(video.LocalPath);
    }
}