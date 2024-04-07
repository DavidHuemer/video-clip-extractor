using System.Windows.Controls;
using System.Windows.Input;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

namespace VideoClipExtractor.UI.Panels.VideoPlayerPanels;

public partial class VideoPlayerNavigationPanel
{
    public VideoPlayerNavigationPanel()
    {
        InitializeComponent();
    }

    private IVideoPlayerNavigationViewModel? VideoPlayerNavigationViewModel =>
        DataContext as IVideoPlayerNavigationViewModel;

    private void OnVideoPositionTextBoxGotFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (VideoPlayerNavigationViewModel == null) return;

        VideoPlayerNavigationViewModel.VideoPlayerNavigationEditor.StartVideoPositionEdit();

        Console.WriteLine("Got focus");
    }

    private void OnVideoPositionTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (VideoPlayerNavigationViewModel == null) return;

        if (e.Key == Key.Enter)
        {
            var bindingExpression = VideoPositionTextBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression?.UpdateSource();
            Keyboard.ClearFocus();
        }
    }

    private void UIElement_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (VideoPlayerNavigationViewModel == null) return;

        VideoPlayerNavigationViewModel.VideoPlayerNavigationEditor.EndVideoPositionEdit();
        Console.WriteLine("Lost focus");
    }
}