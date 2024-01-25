using System.IO;
using BaseUI.ViewModels.Tree;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Data.VideoRepos.Explorer.Pc;

public class PcDrive(string name) : VideoRepositoryDrive(name)
{
    protected override IEnumerable<BaseTreeViewItem> LoadChildren() => Directory.GetDirectories(Path)
        .Select(dir => new PcDirectory(dir));

    public override VideoRepositoryBlueprint GetBlueprint() => new(VideoRepositoryType.Pc, Path);
}