using System.Windows;

namespace VideoClipExtractor.UI.Controls.Extraction
{
    public partial class ExtractionResultWarningControl
    {
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(ExtractionResultWarningControl),
                new PropertyMetadata(""));

        public ExtractionResultWarningControl()
        {
            InitializeComponent();
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
    }
}