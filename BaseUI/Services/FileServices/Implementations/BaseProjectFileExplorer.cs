using BaseUI.Data;
using BaseUI.Services.Provider.DependencyInjection;

namespace BaseUI.Services.FileServices.Implementations;

/// <summary>
///     Base class for project file explorers
/// </summary>
/// <param name="provider">The dependency provider that holds all dependencies</param>
/// <param name="fileTypeInfo">The info of the project file type</param>
public class BaseProjectFileExplorer(IDependencyProvider provider, FileTypeInfo fileTypeInfo) : IProjectFileExplorer
{
    private IDependencyProvider Provider { get; } = provider;

    private FileTypeInfo FileTypeInfo { get; } = fileTypeInfo;

    public string GetSaveProjectFilePath()
    {
        return Provider.GetDependency<IFileExplorer>()
            .GetSaveFilePath(FileTypeInfo.FileFilter);
    }

    public string GetOpenProjectFilePath()
    {
        return Provider.GetDependency<IFileExplorer>()
            .GetOpenFilePath(FileTypeInfo.FileFilter);
    }
}