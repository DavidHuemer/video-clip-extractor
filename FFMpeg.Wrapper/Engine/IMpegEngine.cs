namespace FFMpeg.Wrapper.Engine;

/// <summary>
/// The engine that runs the ffmpeg commands.
/// </summary>
public interface IMpegEngine
{
    Task<string> RunCommandAsync(string command);
}