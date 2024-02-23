using BaseUI.Services.FileServices.Implementations;
using VideoClipExtractor.Tests.Basics.Extensions;

namespace VideoClipExtractor.Tests.BaseUI.Services.FileServices.Implementations;

[TestFixture]
[TestOf(typeof(FileService))]
public class FileServiceTest
{
    [SetUp]
    public void Setup()
    {
        _tempFolder = new TestFolder("FileServiceTest");
        _fileService = new FileService();
    }

    [TearDown]
    public void TearDown()
    {
        _tempFolder.RemoveFolder();
    }

    private TestFolder _tempFolder = null!;
    private FileService _fileService;

    [Test]
    public void FileExists()
    {
        var filePath = _tempFolder.GetFilePath("file.txt");
        File.WriteAllText(filePath, "test");
        Assert.IsTrue(_fileService.FileExists(filePath));
    }

    [Test]
    public void FileExistsReturnsFalse()
    {
        var filePath = _tempFolder.GetFilePath("file.txt");
        Assert.IsFalse(_fileService.FileExists(filePath));
    }

    [Test]
    public void DirectoryExists()
    {
        var directoryPath = _tempFolder.GetFolderPath("directory");
        Directory.CreateDirectory(directoryPath);
        Assert.IsTrue(_fileService.DirectoryExists(directoryPath));
    }

    [Test]
    public void DirectoryExistsReturnsFalse()
    {
        var directoryPath = _tempFolder.GetFolderPath("directory");
        Assert.IsFalse(_fileService.DirectoryExists(directoryPath));
    }

    [Test]
    public void GetTmpFolder()
    {
        var tmpFolder = _fileService.GetTmpFolder();
        Assert.IsTrue(Directory.Exists(tmpFolder));
    }

    [Test]
    public void CreateDirectory()
    {
        var directoryPath = _tempFolder.GetFolderPath("directory");
        _fileService.CreateDirectory(directoryPath);
        Assert.IsTrue(Directory.Exists(directoryPath));
    }

    [Test]
    public void GetFileSize()
    {
        var filePath = _tempFolder.GetFilePath("file.txt");
        File.WriteAllText(filePath, "test");
        Assert.AreEqual(4, _fileService.GetFileSize(filePath));
    }
}