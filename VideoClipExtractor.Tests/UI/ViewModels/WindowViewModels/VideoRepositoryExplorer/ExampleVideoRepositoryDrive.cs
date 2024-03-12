using BaseUI.ViewModels.Tree;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.VideoRepos.Explorer;

namespace VideoClipExtractor.Tests.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;

public class ExampleVideoRepositoryDrive(string name) : VideoRepositoryDrive(name)
{
    protected override IEnumerable<BaseTreeViewItem> LoadChildren()
    {
        throw new NotImplementedException();
    }

    public override VideoRepositoryBlueprint GetBlueprint()
    {
        throw new NotImplementedException();
    }
}