using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigationEditor;

public interface IVideoPlayerNavigationEditor : IBaseViewModel
{
    int Frame { get; set; }

    string VideoPosition { get; set; }

    void StartVideoPositionEdit();

    void EndVideoPositionEdit();
}