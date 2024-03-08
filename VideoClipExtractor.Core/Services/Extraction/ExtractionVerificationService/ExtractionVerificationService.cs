using System.IO;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Exceptions.ExtractionExceptions;
using VideoClipExtractor.Data.Extractions.Results;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;

[UsedImplicitly]
public class ExtractionVerificationService(IDependencyProvider provider) : IExtractionVerificationService
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public ExtractionResult CheckExtraction(string path)
    {
        try
        {
            if (!_fileService.FileExists(path))
                throw new FileNotFoundException("The extraction was not found", path);

            var fileSize = _fileService.GetFileSize(path);
            if (fileSize <= 0)
                throw new ExtractionFailedException(path);

            return new ExtractionResult(path, fileSize);
        }
        catch (Exception e)
        {
            return new ExtractionResult(e, path);
        }
    }
}