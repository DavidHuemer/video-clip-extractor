using FFMpeg.Wrapper.Data;

namespace FFMpeg.Wrapper.MpegInfo;

public interface IMpegInfo
{
    Task<VideoInfo> GetVideoInfoAsync(string inputPath);
}