using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.Managers.Extraction;
using VideoClipExtractor.UI.ViewModels.Main.Explorer.ExplorerActionBar;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideosSetupWindow;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.Explorer.ExplorerActionBar;

[TestFixture]
[TestOf(typeof(ExplorerActionBarViewModel))]
public class ExplorerActionBarViewModelTest : BaseViewModelTest
{
    private Mock<IVideosSetupWindowViewModel> _videosSetupWindowViewModelMock = null!;
    private Mock<IExtractionManager> _extractionManagerMock = null!;
    private ExplorerActionBarViewModel _viewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videosSetupWindowViewModelMock = ViewModelProviderMock.CreateViewModelMock<IVideosSetupWindowViewModel>();
        _extractionManagerMock = DependencyMock.CreateMockDependency<IExtractionManager>();
        _viewModel = new ExplorerActionBarViewModel(DependencyMock.Object);
    }


    [Test]
    public void RefreshVideosNotAllowedAtBeginning()
    {
        Assert.That(_viewModel.RefreshVideos.CanExecute(null), Is.False);
    }

    [Test]
    public void ExportVideosNotAllowedAtBeginning()
    {
        Assert.That(_viewModel.ExportVideos.CanExecute(null), Is.False);
    }

    [Test]
    public void RefreshVideosAllowedWhenProjectIsSet()
    {
        _viewModel.Project = ProjectExamples.GetExampleProject();
        Assert.That(_viewModel.RefreshVideos.CanExecute(null), Is.True);
    }

    [Test]
    public void ExportVideosAllowedWhenProjectIsSet()
    {
        _viewModel.Project = ProjectExamples.GetExampleProject();
        Assert.That(_viewModel.ExportVideos.CanExecute(null), Is.True);
    }

    [Test]
    public void RefreshVideosOpensVideosSetupWindow()
    {
        _viewModel.Project = ProjectExamples.GetExampleProject();
        _viewModel.RefreshVideos.Execute(null);
        _videosSetupWindowViewModelMock.Verify(m => m.ShowDialog(), Times.Once);
    }

    [Test]
    public void ExportVideosExtractsVideos()
    {
        var project = ProjectExamples.GetExampleProject();
        var workingVideos = VideoExamples.GetVideoViewModelExamples(2);
        project.WorkingVideos = workingVideos;

        _viewModel.Project = project;
        _viewModel.ExportVideos.Execute(null);
        _extractionManagerMock.Verify(m => m.ExtractVideos(workingVideos), Times.Once);
    }
}