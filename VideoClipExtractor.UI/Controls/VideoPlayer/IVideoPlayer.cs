namespace VideoClipExtractor.UI.Controls.VideoPlayer;

public interface IVideoPlayer
{
    TimeSpan Position { get; set; }
    event EventHandler? VideoOpened;
}