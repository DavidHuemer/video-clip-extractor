using System.IO;
using System.Text.Json;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Services.ProjectSerializer;

public class JsonProjectSerializer(IDependencyProvider provider) : IProjectSerializer
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public void StoreProject(Project project, string path)
    {
        var json = JsonSerializer.Serialize(project);
        File.WriteAllText(path, json);
    }

    public Project LoadProject(string path)
    {
        if (!_fileService.FileExists(path)) throw new FileNotFoundException("File not found.", path);
        var json = File.ReadAllText(path);
        var project = JsonSerializer.Deserialize<Project>(json);

        if (project is null) throw new JsonException("Project could not be deserialized.");

        return project;
    }
}