namespace FFMpeg.Wrapper.MpegInfo;

public interface IMpegInfo
{
    Task<TimeSpan> GetDurationAsync(string inputPath);
}