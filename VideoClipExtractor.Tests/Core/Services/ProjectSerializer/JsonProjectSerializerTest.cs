using System.Text.Json;
using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Extensions;

namespace VideoClipExtractor.Tests.Core.Services.ProjectSerializer;

[TestFixture]
[TestOf(typeof(JsonProjectSerializer))]
public class JsonProjectSerializerTest : BaseDependencyTest
{
    [TearDown]
    public void TearDown()
    {
        _tempFolder.RemoveFolder();
    }

    private TestFolder _tempFolder = null!;
    private Mock<IFileService> _fileServiceMock = null!;
    private JsonProjectSerializer _jsonProjectSerializer = null!;

    public override void Setup()
    {
        base.Setup();
        _tempFolder = new TestFolder(nameof(JsonProjectSerializerTest));
        _fileServiceMock = DependencyMock.CreateMockDependency<IFileService>();
        _jsonProjectSerializer = new JsonProjectSerializer(DependencyMock.Object);
    }

    [Test]
    public async Task EmptyProjectIsStored()
    {
        var project = ProjectExamples.GetEmptyProject();
        var filePath = await StoreProject(project, "emptyProject.vce");
        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public async Task EmptyProjectCanBeLoaded()
    {
        var project = ProjectExamples.GetEmptyProject();
        var filePath = await StoreProject(project, "emptyProject.vce");
        var loadedProject = await _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public async Task ProjectWithSourceVideosCanBeStored()
    {
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(10);
        var project = ProjectExamples.GetExampleProject(sourceVideos: sourceVideos);
        var filePath = await StoreProject(project, "projectWithSourceVideos.vce");
        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public async Task ProjectWithSourceVideosCanBeLoaded()
    {
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(10);
        var project = ProjectExamples.GetExampleProject(sourceVideos: sourceVideos);
        var filePath = await StoreProject(project, "projectWithSourceVideos.vce");
        var loadedProject = await _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public async Task ProjectWithWorkingVideosCanBeStored()
    {
        var workingVideos = VideoExamples.GetVideoViewModelExamples(10).ToList();
        var project = ProjectExamples.GetExampleProject();
        project.WorkingVideos = workingVideos;
        var filePath = await StoreProject(project, "projectWithWorkingVideos.vce");
        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public async Task ProjectWithWorkingVideosCanBeLoaded()
    {
        var workingVideos = VideoExamples.GetVideoViewModelExamples(10).ToList();
        var project = ProjectExamples.GetExampleProject();
        project.WorkingVideos = workingVideos;
        var filePath = await StoreProject(project, "projectWithWorkingVideos.vce");
        var loadedProject = await _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public async Task RealisticProjectCanBeStored()
    {
        var project = ProjectExamples.GetRealisticProject();
        var filePath = await StoreProject(project, "realisticProject.vcs");
        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public async Task RealisticProjectCanBeLoaded()
    {
        var project = ProjectExamples.GetRealisticProject();
        var filePath = await StoreProject(project, "realisticProject.vcs");
        var loadedProject = await _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public async Task InvalidProjectThrowsJsonException()
    {
        var filePath = _tempFolder.GetFilePath("invalidProject.vce");
        await File.WriteAllTextAsync(filePath, "This is not a valid project file.");

        SetupFileServiceMock(filePath);
        Assert.ThrowsAsync<JsonException>(() => _jsonProjectSerializer.LoadProject(filePath));
    }

    private async Task<string> StoreProject(Project project, string name)
    {
        var filePath = _tempFolder.GetFilePath(name);
        await _jsonProjectSerializer.StoreProject(project, filePath);
        SetupFileServiceMock(filePath);
        return filePath;
    }

    private void SetupFileServiceMock(string filePath)
    {
        _fileServiceMock.Setup(x => x.FileExists(filePath))
            .Returns(true);
    }
}