using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels.VideoSetupSettingsViewModels;

namespace VideoClipExtractor.Tests.UI.ViewModels.SetupViewModels.VideosSetupViewModels.
    VideoSetupSettingsViewModelsTests;

[TestFixture]
[TestOf(typeof(VideoSetupSettingsViewModel))]
public class VideoSetupSettingsViewModelTest : BaseViewModelTest
{
    private VideoSetupSettingsViewModel _videoSetupSettingsViewModel = null!;

    public override void Setup()
    {
        base.Setup();
        _videoSetupSettingsViewModel = new VideoSetupSettingsViewModel();
    }

    [Test]
    public void EnableSettingsFalseAtBeginning()
    {
        Assert.IsFalse(_videoSetupSettingsViewModel.EnableSettings);
    }

    [Test]
    public void ReconsiderSkippedVideosIsFalseAtBeginning()
    {
        Assert.IsFalse(_videoSetupSettingsViewModel.ReconsiderSkippedVideos);
    }

    [Test]
    public void ShowProgressIsFalseAtBeginning()
    {
        Assert.IsFalse(_videoSetupSettingsViewModel.ShowProgress);
    }

    [Test]
    public void ShowStatisticsIsFalseAtBeginning()
    {
        Assert.IsFalse(_videoSetupSettingsViewModel.ShowStatistics);
    }

    [Test]
    public void LoadVideosCommandNotAllowedAtBeginning()
    {
        Assert.IsFalse(_videoSetupSettingsViewModel.LoadVideos.CanExecute(null));
    }

    [Test]
    public void EnableSettingsFalseWhenEmptyProjectSet()
    {
        var project = ProjectExamples.GetEmptyProject();
        _videoSetupSettingsViewModel.Project = project;
        Assert.IsFalse(_videoSetupSettingsViewModel.EnableSettings);
    }

    [Test]
    public void EnableSettingsTrueWhenProjectWithVideosSet()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        Assert.IsTrue(_videoSetupSettingsViewModel.EnableSettings);
    }

    [Test]
    public void LoadVideosCommandAllowedWhenProjectSet()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        Assert.IsTrue(_videoSetupSettingsViewModel.LoadVideos.CanExecute(null));
    }

    [Test]
    public void LoadVideosInvokesLoadVideosRequested()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        _videoSetupSettingsViewModel.ReconsiderSkippedVideos = true;

        var invoked = false;
        _videoSetupSettingsViewModel.LoadVideosRequested += settings =>
        {
            invoked = true;
            Assert.IsTrue(settings.ReconsiderSkippedVideos);
        };
        _videoSetupSettingsViewModel.LoadVideos.Execute(null);
        Assert.IsTrue(invoked);
    }

    [Test]
    public void LoadVideosNotAllowedWhenAlreadyLoading()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        _videoSetupSettingsViewModel.LoadVideos.Execute(null);
        Assert.IsFalse(_videoSetupSettingsViewModel.LoadVideos.CanExecute(null));
    }

    [Test]
    public void ShowProgressTrueWhenLoading()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        _videoSetupSettingsViewModel.LoadVideos.Execute(null);

        Assert.IsTrue(_videoSetupSettingsViewModel.ShowProgress);
    }

    [Test]
    public void ShowStatisticsTrueWhenLoading()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        _videoSetupSettingsViewModel.LoadVideos.Execute(null);

        Assert.IsTrue(_videoSetupSettingsViewModel.ShowStatistics);
    }

    [Test]
    public void ShowProgressFalseWhenLoadingFinished()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        _videoSetupSettingsViewModel.LoadVideos.Execute(null);

        _videoSetupSettingsViewModel.LoadingFinished();
        Assert.IsFalse(_videoSetupSettingsViewModel.ShowProgress);
    }

    [Test]
    public void ShowStatisticsTrueWhenLoadingFinished()
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos.AddRange(SourceVideoExamples.GetSourceVideoExamples(4));
        _videoSetupSettingsViewModel.Project = project;
        _videoSetupSettingsViewModel.LoadVideos.Execute(null);

        _videoSetupSettingsViewModel.LoadingFinished();
        Assert.IsTrue(_videoSetupSettingsViewModel.ShowStatistics);
    }
}