using System.Text.Json;
using BaseUI.Services.FileServices;
using Moq;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
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
    public void EmptyProjectIsStored()
    {
        var project = ProjectExamples.GetEmptyProject();
        var filePath = _tempFolder.GetFilePath("emptyProject.vce");
        _jsonProjectSerializer.StoreProject(project, filePath);

        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public void EmptyProjectCanBeLoaded()
    {
        var project = ProjectExamples.GetEmptyProject();
        var filePath = _tempFolder.GetFilePath("emptyProject.vce");

        _fileServiceMock.Setup(x => x.FileExists(filePath))
            .Returns(true);
        _jsonProjectSerializer.StoreProject(project, filePath);

        var loadedProject = _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public void ProjectWithSourceVideosCanBeStored()
    {
        var sourceVideos = VideoExamples.GetSourceVideoExamples(10);
        var project = ProjectExamples.GetExampleProject(sourceVideos: sourceVideos);
        var filePath = _tempFolder.GetFilePath("projectWithSourceVideos.vce");
        _jsonProjectSerializer.StoreProject(project, filePath);
        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public void ProjectWithSourceVideosCanBeLoaded()
    {
        var sourceVideos = VideoExamples.GetSourceVideoExamples(10);
        var project = ProjectExamples.GetExampleProject(sourceVideos: sourceVideos);
        var filePath = _tempFolder.GetFilePath("projectWithSourceVideos.vce");

        _fileServiceMock.Setup(x => x.FileExists(filePath))
            .Returns(true);
        _jsonProjectSerializer.StoreProject(project, filePath);
        var loadedProject = _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public void ProjectWithWorkingVideosCanBeStored()
    {
        var workingVideos = VideoExamples.GetExampleVideos(10).ToList();
        var project = ProjectExamples.GetExampleProject();
        project.WorkingVideos = workingVideos;
        var filePath = _tempFolder.GetFilePath("projectWithWorkingVideos.vce");
        _jsonProjectSerializer.StoreProject(project, filePath);
        Assert.IsTrue(File.Exists(filePath));
    }

    [Test]
    public void ProjectWithWorkingVideosCanBeLoaded()
    {
        var workingVideos = VideoExamples.GetExampleVideos(10).ToList();
        var project = ProjectExamples.GetExampleProject();
        project.WorkingVideos = workingVideos;
        var filePath = _tempFolder.GetFilePath("projectWithWorkingVideos.vce");

        _fileServiceMock.Setup(x => x.FileExists(filePath))
            .Returns(true);
        _jsonProjectSerializer.StoreProject(project, filePath);
        var loadedProject = _jsonProjectSerializer.LoadProject(filePath);
        Assert.That(loadedProject, Is.EqualTo(project));
    }

    [Test]
    public void InvalidProjectThrowsJsonException()
    {
        var filePath = _tempFolder.GetFilePath("invalidProject.vce");
        File.WriteAllText(filePath, "This is not a valid project file.");

        _fileServiceMock.Setup(x => x.FileExists(filePath))
            .Returns(true);
        Assert.Throws<JsonException>(() => _jsonProjectSerializer.LoadProject(filePath));
    }
}