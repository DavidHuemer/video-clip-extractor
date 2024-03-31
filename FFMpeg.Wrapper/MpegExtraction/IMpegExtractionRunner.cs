namespace FFMpeg.Wrapper.MpegExtraction;

public interface IMpegExtractionRunner
{
    Task ExtractImageAsync(string inputVideoPath, string outputImagePath, TimeSpan position);

    Task ExtractVideoAsync(string inputPath, string outputPath, TimeSpan begin, TimeSpan duration);
}