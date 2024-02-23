using BaseUI.Services.FileServices;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class FileServiceMock : Mock<IFileService>
{
    public void SetupFileExists(string path, bool exists)
    {
        Setup(f => f.FileExists(path)).Returns(exists);
    }

    public void SetupGetTmpFolder(string folder = @"C:\Tmp")
    {
        Setup(f => f.GetTmpFolder()).Returns(folder);
    }

    public void SetupDirectoryExists(string cImages, bool b)
    {
        Setup(f => f.DirectoryExists(cImages)).Returns(b);
    }

    public void SetupGetFileSize(string path, int size)
    {
        Setup(f => f.GetFileSize(path)).Returns(size);
    }
}