using System.IO;
using System.Text.Json;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Services.ProjectSerializer;

public class JsonProjectSerializer(IDependencyProvider provider) : IProjectSerializer
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public async Task StoreProject(Project project, string path)
    {
        var json = JsonSerializer.Serialize(project);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task<Project> LoadProject(string path)
    {
        if (!_fileService.FileExists(path)) throw new FileNotFoundException("File not found.", path);
        var json = await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<Project>(json) ??
               throw new JsonException("Project could not be deserialized.");
    }
}