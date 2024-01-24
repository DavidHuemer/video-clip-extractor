using System.IO;
using BaseUI.ViewModels.Tree;

namespace VideoClipExtractor.Data.VideoRepos.Pc;

public class PcDrive : VideoRepositoryDrive
{
    public PcDrive(string name) : base(name)
    {
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren() => Directory.GetDirectories(Path)
        .Select(dir => new PcDirectory(dir));
}