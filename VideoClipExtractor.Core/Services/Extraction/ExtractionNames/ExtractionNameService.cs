using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionNames;

[UsedImplicitly]
public class ExtractionNameService(IDependencyProvider provider) : IExtractionNameService
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();
    private readonly IProjectManager _projectManager = provider.GetDependency<IProjectManager>();


    public string GetImagePath(VideoViewModel video, ImageExtraction imageExtraction)
    {
        return GetExtractionName(video, imageExtraction, "png");
    }

    public string GetVideoPath(VideoViewModel video, VideoExtraction videoExtraction)
    {
        return GetExtractionName(video, videoExtraction, "mp4");
    }

    public event EventHandler? NoProjectSpecified;

    private string GetExtractionName(VideoViewModel video, IExtraction extraction, string fileExtension)
    {
        var folderPath = GetExtractionFolderPath();
        if (_fileService.DirectoryExists(folderPath) == false)
            _fileService.CreateDirectory(folderPath);


        if (!string.IsNullOrWhiteSpace(extraction.Name))
        {
            // extraction has a name, so the file will be located in a subfolder of the extraction folder.
            folderPath = System.IO.Path.Combine(folderPath, extraction.Name);
            HandleSubFolderExtraction(folderPath);
        }

        return GetValidFileName(folderPath, video, fileExtension);
    }

    private string GetValidFileName(string extractionFolder, VideoViewModel video, string fileExtension)
    {
        var nrIncrement = 0;

        var filePath = GetFilePathWithIncrement(extractionFolder, video.Name, fileExtension, nrIncrement);

        while (_fileService.FileExists(filePath))
        {
            filePath = GetFilePathWithIncrement(extractionFolder, video.Name, fileExtension, ++nrIncrement);
        }

        return filePath;
    }

    private string GetFilePathWithIncrement(string extractionFolder, string fileName, string extension, int nrIncrement)
    {
        if (nrIncrement == 0)
        {
            return System.IO.Path.Combine(extractionFolder, $"{fileName}.{extension}");
        }
        else
        {
            return System.IO.Path.Combine(extractionFolder, $"{fileName}_{nrIncrement}.{extension}");
        }
    }

    private void HandleSubFolderExtraction(string extractionFolderPath)
    {
        if (!_fileService.DirectoryExists(extractionFolderPath))
        {
            _fileService.CreateDirectory(extractionFolderPath);
        }
    }

    private string GetExtractionFolderPath()
    {
        var project = _projectManager.Project;
        if (project == null)
        {
            NoProjectSpecified?.Invoke(this, EventArgs.Empty);
            return _fileService.GetTmpFolder();
        }
        else
        {
            return project.ImageDirectory;
        }
    }
}