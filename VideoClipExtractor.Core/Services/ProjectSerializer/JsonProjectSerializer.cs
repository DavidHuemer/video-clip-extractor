using System.IO;
using System.Text.Json;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Services.ProjectSerializer;

public class JsonProjectSerializer : IProjectSerializer
{
    public void StoreProject(Project project, string path)
    {
        var json = JsonSerializer.Serialize(project);
        File.WriteAllText(path, json);
    }

    public Project LoadProject(string path)
    {
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var project = JsonSerializer.Deserialize<Project>(json);

            if (project is null) throw new JsonException("Project could not be deserialized.");

            return project;
        }

        throw new FileNotFoundException("File not found.", path);
    }
}