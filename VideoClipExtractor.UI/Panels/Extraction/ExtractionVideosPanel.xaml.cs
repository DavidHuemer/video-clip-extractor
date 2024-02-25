using System.Windows.Controls;

namespace VideoClipExtractor.UI.Panels.Extraction;

public partial class ExtractionVideosPanel : UserControl
{
    public ExtractionVideosPanel()
    {
        InitializeComponent();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        e.Handled = true;
    }
}