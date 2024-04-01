using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionFactory;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigationEditor;

[Transient]
public class VideoPlayerNavigationEditor : BaseViewModelContainer, IVideoPlayerNavigationEditor
{
    private readonly IVideoPositionFactory _videoPositionFactory;
    private readonly IVideoPositionService _videoPositionService;

    private bool _isEditing;

    public VideoPlayerNavigationEditor(IDependencyProvider provider) : base(provider)
    {
        _videoPositionFactory = provider.GetDependency<IVideoPositionFactory>();
        _videoPositionService = provider.GetDependency<IVideoPositionService>();
        FrameNavigationViewModel = ViewModelProvider.Get<IFrameNavigationViewModel>();

        FrameNavigationViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(IFrameNavigationViewModel.VideoPosition))
            {
                OnPropertyChanged(nameof(Frame));

                if (!_isEditing)
                {
                    OnPropertyChanged(nameof(VideoPosition));
                }
            }
        };
    }

    private IFrameNavigationViewModel FrameNavigationViewModel { get; }

    public int Frame
    {
        get => FrameNavigationViewModel.VideoPosition.Frame;
        set
        {
            var videoPosition = _videoPositionFactory.GetVideoPositionByFrame(value);
            _videoPositionService.RequestPositionChange(videoPosition);
        }
    }

    public string VideoPosition
    {
        get => FrameNavigationViewModel.VideoPosition.ToString();
        set
        {
            try
            {
                var videoPosition = _videoPositionFactory.GetVideoPositionByString(value);
                _videoPositionService.RequestPositionChange(videoPosition);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void StartVideoPositionEdit()
    {
        _isEditing = true;
    }

    public void EndVideoPositionEdit()
    {
        _isEditing = false;
    }
}