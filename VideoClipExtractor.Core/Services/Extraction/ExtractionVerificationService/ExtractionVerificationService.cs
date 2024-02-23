using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionVerificationService;

[UsedImplicitly]
public class ExtractionVerificationService(IDependencyProvider provider) : IExtractionVerificationService
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public bool ExtractionSucceeded(string path)
    {
        return _fileService.FileExists(path)
               && _fileService.GetFileSize(path) > 0;
    }
}