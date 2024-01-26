using MediaDevices;

namespace VideoClipExtractor.Data.VideoRepos.Implementations;

public class PhoneVideoRepository(MediaDevice device, string path) : IVideoRepository
{
    public void Connect()
    {
        device.Connect();
    }
}