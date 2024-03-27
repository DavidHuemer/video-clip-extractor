using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels.VideosSetupWindow;

[TestFixture]
[TestOf(typeof(VideosSetupWindowViewModel))]
public class VideosSetupWindowViewModelTest : BaseWindowViewModelTest
{
    private Mock<IVideosSetupViewModel> _videosSetupViewModelMock = null!;
    private VideosSetupWindowViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videosSetupViewModelMock = ViewModelProviderMock.CreateViewModelMock<IVideosSetupViewModel>();
        _viewModel = new VideosSetupWindowViewModel(DependencyMock.Object);
    }

    [Test]
    public void FinishEventClosesWindow()
    {
        _viewModel.Show();
        _videosSetupViewModelMock.Raise(vm => vm.Finish += null!, EventArgs.Empty);
        WindowMock.Verify(w => w.Close(), Times.Once);
    }
}