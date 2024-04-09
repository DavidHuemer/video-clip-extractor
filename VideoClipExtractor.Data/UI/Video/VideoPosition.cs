using System.Text.Json.Serialization;

namespace VideoClipExtractor.Data.UI.Video;

public class VideoPosition
{
    [JsonConstructor]
    public VideoPosition(TimeSpan time, double frameRate)
    {
        Time = time;
        FrameRate = Math.Ceiling(frameRate);
    }

    public VideoPosition(int frame, double frameRate)
    {
        FrameRate = Math.Ceiling(frameRate);
        Time = TimeSpan.FromSeconds(frame / FrameRate);
    }

    public TimeSpan Time { get; set; }
    public double FrameRate { get; set; }

    [JsonIgnore] public int Frame => (int)Math.Round(Time.TotalSeconds * FrameRate);

    public override string ToString()
    {
        var totalFrames = Frame;
        var frames = totalFrames % (int)FrameRate;
        var seconds = (int)Time.TotalSeconds % 60;
        var minutes = (int)Time.TotalMinutes % 60;
        var hours = (int)Time.TotalHours;

        return $"{hours:00}:{minutes:00}:{seconds:00}:{frames:00}";
    }


    public override bool Equals(object? obj)
    {
        return obj is VideoPosition position && position.Frame == Frame;
    }
}