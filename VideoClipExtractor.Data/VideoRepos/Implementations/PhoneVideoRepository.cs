using MediaDevices;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.VideoRepos.Implementations;

public class PhoneVideoRepository(MediaDevice device, string path) : IVideoRepository
{
    public void Connect()
    {
        device.Connect();
    }

    public IEnumerable<SourceVideo> GetFiles()
    {
        var dir = device.GetDirectoryInfo(path);
        return dir.EnumerateFiles()
            .Where(file => VideoFileTypes.IsSupported(file.Name.Split('.').Last()))
            .Select(file => new SourceVideo(file));
    }
}