namespace FFMpeg.Wrapper.Engine;

public interface IMpegEngine
{
    /// <summary>
    /// Extracts an image from a video file at a specific time.
    /// </summary>
    /// <param name="inputVideoPath">The path to the video from which the image should be taken</param>
    /// <param name="outputImagePath">The path to the image that should be extracted</param>
    /// <param name="timeSpan">The time at which the image should be taken</param>
    /// <returns></returns>
    Task ExtractImageAsync(string inputVideoPath, string outputImagePath, TimeSpan timeSpan);

    Task ExtractVideoAsync(string inputVideoPath, string outputVideoPath, TimeSpan startTime, TimeSpan duration);
}