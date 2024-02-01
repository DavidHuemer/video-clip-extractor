using System.IO;
using MediaDevices;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.VideoRepos.Implementations;

public class PhoneVideoRepository(MediaDevice device, string path) : IVideoRepository
{
    public void Connect()
    {
        device.Connect();
    }

    public void CopyFileByPath(string sourceVideoPath, string cacheVideoPath)
    {
        using var memoryStream = new MemoryStream();
        device.DownloadFile(sourceVideoPath, memoryStream);
        memoryStream.Position = 0;
        using var fileStream = new FileStream(cacheVideoPath, FileMode.Create);
        memoryStream.WriteTo(fileStream);
    }

    public IEnumerable<SourceVideo> GetFiles()
    {
        var dir = device.GetDirectoryInfo(path);
        return dir.EnumerateFiles()
            .Where(file => VideoFileTypes.IsSupported(file.Name.Split('.').Last()))
            .Select(file => new SourceVideo(file));
    }
}