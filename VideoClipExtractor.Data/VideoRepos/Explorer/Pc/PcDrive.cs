using System.IO;
using BaseUI.ViewModels.Tree;

namespace VideoClipExtractor.Data.VideoRepos.Explorer.Pc;

public class PcDrive(string name) : VideoRepositoryDrive(name)
{
    protected override IEnumerable<BaseTreeViewItem> LoadChildren() => Directory.GetDirectories(Path)
        .Select(dir => new PcDirectory(dir));
}