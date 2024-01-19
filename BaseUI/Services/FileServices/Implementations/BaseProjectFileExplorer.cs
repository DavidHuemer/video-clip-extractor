using BaseUI.Data;
using BaseUI.Services.DependencyInjection;

namespace BaseUI.Services.FileServices.Implementations;

public class BaseProjectFileExplorer : IProjectFileExplorer
{
    public BaseProjectFileExplorer(IDependencyProvider provider, FileTypeInfo fileTypeInfo)
    {
        Provider = provider;
    }

    public IDependencyProvider Provider { get; }

    public string GetSaveProjectFilePath()
    {
        return Provider.GetDependency<IFileExplorer>().GetSaveFilePath("Project files (*.json)|*.json", "json");
    }
}